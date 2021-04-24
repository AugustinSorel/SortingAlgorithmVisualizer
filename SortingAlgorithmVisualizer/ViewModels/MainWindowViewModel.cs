using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SortingAlgorithmVisualizer
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private int[] randomInts;
        private int arraySize;
        private Canvas canvas;

        public int ArraySize
        {
            get { return arraySize; }
            set
            {
                if (value != arraySize && value > 0 && value < 101)
                {
                    arraySize = value;
                    NotifyPropertyChanged("ArraySize");
                }
            }
        }

        public int[] RandomInts
        {
            get { return randomInts; }
            set { randomInts = value; }
        }


        public MainWindowViewModel(ComboBox comboBox)
        {
            arraySize = 10;
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

        internal void DisplayArray(Canvas canvas)
        {
            this.canvas = canvas;
            SetUpArray();
            AddRectanglesToCanvas();
        }

        private void SetUpArray()
        {
            randomInts = new int[arraySize];
            Random random = new Random();
            for (int i = 0; i < randomInts.Length; i++)
                randomInts[i] = random.Next(0, (int)canvas.ActualHeight);
        }

        private void AddRectanglesToCanvas()
        {
            canvas.Children.Clear();
            for (int i = 0; i < randomInts.Length; i++)
            {
                Rectangle rectangle = new Rectangle()
                {
                    Height = randomInts[i],
                    Width = canvas.ActualWidth / randomInts.Length,
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
            List<Rectangle> rectangles = rectangles = canvas.Children.OfType<Rectangle>().Where(x => (int)x.Tag == tag || (int)x.Tag == tag2).ToList();

            Canvas.SetLeft(rectangles[0], rectangles[0].Width * tag + rectangles[0].Width);
            Canvas.SetLeft(rectangles[1], rectangles[1].Width * tag2 - rectangles[1].Width);

            rectangles[0].Tag = (int)rectangles[0].Tag + 1;
            rectangles[1].Tag = (int)rectangles[1].Tag - 1;

            //collection[0].Fill = Brushes.Red;
            //collection[1].Fill = Brushes.Blue;
        }

        #region Property Changed Event Handler
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
