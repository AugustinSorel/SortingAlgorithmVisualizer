using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SortingAlgorithmVisualizer
{
    class MainWindowViewModel
    {
        private Canvas canvas;
        private Rectangle[] oldRectangles = new Rectangle[2];
        private readonly ProgressBar progressBar;

        public SortingEngine SortingEngine { get; set; }

        public MainWindowViewModel(ComboBox comboBox, ProgressBar progressBar, Canvas canvas)
        {
            SortingEngine = new SortingEngine();
            this.canvas = canvas;
            this.progressBar = progressBar;
            PopulateComboBox(comboBox);
        }

        #region Combo Box
        private void PopulateComboBox(ComboBox comboBox)
        {
            List<string> ClassList = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(ISortAlo).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name)
                .ToList();
            ClassList.Sort();

            foreach (string entry in ClassList)
                comboBox.Items.Add(entry);

            comboBox.SelectedIndex = 0;
        }
        #endregion

        internal void Abort()
        {
            SortingEngine.Abort();
        }

        internal void Pause()
        {
            SortingEngine.Pause();
        }

        internal void Start(string algoName)
        {
            SortingEngine.Start(algoName, this);
        }

        #region Display Array
        internal void SetUp()
        {
            SortingEngine.SetUpArray((int)canvas.ActualHeight);
            SetUpProgressBar();
            AddRectanglesToCanvas();
        }
        #endregion

        #region Progress Bar
        private void SetUpProgressBar()
        {
            progressBar.Value = 0;
            progressBar.Maximum = SortingEngine.ArraySize;
        }

        internal void ReportProgress(int value)
        {
            progressBar.Value += value;
        }
        #endregion

        #region Rectangles
        private void AddRectanglesToCanvas()
        {
            canvas.Children.Clear();
            for (int i = 0; i < SortingEngine.RandomInts.Length; i++)
            {
                Rectangle rectangle = new Rectangle()
                {
                    Height = SortingEngine.RandomInts[i],
                    Width = canvas.ActualWidth / SortingEngine.RandomInts.Length,
                    Fill = new BrushConverter().ConvertFromString(GlobalColors.BackgroundColor) as SolidColorBrush,
                    StrokeThickness = 1,
                    Stroke = new BrushConverter().ConvertFromString(GlobalColors.StripsColor) as SolidColorBrush,
                    Tag = i,
                };

                canvas.Children.Add(rectangle);
                SetRecangleLeft(rectangle, i * rectangle.Width);
                SetRectangleTop(rectangle, canvas.ActualHeight - rectangle.Height);
            }
        }

        private void SetRectangleTop(Rectangle rectangle, double yPoint)
        {
            Canvas.SetTop(rectangle, yPoint);
        }

        private void SetRecangleLeft(Rectangle rectangle, double xPoint)
        {
            Canvas.SetLeft(rectangle, xPoint);
        }

        private List<Rectangle> GetRectangles(int tag, int tag2)
        {
            List<Rectangle> rectangles = canvas.Children.OfType<Rectangle>().Where(x => (int)x.Tag == tag || (int)x.Tag == tag2).ToList();
            return rectangles;
        }

        private void SwapRectanglesTag(int tag1, int tag2, Rectangle rectangle1, Rectangle rectangle2)
        {
            rectangle1.Tag = (int)rectangle1.Tag + tag1;
            rectangle2.Tag = (int)rectangle2.Tag + tag2;
        }

        private void FillRectangles(Rectangle rectangle1, Rectangle rectangle2)
        {
            rectangle1.Fill = new BrushConverter().ConvertFromString(GlobalColors.BigRectangleColor) as SolidColorBrush;
            rectangle2.Fill = new BrushConverter().ConvertFromString(GlobalColors.SmallRectangleColor) as SolidColorBrush;
        }

        private void SetOldRectanglesArray(Rectangle rectangle1, Rectangle rectangle2)
        {
            oldRectangles[0] = rectangle1;
            oldRectangles[1] = rectangle2;
        }

        private void SwapRectanglesPosition(int tag, int tag2, Rectangle rectangle1, Rectangle rectangle2)
        {
            SetRecangleLeft(rectangle1, rectangle1.Width * tag + rectangle1.Width);
            SetRecangleLeft(rectangle2, rectangle2.Width * tag2 - rectangle2.Width);
        }

        internal void UpdateRectangles(int tag, int tag2, int bound, int bound2)
        {
            ClearLastRectanglesColor();
            List<Rectangle> rectangles = GetRectangles(tag, tag2);
            SwapRectanglesTag(bound, bound2, rectangles[0], rectangles[1]);
            SwapRectanglesPosition(tag, tag2, rectangles[0], rectangles[1]);
            FillRectangles(rectangles[0], rectangles[1]);
            SetOldRectanglesArray(rectangles[0], rectangles[1]);
        }

        internal void ClearLastRectanglesColor()
        {
            foreach (var item in oldRectangles)
            {
                if (item == null)
                    break;

                item.Fill = new BrushConverter().ConvertFromString(GlobalColors.BackgroundColor) as SolidColorBrush;
            }
        }
        #endregion
    }
}
