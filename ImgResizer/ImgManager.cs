using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImgResizer
{
    public class ImgManager
    {
        public string Path;
        public ImgManager(string path)
        {
            Path = path;
        }

        #region Thumbs
        public void Thumbs()
        {
            string thumbsDirPath = $"{Path}\\thumbs";
            Helpers.CheckingIfDirExistAndMakingIt(thumbsDirPath);
            var Images = FindImages();
            if (Helpers.CheckIfArrayIsEmpty(Images))
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
                    img.SaveAsJpeg($"{thumbsDirPath}\\{image.Name}.jpeg");
                }
                time.Stop();
                Console.WriteLine($"Image thumb for: {GetFileName(image)}.jpeg created in {time.ElapsedMilliseconds}ms");
            }
        }


        #endregion

        public void Clean()
        {
            if (Directory.Exists($"{Path}\\thumbs"))
            {
                Directory.Delete($"{Path}\\thumbs", true);
            }
            var imgs = FindImages();

            foreach (var img in imgs)
            {
                if (img.Extension.Equals(".jpeg"))
                {
                    var arr = img.Name.Split(".");
                    
                    if (arr[arr.Length - 2].Equals("width") )
                    {
                        string nameWithoutWidth = null;
                        for (int i = 0; i < arr.Length - 2; i++)
                        {
                            nameWithoutWidth+=arr[i];
                        }
                        nameWithoutWidth += img.Extension;
                        var imgsNames = imgs.Select(i => i.Name).ToList();
                        if (imgsNames.Contains(nameWithoutWidth))
                        {
                            img.Delete();
                        }

                    }
                }

            }
        }

        private static string GetFileName(FileInfo file)
        {
            return file.Name.Replace(file.Extension, string.Empty);
        }

        private List<FileInfo> FindImages()
        {
            var allfiles = Directory.GetFiles(Path).Select(x => new FileInfo(x)).ToList();
            List<FileInfo> Images = new List<FileInfo>();
            foreach (var file in allfiles)
            {

                if ((file.Extension.Equals(".jpeg")) || (file.Extension.Equals(".jpg")))
                {
                    Images.Add(file);
                }
            }
            return Images;
        }
    }
}