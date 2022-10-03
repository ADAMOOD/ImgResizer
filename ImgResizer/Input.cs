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
        public void Thumbs()
        {
            var allfiles = Directory.GetFiles(Path).Select(x => new FileInfo(x)).ToList();
            var Images = FindImages(allfiles);
            if (CheckIfArrayIsEmpty(allfiles))
            {
                Helpers.Error($"Can't find any image in Directory ->{Path}");
            }

            foreach (var image in Images)
            {
                var time = new Stopwatch();
                time.Start();
                using (var img = Image.Load(image.FullName))
                {
                    img.Mutate(i => i.Resize(new ResizeOptions
                    {
                        Size = new Size(75),
                        Mode = ResizeMode.Crop
                    }));
                    img.SaveAsJpeg($"{Path}\\thumbs\\{image.Name}.jpeg");
                }
                time.Stop();
                Console.WriteLine($"Image thumb for: {GetFileName(image)}.jpeg created in {time.ElapsedMilliseconds}ms");
            }
        }

       private static string GetFileName(FileInfo file)
        {
            return file.Name.Replace(file.Extension, string.Empty);
        }
        private static List<FileInfo> FindImages(List<FileInfo> allfiles)
        {
            List<FileInfo> Images = new List<FileInfo>();
            foreach (var file in allfiles)
            {
                
                if ((file.Extension == ".jpeg") || (file.Extension == ".jpg"))
                {
                    Images.Add(file);
                }
            }
            return Images;
        }

        public static bool CheckIfArrayIsEmpty(List<FileInfo> files)
        {
            return (files == null || files.Count == 0);
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

            Helpers.Error($"Invalid path ->{Path}");
            Environment.Exit(0);//Po odebrání se bude program ptát na cestu dokud uživatel nezadá existující cestu
            return null;
        }
    }
}