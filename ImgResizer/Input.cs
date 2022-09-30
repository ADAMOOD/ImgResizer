using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ImgResizer
{
    public enum Commands
    {
        resize, thumbs, clean
    }
    public class Input
    {

        public static string[] Commands = { "-t", "--thumbs", "-c", "--clean" };
        public static string Path = null;
        public string Command = null;
        public Commands Type { get; set; }
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
                        continue;
                    }
                    if (String.IsNullOrEmpty(args[1]))
                    {
                        Command = GetCommand();
                    }
                    Command = CheckCommand(args[1]);
                }
            }
            Type = GetCommandType(Command);
        }
        private static string GetCommand()
        {
            Console.WriteLine("Enter Command");
            string inputCommand = Console.ReadLine();
            return CheckCommand(inputCommand);
        }

        private static string CheckCommand(string inputCommand)
        {
            if ((Regex.Replace(inputCommand, @"\s+", "") == "-r") || (Regex.Replace(inputCommand, @"\s+", "") == "--resize"))
            {
                Helpers.InvalidInput("Width command");
                Console.WriteLine("please Enter width command");
                return CheckResizeWidthCommand($"{inputCommand}{Console.ReadLine()}");
            }
            if (inputCommand.Contains("="))
            {
                return CheckResizeWidthCommand(inputCommand);
            }

            if (Commands.Contains(inputCommand))
            {
                return inputCommand;
            }
            Helpers.InvalidInput("Command");
            Environment.Exit(0);
            return null;
        }

        private static string CheckResizeWidthCommand(string inputCommand)
        {
            string[] sub = inputCommand.Split('=');
            int num;
            int.TryParse(sub[1], out num);
            if (((Regex.Replace(sub[0], @"\s+", "") == "-r-w") //špinave
                 || (Regex.Replace(sub[0], @"\s+", "") == "-resize-w") //špinave
                 || (Regex.Replace(sub[0], @"\s+", "") == "-r--width") //špinave
                 || (Regex.Replace(sub[0], @"\s+", "") == "-resize--width")) //špinave
                && ((num < 1200) && (num > 100)))
            {
                return inputCommand;
            }
            Helpers.InvalidInput("Command");
            return null;
        }

        private static Commands GetCommandType(string inputCommand)
        {
            if (inputCommand.Contains("="))
            {
                return ImgResizer.Commands.resize;
            }
            if (inputCommand.Contains("-t"))
            {
                CheckingIfDirExistAndMakingIt();
                return ImgResizer.Commands.thumbs;
            }
            if (inputCommand.Contains("-c"))
            {
                return ImgResizer.Commands.clean;
            }
            return ImgResizer.Commands.clean;

        }
        private static void CheckingIfDirExistAndMakingIt()
        {
            string thumbsPath = $"{Path}\\thumbs";
            if (!Directory.Exists(thumbsPath))
            {
                Directory.CreateDirectory(thumbsPath);
            }
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