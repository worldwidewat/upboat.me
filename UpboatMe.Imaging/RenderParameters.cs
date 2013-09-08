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
        public Color Fill { get; set; }
        public Color Stroke { get; set; }
        public int StrokeWidth { get; set; }
        public bool DebugMode { get; set; }
        public string FullWatermarkImageFilePath { get; set; }
        public int WatermarkImageWidth { get; set; }
        public int WatermarkImageHeight { get; set; }
        public string WatermarkText { get; set; }
        public Color WatermarkStroke { get; set; }
        public Color WatermarkFill { get; set; }
        public int WatermarkStrokeWidth { get; set; }
        public string WatermarkFont { get; set; }
        public int WatermarkFontSize { get; set; }
    }
}
