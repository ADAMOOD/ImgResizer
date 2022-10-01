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
            var operation = new Input(args);
           
            switch (operation.Type)
            {
                case Commands.resize:
                {
                    Console.WriteLine("");
                    break;
                }
                case Commands.thumbs:
                {
                    Input.Thumbs(operation);
                    break;
                }

            }
        }


    }
}
