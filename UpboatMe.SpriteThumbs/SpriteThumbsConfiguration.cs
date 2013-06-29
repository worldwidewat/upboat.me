using System;
using System.Collections.Generic;
using System.IO;

namespace UpboatMe.SpriteThumbs
{
    public class SpriteThumbsConfiguration
    {
        public const string SpriteResource = "/thumbs.axd?type=sprite";
        public const string StylesheetResource = "/thumbs.axd?type=stylesheet";

        public SpriteThumbsConfiguration()
        {
            ThumbImagesPath = @".\thumbs";
            ThumbWidth = 100;
            ThumbHeight = 100;
            ThumbsPerRow = 10;
            ImageQualityPercent = 50;
            SpriteOutputPath = @".\sprites";
            SpriteFileName = "sprite.jpg";
            StylesheetFileName = "sprite.css";
            RawImagesPath = @".\images";
        }

        public string ThumbImagesPath { get; private set; }
        public string SpriteOutputPath { get; private set; }
        public int ThumbWidth { get; private set; }
        public int ThumbHeight { get; private set; }
        public int ThumbsPerRow { get; private set; }
        public int ImageQualityPercent { get; private set; }
        public string SpriteFileName { get; private set; }
        public string StylesheetFileName { get; private set; }
        public string RawImagesPath { get; private set; }

        public void SetThumbImagesPath(string thumbImagesPath)
        {
            ThumbImagesPath = thumbImagesPath;
        }

        public string SpriteFullFileName
        {
            get
            {
                return Path.Combine(SpriteOutputPath, SpriteFileName);
            }
        }

        public string StylesheetFilePath
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

        public void SetRawImagesPath(string rawImagesPath)
        {
            RawImagesPath = rawImagesPath;
        }
    }
}