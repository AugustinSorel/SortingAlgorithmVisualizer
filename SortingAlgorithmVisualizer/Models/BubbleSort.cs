using System.Collections.Generic;
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
            int temp = arrayInt[i + 1];
            arrayInt[i+1] = arrayInt[i];
            arrayInt[i] = temp;

            DrawBar(i, v);
        }

        private MainWindow mainWindow = Application.Current.Windows[0] as MainWindow;

        private void DrawBar(int tag, int tag2)
        {
            List<Rectangle> collection = collection = mainWindow.canvas.Children.OfType<Rectangle>().Where(x => (int)x.Tag == tag || (int)x.Tag == tag2).ToList();

            Canvas.SetLeft(collection[0], collection[0].Width * tag + collection[0].Width);
            Canvas.SetLeft(collection[1], collection[1].Width * tag2 - collection[1].Width);

            collection[0].Tag = (int)collection[0].Tag + 1;
            collection[1].Tag = (int)collection[1].Tag - 1;

            collection[0].Fill = Brushes.Red;
            collection[1].Fill = Brushes.Blue;
        }
    }
}