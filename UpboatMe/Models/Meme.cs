using System.Collections.Generic;
using System.Drawing;

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
        public string Description { get; private set; }
        public IList<string> Aliases { get; set; }

        public Meme(
            string description,
            string imagePath,
            IList<string> aliases,
            string imageType = "image/png",
            string stroke = "black",
            string fill = "white",
            string font = "Impact",
            int fontSize = 40,
            int topLineHeight = 200,
            int bottomLineHeight = 200)
        {
            Description = description;
            ImagePath = string.Format(ImagePathFormat, imagePath);
            Aliases = aliases;
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