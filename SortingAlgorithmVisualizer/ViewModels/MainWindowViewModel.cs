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

        internal void SetRectangleTop(Rectangle rectangle, double yPoint)
        {
            Canvas.SetTop(rectangle, yPoint);
        }

        internal void SetRecangleLeft(Rectangle rectangle, double xPoint)
        {
            Canvas.SetLeft(rectangle, xPoint);
        }

        internal List<Rectangle> GetRectangles(int tag, int tag2)
        {
            List<Rectangle> rectangles = canvas.Children.OfType<Rectangle>().Where(x => (int)x.Tag == tag || (int)x.Tag == tag2).ToList();
            return rectangles;
        }

        internal void SwapRectanglesTag(int tag1, int tag2, Rectangle rectangle1, Rectangle rectangle2)
        {
            rectangle1.Tag = (int)rectangle1.Tag + tag1;
            rectangle2.Tag = (int)rectangle2.Tag + tag2;
        }

        internal void FillRectangles(Rectangle rectangle1, Rectangle rectangle2)
        {
            rectangle1.Fill = new BrushConverter().ConvertFromString(GlobalColors.BigRectangleColor) as SolidColorBrush;
            rectangle2.Fill = new BrushConverter().ConvertFromString(GlobalColors.SmallRectangleColor) as SolidColorBrush;
        }

        internal void SetOldRectanglesArray(Rectangle rectangle1, Rectangle rectangle2)
        {
            oldRectangles[0] = rectangle1;
            oldRectangles[1] = rectangle2;
        }

        internal void SwapRectanglesPosition(int tag, int tag2, Rectangle rectangle1, Rectangle rectangle2)
        {
            SetRecangleLeft(rectangle1, rectangle1.Width * tag + rectangle1.Width);
            SetRecangleLeft(rectangle2, rectangle2.Width * tag2 - rectangle2.Width);
        }

        internal void Test(int tag, int tag2)
        {
            ClearLastRectanglesColor();

            List<Rectangle> rectangles = canvas.Children.OfType<Rectangle>().Where(x => (int)x.Tag == tag || (int)x.Tag == tag2).ToList();

            Canvas.SetLeft(rectangles[0], rectangles[0].Width * tag + rectangles[0].Width);
            Canvas.SetLeft(rectangles[1], rectangles[1].Width * tag2 - rectangles[1].Width);

            rectangles[0].Tag = (int)rectangles[0].Tag + 1;
            rectangles[1].Tag = (int)rectangles[1].Tag - 1;

            rectangles[0].Fill = new BrushConverter().ConvertFromString(GlobalColors.BigRectangleColor) as SolidColorBrush;
            rectangles[1].Fill = new BrushConverter().ConvertFromString(GlobalColors.SmallRectangleColor) as SolidColorBrush;

            oldRectangles[0] = rectangles[0];
            oldRectangles[1] = rectangles[1];
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
