using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImgResizer
{
    internal class Helpers
    {
        public static void Error(string errorMsg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMsg);
            Console.ResetColor();
        }
        public static bool CheckIfArrayIsEmpty(List<FileInfo> files)
        {
            return (files == null || files.Count == 0);
        }

        public static void CheckingIfDirExistAndMakingIt(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
