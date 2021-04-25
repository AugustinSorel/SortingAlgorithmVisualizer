namespace SortingAlgorithmVisualizer
{
    static class GlobalColors
    {
        private const string FOREGROUND_COLOR = "FFFFFF";
        private const string BIGRECTANGLE_COLOR = "c0392b";
        private const string SMALLRECTANGLE_COLOR = "2ecc71";

        public static string BackgroundColor { get; set; } = "black";
        public static string StripsColor { get; set; } = "#FF" + FOREGROUND_COLOR;
        public static string ForegroundColor { get; set; } = "#FF" + FOREGROUND_COLOR;
        public static string BigRectangleColor { get; set; } = "#FF" + BIGRECTANGLE_COLOR;
        public static string SmallRectangleColor { get; set; } = "#FF" + SMALLRECTANGLE_COLOR;
    }
}
