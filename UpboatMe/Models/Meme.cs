using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace UpboatMe.Models
{
    public class Meme
    {
        private static string ImagePathFormat = "~/Images/{0}-template.png";

        public string ImagePath { get; private set; }
        public string ImageType { get; private set; }
        public Brush Stroke { get; private set; }
        public Brush Fill { get; private set; }
        public string Font { get; private set; }
        public int FontSize { get; private set; }
        public int TopLineHeight { get; private set; }
        public int BottomLineHeight { get; private set; }

        public Meme(
            string imagePath, 
            string imageType = "image/png",
            string stroke = "black", 
            string fill = "white", 
            string font = "Impact", 
            int fontSize = 40, 
            int topLineHeight = 200, 
            int bottomLineHeight = 200)
        {
            ImagePath = string.Format(ImagePathFormat, imagePath);
            ImageType = imageType;
            Stroke = new SolidBrush(Color.FromName(stroke));
            Fill = new SolidBrush(Color.FromName(fill));
            Font = font;
            FontSize = fontSize;
            TopLineHeight = topLineHeight;
            BottomLineHeight = bottomLineHeight;
        }
    }
}