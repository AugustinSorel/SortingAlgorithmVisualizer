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
        public SortingEngine SortingEngine { get; set; }

        public MainWindowViewModel(ComboBox comboBox)
        {
            SortingEngine = new SortingEngine();
            PopulateComboBox(comboBox);
        }

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

        internal void Start(string algoName)
        {
            SortingEngine.Start(algoName, this);
        }

        internal void DisplayArray(Canvas canvas)
        {
            this.canvas = canvas;
            SortingEngine.SetUpArray(canvas);
            AddRectanglesToCanvas();
        }

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
            List<Rectangle> rectangles = canvas.Children.OfType<Rectangle>().Where(x => (int)x.Tag == tag || (int)x.Tag == tag2).ToList();

            Canvas.SetLeft(rectangles[0], rectangles[0].Width * tag + rectangles[0].Width);
            Canvas.SetLeft(rectangles[1], rectangles[1].Width * tag2 - rectangles[1].Width);

            rectangles[0].Tag = (int)rectangles[0].Tag + 1;
            rectangles[1].Tag = (int)rectangles[1].Tag - 1;

            //collection[0].Fill = Brushes.Red;
            //collection[1].Fill = Brushes.Blue;
        }
    }
}
