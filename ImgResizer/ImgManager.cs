using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                if ((file.Extension == ".jpeg") || (file.Extension == ".jpg"))
                {
                    Images.Add(file);
                }
            }
            return Images;
        }
    }
}