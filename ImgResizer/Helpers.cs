using System;
using System.Collections.Generic;
using System.Text;

namespace ImgResizer
{
    internal class Helpers
    {

        public static void InvalidInput(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Invalid {input}");
            Console.ResetColor();
        }
    }
}
