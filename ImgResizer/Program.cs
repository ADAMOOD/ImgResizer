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
            switch (operation.idk)
            {
                case 0:
                    {
                        if (!Directory.Exists($"{operation.Path}\\thumbs"))
                        {
                            Directory.CreateDirectory($"{operation.Path}\\thumbs");
                            var allfiles = Directory.GetFiles(operation.Path).Select(x => new FileInfo(x)).ToList();
                            foreach (var file in allfiles)
                            {
                              /*  if ()
                                {
                                    
                                }
                                var images=*/
                            }
                        }
                            break;
                    }

            }
        }


    }
}
