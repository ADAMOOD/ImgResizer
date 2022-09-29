using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ImgResizer
{
    public enum commands
    {
        resize, thumbs, clean
    }
    public class Input
    {

        public static string[] Commands = { "-t", "--thumbs", "-c", "--clean" };
        public string Path = null;
        public string Command = null;
        public commands idk { get; set; }
        public Input(string[] args)
        {
            while ((Path == null) || (Command == null))
            {
                if (args.Length == 0)
                {
                    if (Path == null)
                    {
                        Path = GetPath();
                    }
                    if (Command == null)
                    {
                        Command = GetCommand();
                    }
                }

                if (args.Length > 0)
                {
                    if (!Directory.Exists(args[0]))
                    {
                        Path = GetPath();
                        Command = GetCommand();
                        continue;
                    }
                    for (int i = 1; i < args.Length; i++)
                    {
                        Command = CheckCommand(args[i]);
                    }
                }
            }
            idk = GetCommandType(Command);
        }
        private static string GetCommand()
        {
            Console.WriteLine("Enter Command");
            string inputCommand = Console.ReadLine();
            return CheckCommand(inputCommand);
        }

        private static string CheckCommand(string inputCommand)
        {
            if (inputCommand.Contains("="))
            {
                string[] sub = inputCommand.Split('=');
                int num;
                int.TryParse(sub[1], out num);
                if (((Regex.Replace(sub[0], @"\s+", "") == "-r-w")//špinave
                     || (Regex.Replace(sub[0], @"\s+", "") == "-resize-w")//špinave
                     || (Regex.Replace(sub[0], @"\s+", "") == "-r--width")//špinave
                     || (Regex.Replace(sub[0], @"\s+", "") == "-resize--width"))//špinave
                    && ((num < 1200) && (num > 100)))
                {
                    return inputCommand;
                } 
                Helpers.InvalidInput("Command");
                Environment.Exit(0);
                return null;
            }

            if (Commands.Contains(inputCommand))
            {
                return inputCommand;
            }

            Helpers.InvalidInput("Command");
            Environment.Exit(0);
            return null;
        }

        private static commands GetCommandType(string inputCommand)
        {
            if (inputCommand.Contains("="))
            {
                return commands.resize;
            }
            if (inputCommand.Contains("-t"))
            {
                return commands.thumbs;
            }
            if (inputCommand.Contains("-c"))
            {
                return commands.clean;
            }
            return commands.clean ;

        }
        private static string GetPath()
        {
            Console.WriteLine("Enter Path:");
            string inputString = Console.ReadLine();
            if (Directory.Exists(inputString))
            {
                return inputString;
            }

            Helpers.InvalidInput("Path");
            Environment.Exit(0);//Po odebrání se bude program ptát na cestu dokud uživatel nezadá existující cestu
            return null;
        }
    }


}