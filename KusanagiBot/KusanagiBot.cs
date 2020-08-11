using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppFramework;
using Discord;
using Discord.WebSocket;
using KusanagiBot.Model;
using Microsoft.Extensions.Options;

namespace KusanagiBot
{
    public class KusanagiBot : ConsoleAppBase
    {
        private IOptions<Config> _config;
        public KusanagiBot(IOptions<Config> config)
        {
            _config = config;
        }
        public async Task ExecuteAsync()
        {
            var client = new DiscordSocketClient();
            client.Log += message =>
            {
                Console.WriteLine($"{message.Message} : {message.Exception}");
                return Task.CompletedTask;
            };

            client.MessageReceived += MessageHandle;
            await client.LoginAsync(TokenType.Bot, _config.Value.DiscordToken);
            await client.StartAsync();

            await Task.Delay(-1, Context.CancellationToken);

            await client.StopAsync();
        }

        async Task MessageHandle(SocketMessage message)
        {
            if (!(message is SocketUserMessage um) || um.Author.IsBot) return;

            var msgArray = message.Content.Split(new char[] { ' ', '　' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (msgArray.Length == 0) return;
            if (await DefaultCommand(um, msgArray)) return;
            var res = Command.FindCommand(msgArray[0]);
            if (!string.IsNullOrEmpty(res)) await um.Channel.SendMessageAsync(res);
        }

        private string[] defaultCommands = new []
        {
            "!add",
            "!delete",
            "!edit"
        };

        async Task<bool> DefaultCommand(SocketUserMessage m, string[] msg)
        {
            //もっとスマートなやり方ないかなぁ
            if (msg[0] == "!list")
            {
                var hasCmd = Command.Commands.Keys.Count != 0;
                var eb = new EmbedBuilder()
                {
                    Color = Color.DarkBlue,
                    Title = "Command List",
                    Description = hasCmd
                        ? Command.Commands.Keys.Aggregate((a, b) => $"{a}\n{b}").ToString()
                        : "コマンドはありません。"
                };
                await m.Channel.SendMessageAsync(embed: eb.Build());
                return true;
            }
            if (msg.Length < 2 && defaultCommands.Any(x => x == msg[0]))
            {
                await m.Channel.SendMessageAsync("コマンドの引数が正しくありません。");
                return false;
            }

            var f = true;
            switch (msg[0])
            {
                case "!add":
                    var s = msg[1].Split(new char[] { ' ', '　' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (s.Length == 2)
                    {
                        _ = Command.TryAddCommand(s[0], s[1])
                            ? await m.Channel.SendMessageAsync($"コマンド {s[0]} が追加されました。")
                            : await m.Channel.SendMessageAsync($"コマンド追加に失敗しました。なんででしょうね？");
                    }
                    else await m.Channel.SendMessageAsync("!add [command名] [response]");
                    break;

                case "!delete":
                    _ = Command.TryDeleteCommand(msg[1])
                        ? await m.Channel.SendMessageAsync($"コマンド {msg[0]} が削除されました。かなしい。")
                        : await m.Channel.SendMessageAsync($"コマンド削除に失敗しました。");
                    break;
                case "!edit":
                    var ss = msg[1].Split(new char[] { ' ', '　' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (ss.Length == 2)
                    {
                        _ = Command.TryEditCommand(ss[0], ss[1])
                            ? await m.Channel.SendMessageAsync($"{ss[0]} => {ss[1]}")
                            : await m.Channel.SendMessageAsync($"コマンド編集に失敗しました。なんででしょうね？");
                    }
                    else await m.Channel.SendMessageAsync("!edit [command名] [response]");
                    break;
                default:
                    f = false;
                    break;
            }

            return f;
        }
    }
}
