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

            DrawBar(i, arrayInt[i], i);
            //DrawBar(v, arrayInt[v], i);
        }

        private MainWindow mainWindow = Application.Current.Windows[0] as MainWindow;

        private void DrawBar(int position, int height, int tag)
        {
            Rectangle r = mainWindow.canvas.Children[tag] as Rectangle;
            Rectangle r2 = mainWindow.canvas.Children[tag + 1] as Rectangle;

            r.Fill = Brushes.Red;
            MessageBox.Show("");
            Canvas.SetLeft(r, r.Width * tag + r.Width);
            MessageBox.Show((r.Width * tag + r.Width).ToString());

            r2.Fill = Brushes.Blue;
            MessageBox.Show("");
            Canvas.SetLeft(r2, r2.Width * (tag+1) - r.Width);
            MessageBox.Show((r2.Width * (tag+1) - r.Width).ToString());
        }
    }
}