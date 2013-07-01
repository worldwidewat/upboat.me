using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace UpboatMe.SpriteThumbs
{
    public class SpriteThumbsConfiguration
    {
        public const string SpriteResource = "/thumbs.axd?type=sprite";
        public const string StylesheetResource = "/thumbs.axd?type=stylesheet";
        public const string HashFileName = "SpriteThumbsResourceHash.txt";

        public SpriteThumbsConfiguration()
        {
            ThumbWidth = 100;
            ThumbHeight = 100;
            ThumbsPerRow = 10;
            ImageQualityPercent = 50;
            SpriteOutputPath = @".\sprites";
            SpriteFileName = "sprite.jpg";
            StylesheetFileName = "sprite.css";
            RawImages = new List<RawImage>();
        }

        public string SpriteOutputPath { get; private set; }
        public int ThumbWidth { get; private set; }
        public int ThumbHeight { get; private set; }
        public int ThumbsPerRow { get; private set; }
        public int ImageQualityPercent { get; private set; }
        public string SpriteFileName { get; private set; }
        public string StylesheetFileName { get; private set; }
        public IList<RawImage> RawImages { get; private set; }

        public string FullHashFileName
        {
            get
            {
                return Path.Combine(SpriteOutputPath, HashFileName);
            }
        }

        public string SpriteResourceUrl
        {
            get
            {
                return string.Format("{0}&hash={1}", SpriteResource, GetResourceHash());
            }
        }

        public string StylesheetResourceUrl
        {
            get
            {
                return string.Format("{0}&hash={1}", StylesheetResource, GetResourceHash());
            }
        }

        public string GetResourceHash()
        {

            var count = RawImages.Count;
            var lastModified = RawImages.Max(i => i.LastWriteTime);
            var input = string.Format("{0}-{1}", count, lastModified);
            var algorithm = MD5.Create();
            var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            return string.Join("", hash.Select(h => h.ToString("X2")));
        }

        public string SpriteFullFileName
        {
            get
            {
                return Path.Combine(SpriteOutputPath, SpriteFileName);
            }
        }

        public string StylesheetFullFileName
        {
            get
            {
                return Path.Combine(SpriteOutputPath, StylesheetFileName);
            }
        }

        public void SetThumbSize(int thumbWidth, int thumbHeight)
        {
            ThumbWidth = thumbWidth;
            ThumbHeight = thumbHeight;
        }

        public void SetThumbsPerRow(int thumbsPerRow)
        {
            ThumbsPerRow = thumbsPerRow;
        }

        public void SetImageQualityPercent(int imageQualityPercent)
        {
            if (imageQualityPercent <= 0 || imageQualityPercent > 100)
            {
                throw new ArgumentOutOfRangeException("imageQualityPercent", "Value must be 1 - 100");
            }

            ImageQualityPercent = imageQualityPercent;
        }

        public void SetSpriteOutputPath(string spriteOutputPath)
        {
            if (!Directory.Exists(spriteOutputPath))
            {
                throw new ArgumentOutOfRangeException("spriteOutputPath", "Directory does not exist!");
            }

            SpriteOutputPath = spriteOutputPath;
        }

        public void SetSpriteOutputFileNames(string spriteFileName, string stylesheetFileName)
        {
            SpriteFileName = spriteFileName;
            StylesheetFileName = stylesheetFileName;
        }

        public void AddRawImages<T>(IEnumerable<T> collection, Func<T, string> idSelector, Func<T, string> fullFilePathSelector)
        {
            foreach (var item in collection)
            {
                RawImages.Add(new RawImage
                {
                    Id = idSelector(item),
                    FullFilePath = fullFilePathSelector(item)
                });
            }
        }

        public static string GetThumbClassName()
        {
            return "thumb";
        }

        public static string GetImageClassName(string imageIdentifier)
        {
            return string.Format("bg-{0}", imageIdentifier);
        }
    }
}