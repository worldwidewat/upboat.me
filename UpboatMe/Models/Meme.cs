using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace UpboatMe.Models
{
    public class Meme
    {
        private static string ImagePathFormat = "~/Images/{0}";

        public string ImagePath { get; private set; }
        public string ImageType { get; private set; }
        public string ImageFileName { get; private set; }
        public Brush Stroke { get; private set; }
        public Brush Fill { get; private set; }
        public string Font { get; private set; }
        public int FontSize { get; private set; }
        public int StrokeWidth { get; private set; }
        public float TopLineHeightPercent { get; private set; }
        public float BottomLineHeightPercent { get; private set; }
        public string Description { get; private set; }
        public IList<string> Aliases { get; set; }

        public string ImageFileNameWithoutExtension
        {
            get { return Path.GetFileNameWithoutExtension(ImageFileName); }
        }

        public Meme(
            string description,
            string imageFileName,
            IList<string> aliases,
            string imageType = "image/jpg",
            string stroke = "black",
            string fill = "white",
            string font = "Impact",
            int fontSize = 40,
            int strokeWidth = 5,
            float topLineHeightPercent = 25,
            float bottomLineHeightPercent = 25)
        {
            Description = description;
            ImagePath = string.Format(ImagePathFormat, imageFileName);
            ImageFileName = imageFileName;
            Aliases = aliases;
            ImageType = imageType;
            Stroke = new SolidBrush(Color.FromName(stroke));
            Fill = new SolidBrush(Color.FromName(fill));
            Font = font;
            FontSize = fontSize;
            StrokeWidth = strokeWidth;
            TopLineHeightPercent = topLineHeightPercent;
            BottomLineHeightPercent = bottomLineHeightPercent;
        }
    }
}