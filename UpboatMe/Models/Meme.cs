using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace UpboatMe.Models
{
    public class Meme
    {
        private const string ImagePathFormat = "~/Images/{0}";

        public string ImagePath { get; private set; }
        public string ImageType { get; private set; }
        public string ImageFileName { get; private set; }
        public string Description { get; private set; }
        public IList<string> Aliases { get; set; }
        public IReadOnlyList<LineConfig> Lines { get; private set; }

        public string ImageFileNameWithoutExtension
        {
            get { return Path.GetFileNameWithoutExtension(ImageFileName); }
        }


        public Meme(string description, string imageFileName, IList<string> aliases, string imageType = "image/jpg", int lines = 2)
        {
            Description = description;
            ImagePath = string.Format(ImagePathFormat, imageFileName);
            ImageFileName = imageFileName;
            Aliases = aliases;
            ImageType = imageType;
            Lines = Enumerable.Range(0, lines).Select(i => new LineConfig()).ToList().AsReadOnly();
        }
    }

    public class LineConfig
    {
        public Color Stroke { get; set; }
        public Color Fill { get; set; }
        public string Font { get; set; }
        public int FontSize { get; set; }
        public FontStyle FontStyle { get; set; }
        public int StrokeWidth { get; set; }
        public StringAlignment TextAlignment { get; set; }
        public StringAlignment LineAlignment { get; set; }
        public bool DoForceTextToAllCaps { get; set; }
        public float HeightPercent { get; set; }
        public Rectangle? Bounds { get; set; }

        public LineConfig(string stroke = "black",
            string fill = "white",
            string font = "Impact",
            FontStyle fontStyle = FontStyle.Regular,
            int fontSize = 40,
            int strokeWidth = 5,
            float heightPercent = 25,
            StringAlignment textAlignment = StringAlignment.Center,
            StringAlignment lineAlignment = StringAlignment.Near,
            bool doForceTextToAllCaps = true)
        {
            Stroke = Color.FromName(stroke);
            Fill = Color.FromName(fill);
            Font = font;
            FontSize = fontSize;
            StrokeWidth = strokeWidth;
            HeightPercent = heightPercent;
            TextAlignment = textAlignment;
            LineAlignment = lineAlignment;
            DoForceTextToAllCaps = doForceTextToAllCaps;
            FontStyle = fontStyle;
        }
    }
}