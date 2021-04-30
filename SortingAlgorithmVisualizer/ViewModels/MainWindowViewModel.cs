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
        private readonly Canvas canvas;
        private readonly List<Rectangle> oldRectanglesList;
        private readonly ProgressBar progressBar;

        public SortingEngine SortingEngine { get; set; }

        public MainWindowViewModel(ComboBox comboBox, ProgressBar progressBar, Canvas canvas)
        {
            SortingEngine = new SortingEngine();
            oldRectanglesList = new List<Rectangle>();
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
            return canvas.Children.OfType<Rectangle>().Where(x => (int)x.Tag == tag || (int)x.Tag == tag2).ToList();
        }

        private void SwapRectanglesTag(int tag1, int tag2, Rectangle rectangle1, Rectangle rectangle2)
        {
            rectangle1.Tag = (int)rectangle1.Tag + tag1;
            rectangle2.Tag = (int)rectangle2.Tag + tag2;
        }

        private void FillRectangle(Rectangle rectangle, string colorString)
        {
            rectangle.Fill = new BrushConverter().ConvertFromString(colorString) as SolidColorBrush;
        }

        private void SetOldRectanglesArray(List<Rectangle> newRectangle)
        {
            oldRectanglesList.Clear();
            foreach (var item in newRectangle)
                oldRectanglesList.Add(item);
        }

        private void SwapRectanglesPosition(int tag, int tag2, Rectangle rectangle1, Rectangle rectangle2)
        {
            SetRecangleLeft(rectangle1, rectangle1.Width * tag + rectangle1.Width);
            SetRecangleLeft(rectangle2, rectangle2.Width * tag2 - rectangle2.Width);
        }

        internal void HandleBubbleSortDrawing(int tag, int tag2, int bound, int bound2)
        {
            ClearLastRectanglesColor();
            List<Rectangle> rectangles = GetRectangles(tag, tag2);
            SwapRectanglesTag(bound, bound2, rectangles[0], rectangles[1]);
            SwapRectanglesPosition(tag, tag2, rectangles[0], rectangles[1]);
            FillRectangle(rectangles[0], GlobalColors.BigRectangleColor);
            FillRectangle(rectangles[1], GlobalColors.SmallRectangleColor);
            SetOldRectanglesArray(rectangles);
        }

        internal void HandleInsertionSortDrawing(int tag, int tag2, int firstRectangleTag)
        {
            ClearLastRectanglesColor();
            List<Rectangle> rectangles = GetRectangles(tag, tag2);
            rectangles.Add(GetOneRectangle(firstRectangleTag));

            SwapRectanglesTag(1, -1, rectangles[0], rectangles[1]);
            SwapRectanglesPosition(tag, tag2, rectangles[0], rectangles[1]);
            FillRectangle(rectangles[2], GlobalColors.BigRectangleColor);
            FillRectangle(rectangles[1], GlobalColors.SmallRectangleColor);
            SetOldRectanglesArray(rectangles);
        }

        internal void HandleQuickSortAnimation(int[] randomInts, int start, int index, int end)
        {
            ClearLastRectanglesColor();

            int i = -1;
            foreach (var item in canvas.Children.Cast<Rectangle>())
            {
                i++;
                item.Height = randomInts[i];
                item.Width = canvas.ActualWidth / randomInts.Length;
                item.Fill = new BrushConverter().ConvertFromString(GlobalColors.BackgroundColor) as SolidColorBrush;
                item.StrokeThickness = 1;
                item.Stroke = new BrushConverter().ConvertFromString(GlobalColors.StripsColor) as SolidColorBrush;
                item.Tag = i;

                SetRecangleLeft(item, i * item.Width);
                SetRectangleTop(item, canvas.ActualHeight - item.Height);
            }

            List<Rectangle> rectangles = new List<Rectangle>
            {
                GetOneRectangle(start),
                GetOneRectangle(index),
                GetOneRectangle(end)
            };

            FillRectangle(rectangles[0], GlobalColors.BigRectangleColor);
            FillRectangle(rectangles[1], GlobalColors.SmallRectangleColor);
            FillRectangle(rectangles[2], GlobalColors.BigRectangleColor);

            SetOldRectanglesArray(rectangles);

        }

        private Rectangle GetOneRectangle(int tag)
        {
            return canvas.Children.OfType<Rectangle>().Where(x => (int)x.Tag == tag).FirstOrDefault();
        }

        internal void Test2(int currentIndex)
        {
            List<Rectangle> rectangle = canvas.Children.OfType<Rectangle>().Where(x => (int)x.Tag == currentIndex).ToList();

            rectangle[0].Fill = Brushes.Blue;
            MessageBox.Show("dd");
        }

        internal void ClearLastRectanglesColor()
        {
            foreach (var item in oldRectanglesList)
            {
                if (item == null)
                    break;

                item.Fill = new BrushConverter().ConvertFromString(GlobalColors.BackgroundColor) as SolidColorBrush;
            }
        }
        #endregion
    }
}
