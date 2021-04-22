using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SortingAlgorithmVisualizer
{
    class BubbleSort : ISortAlo
    {
        private int[] arrayInt;

        public BubbleSort(int[] arrayInt)
        {
            this.arrayInt = arrayInt;
        }

        public bool IsSorted()
        {
            for (int i = 0; i < arrayInt.Count() - 1; i++)
                if (arrayInt[i] > arrayInt[i + 1])
                    return false;

            return true;
        }

        public void NextStep()
        {
            for (int i = 0; i < arrayInt.Count() - 1; i++)
                if (arrayInt[i] > arrayInt[i + 1])
                    Swap(i, i + 1);

        }

        private void Swap(int i, int v)
        {
            int temp = arrayInt[i];
            arrayInt[i] = arrayInt[i + 1];
            arrayInt[i + 1] = temp;

            DrawBar(i, arrayInt[i]);
            DrawBar(v, arrayInt[v]);
        }

        private async void DrawBar(int position, int height)
        {
            MainWindow mainWindow = Application.Current.Windows[0] as MainWindow;
            mainWindow.canvas.Children.Clear();

            for (int i = 0; i < arrayInt.Length; i++)
            {
                Rectangle rectangle = new Rectangle()
                {
                    Height = arrayInt[i],
                    Width = mainWindow.canvas.ActualWidth / arrayInt.Length,
                    Fill = new BrushConverter().ConvertFromString(GlobalColors.BackgroundColor) as SolidColorBrush,
                    StrokeThickness = 1,
                    Stroke = new BrushConverter().ConvertFromString(GlobalColors.StripsColor) as SolidColorBrush,
                };

                mainWindow.canvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, i * rectangle.Width);
                Canvas.SetTop(rectangle, mainWindow.canvas.ActualHeight - rectangle.Height);

                await Task.Delay(1000);
            }
        }

        public void ReDraw()
        {

        }
    }
}