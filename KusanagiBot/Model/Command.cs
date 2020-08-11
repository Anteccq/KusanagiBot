using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KusanagiBot.Model
{
    public class Command
    {
        public static Dictionary<string, string> Commands { get; set; }

        public static string FindCommand(string command)
        {
            return Commands.TryGetValue(command, out var response) 
                ? response
                : null;
        }

        public static bool TryAddCommand(string command, string response) => Commands.TryAdd(command, response);

        public static bool TryDeleteCommand(string command) => Commands.Remove(command);

        public static bool TryEditCommand(string command, string response)
        {
            if (!Commands.ContainsKey(command)) return false;
            try
            {
                Commands[command] = response;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string[] ListCommand() => Commands.Values.ToArray();
    }
}
