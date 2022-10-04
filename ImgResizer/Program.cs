using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ImgResizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = new Input(args);
            var operation = new ImgManager(input.Path);
            switch (input.Type)
            {
                case Commands.resize:
                    {
                        Console.WriteLine("");
                        break;
                    }
                case Commands.thumbs:
                    {
                        operation.Thumbs();
                        break;
                    }
            }
        }
    }
}
