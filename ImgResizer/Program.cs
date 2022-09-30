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
                    var allfiles = Directory.GetFiles(operation.).Select(x => new FileInfo(x)).ToList();
                            foreach (var file in allfiles)
                            {
                                var fileSplited = file.Name.Split(".");
                                if ((fileSplited[fileSplited.Length-1]=="jpeg")|| (fileSplited[fileSplited.Length-1] == "jpg"))
                                {
                                    Console.WriteLine(file.Name);
                                }
                                
                            }
                        
                            break;
                    }

            }
        }


    }
}
