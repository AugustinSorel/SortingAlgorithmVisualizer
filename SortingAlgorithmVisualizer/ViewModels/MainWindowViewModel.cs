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

        public MainWindowViewModel(ComboBox comboBox, ProgressBar progressBar)
        {
            SortingEngine = new SortingEngine();
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

        internal void Start(string algoName)
        {
            SortingEngine.Start(algoName, this);
        }

        #region Display Array
        internal void DisplayArray(Canvas canvas)
        {
            this.canvas = canvas;
            SortingEngine.SetUpArray(canvas);
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

        internal void ReportProgress()
        {
            progressBar.Value += 1;
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
                Canvas.SetLeft(rectangle, i * rectangle.Width);
                Canvas.SetTop(rectangle, canvas.ActualHeight - rectangle.Height);
            }
        }

        internal void DrawRectangles(int tag, int tag2)
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
