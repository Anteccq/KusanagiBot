# Kusanagi Bot
> A Discord Bot for custom command.

カスタムコマンドを作るためのDiscord用Botです。  
[Screenshot](Images/KusanagiBot.png)

コマンドの形は自由です。  
コマンドに絵文字を含めることもできます。

## Requirements
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* Humor

## Development
DiscordのBot用トークンを`appsettings.Development.json`にDiscordTokenとして登録する必要があります。  
KusanagiBot.cs と同じフォルダに追加してください。  
(例)

```json:appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "DiscordToken": "YourToken"
}
```

## License
Under the MIT

## 権利表記
ConsoleAppFramework Copyright (c) 2020 Cysharp, Inc.  
[LICENSE](https://github.com/Cysharp/ConsoleAppFramework/blob/master/LICENSE)

Discord.Net Copyright (c) 2015-2019 Discord.Net Contributors  
[LICENSE](https://github.com/discord-net/Discord.Net/blob/dev/LICENSE)  