using System;
using System.Collections.Generic;
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
    }
}
