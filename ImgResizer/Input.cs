using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImgResizer
{
    public enum Commands
    {
        resize, thumbs, clean
    }
    public class Input
    {
        public static string[] Commands = { "-t", "--thumbs", "-c", "--clean" };
        public string Path = null;
        public string Command = null;
        public Commands Type { get; set; }
        public Input(string[] args)
        {
            string pathHelp = null;
            string commandHelp = null;
            while ((pathHelp == null) || (commandHelp == null))
            {
                if (args.Length == 0)
                {
                    if (pathHelp == null)
                    {
                        pathHelp = GetPath();
                    }
                    if (commandHelp == null)
                    {
                        commandHelp = GetCommand();
                    }
                }
                if (args.Length > 0)
                {
                    if (!Directory.Exists(args[0]))
                    {
                        pathHelp = GetPath();
                        continue;
                    }
                    if (String.IsNullOrEmpty(args[1]))
                    {
                        commandHelp = GetCommand();
                    }
                    commandHelp = CheckCommand(args[1]);
                }
            }
            Type = GetCommandType(commandHelp);
            Path = pathHelp;
            Command = commandHelp;
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
                Helpers.Error("Width command");
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
            Helpers.Error($"Invalid input ->{inputCommand}");
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
            Helpers.Error($"Invalid input ->{inputCommand}");
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
                return ImgResizer.Commands.thumbs;
            }
            if (inputCommand.Contains("-c"))
            {
                return ImgResizer.Commands.clean;
            }
            return ImgResizer.Commands.clean;

        }

        private  string GetPath()
        {
            Console.WriteLine("Enter Path:");
            string inputString = Console.ReadLine();
            if (Directory.Exists(inputString))
            {
                return inputString;
            }

            Helpers.Error($"Invalid path ->{Path}");
            Environment.Exit(0);
            return null;
        }
    }
}