using System.Drawing;

namespace UpboatMe.Imaging
{
    public class RenderParameters
    {
        public string FullImagePath { get; set; }
        public int FontSize { get; set; }
        public float TopLineHeightPercent { get; set; }
        public float BottomLineHeightPercent { get; set; }
        public string Font { get; set; }
        public Brush Fill { get; set; }
        public Brush Stroke { get; set; }
        public int StrokeWidth { get; set; }
        public bool DrawBoxes { get; set; }
    }
}
