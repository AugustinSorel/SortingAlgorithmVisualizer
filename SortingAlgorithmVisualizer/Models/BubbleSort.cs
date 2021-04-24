﻿using System.Linq;
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
                {
                    MessageBox.Show("Start");
                    Swap(i, i + 1);
                }
        }

        private void Swap(int i, int v)
        {
            int temp = arrayInt[i + 1];
            arrayInt[i+1] = arrayInt[i];
            arrayInt[i] = temp;

            DrawBar(i, v);
        }

        private MainWindow mainWindow = Application.Current.Windows[0] as MainWindow;

        private void DrawBar(int index, int index2)
        {
            // USE TAG INSTEAD.
            Rectangle r = mainWindow.canvas.Children[index] as Rectangle;
            Rectangle r2 = mainWindow.canvas.Children[index2] as Rectangle;

            r.Fill = Brushes.Red;
            r2.Fill = Brushes.Blue;
            MessageBox.Show("index: " + index.ToString());

            Canvas.SetLeft(r, r.Width * index + r.Width);
            MessageBox.Show((r.Width * index + r.Width).ToString());

            Canvas.SetLeft(r2, r2.Width * index2 - r.Width);
            MessageBox.Show((r2.Width * index2 - r.Width).ToString());

            r.Fill = Brushes.Black;
            r2.Fill = Brushes.Black;
        }
    }
}