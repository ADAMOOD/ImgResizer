using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ImgResizer
{
    internal class Program
    {
        public static string[] Commands = { "-r", "--resize", "-t", "--thumbs", "-c", "--clean" };
        static void Main(string[] args)
        {
            string path = null;
            string command = null;
            #region GettingInputs
            while ((path == null) || (command == null))
            {
                if (args.Length == 0)
                {
                    if (path == null)
                    {
                        path = GetPath();
                    }
                    if (command == null)
                    {
                        command = GetCommand();
                    }
                }
                if (args.Length == 1)
                {
                    if (!Directory.Exists(args[0]))
                    {
                        path = GetPath();
                        command = GetCommand();
                        continue;
                    }
                    path = args[0];
                    command = GetCommand();
                }
                if (args.Length == 2)
                {
                    if (!Directory.Exists(args[0]))
                    {
                        path = GetPath();
                        continue;
                    }
                    path = args[0];
                    if (Commands.Contains(args[1]))
                    {
                        command = args[1];
                        continue;
                    }
                    command = GetCommand();
                }
            }
            #endregion


        }

        public static string GetCommand()
        {
            int x;
            
            Console.WriteLine("Enter command");
            string inputCommand = Console.ReadLine();
            if (inputCommand.Contains("="))
            {
               string[] sub= inputCommand.Split('=');
               int num;
               int.TryParse(sub[1],out num);
                if(((sub[0]=="-w"||(sub[0]=="--width"))&&((num<1200)&&(num>100))))
                {
                    return inputCommand;
                }
                return null;
            }
            if (Commands.Contains(inputCommand))
            {
                return inputCommand;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid command");
            Console.ResetColor();
            return null;

        }
        public static string GetPath()
        {
            Console.WriteLine("Enter path:");
            string inputString = Console.ReadLine();
            if (Directory.Exists(inputString))
            {
                return inputString;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid path");
            Console.ResetColor();
            Environment.Exit(0);//Po odebrání se bude program ptát na cestu dokud uživatel nezadá existující cestu
            return null;
        }
    }

}
