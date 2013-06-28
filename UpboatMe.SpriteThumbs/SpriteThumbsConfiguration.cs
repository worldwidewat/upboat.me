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
            ImagePaths = new List<string>();
            Width = 100;
            Height = 100;
            ThumbsPerRow = 10;
            ImageQualityPercent = 50;
            OutputPath = "App_Data";
            SpriteFileName = "sprite.jpg";
            StylesheetFileName = "sprite.css";
        }

        public List<string> ImagePaths { get; private set; }
        public string OutputPath { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int ThumbsPerRow { get; private set; }
        public int ImageQualityPercent { get; private set; }
        public string SpriteFileName { get; private set; }
        public string StylesheetFileName { get; private set; }

        public string SpriteFilePath
        {
            get
            {
                return Path.Combine(OutputPath, SpriteFileName);
            }
        }

        public string StylesheetFilePath
        {
            get
            {
                return Path.Combine(OutputPath, StylesheetFileName);
            }
        }

        public void SetThumbSize(int width, int height)
        {
            Width = width;
            Height = height;
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

        public void SetOutputPath(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new ArgumentOutOfRangeException("path", "Directory does not exist!");
            }

            OutputPath = path;
        }

        public void SetOutputFileNames(string spriteFileName, string stylesheetFileName)
        {
            SpriteFileName = spriteFileName;
            StylesheetFileName = stylesheetFileName;
        }
    }
}