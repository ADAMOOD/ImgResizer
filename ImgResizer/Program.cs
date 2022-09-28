using System;
using System.IO;

namespace ImgResizer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string Path = null;
            while (Path == null)
            {
                if (args.Length == 0)
                {

                    Path = GetPath();

                }
                else
                {
                    if (!Directory.Exists(args[0]))
                    {
                        Path = GetPath();
                        continue;
                    }
                    Path = args[0];
                }
            }


        }

        public static string GetPath()
        {
            Console.WriteLine("Enter Path:");
            string inputString = Console.ReadLine();
            if (Directory.Exists(inputString))
            {
                return inputString;
            }
            Console.WriteLine("Invalid Path");
            return null;
        }
    }

}
