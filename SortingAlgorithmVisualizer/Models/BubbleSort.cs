using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SortingAlgorithmVisualizer
{
    class BubbleSort : ISortAlo
    {
        private int[] RandomInts;
        private readonly MainWindowViewModel mainWindowViewModel;

        public BubbleSort(MainWindowViewModel mainWindowViewModel)
        {
            this.RandomInts = mainWindowViewModel.RandomInts;
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public bool IsSorted()
        {
            for (int i = 0; i < RandomInts.Count() - 1; i++)
                if (RandomInts[i] > RandomInts[i + 1])
                    return false;

            return true;
        }

        public void NextStep()
        {
            for (int i = 0; i < RandomInts.Count() - 1; i++)
                if (RandomInts[i] > RandomInts[i + 1])
                    Swap(i, i + 1);
        }

        private void Swap(int i, int v)
        {
            int temp = RandomInts[i + 1];
            RandomInts[i+1] = RandomInts[i];
            RandomInts[i] = temp;

            DrawBar(i, v);
        }
        
        private void DrawBar(int tag, int tag2)
        {
            mainWindowViewModel.DrawRectangles(tag, tag2);
        }
    }
}