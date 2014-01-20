using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace UpboatMe.Models
{
    public class Meme
    {
        private const string ImagePathFormat = "~/Images/{0}";

        public string ImagePath { get; private set; }
        public string ImageType { get; private set; }
        public string ImageFileName { get; private set; }
        public Color Stroke { get; set; }
        public Color Fill { get; set; }
        public string Font { get; set; }
        public int FontSize { get; private set; }
        public int StrokeWidth { get;  set; }
        public float TopLineHeightPercent { get; private set; }
        public Rectangle? TopLineBounds { get; set; }
        public float BottomLineHeightPercent { get; private set; }
        public Rectangle? BottomLineBounds { get; set; }
        public string Description { get; private set; }
        public IList<string> Aliases { get; set; }
        public StringAlignment TextAlignment { get; set; }
        public bool DoForceTextToAllCaps { get; set; }

        public string ImageFileNameWithoutExtension
        {
            get { return Path.GetFileNameWithoutExtension(ImageFileName); }
        }

        public FontStyle FontStyle { get; set; }

        public Meme(
            string description,
            string imageFileName,
            IList<string> aliases,
            string imageType = "image/jpg",
            string stroke = "black",
            string fill = "white",
            string font = "Impact",
            FontStyle fontStyle = FontStyle.Regular,
            int fontSize = 40,
            int strokeWidth = 5,
            float topLineHeightPercent = 25,
            float bottomLineHeightPercent = 25,
            StringAlignment textAlignment = StringAlignment.Center,
            bool doForceTextToAllCaps = true)
        {
            Description = description;
            ImagePath = string.Format(ImagePathFormat, imageFileName);
            ImageFileName = imageFileName;
            Aliases = aliases;
            ImageType = imageType;
            Stroke = Color.FromName(stroke);
            Fill = Color.FromName(fill);
            Font = font;
            FontSize = fontSize;
            StrokeWidth = strokeWidth;
            TopLineHeightPercent = topLineHeightPercent;
            BottomLineHeightPercent = bottomLineHeightPercent;
            TextAlignment = textAlignment;
            DoForceTextToAllCaps = doForceTextToAllCaps;
            FontStyle = fontStyle;
        }

    }
}