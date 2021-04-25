using System;
using System.Linq;
using System.Threading;
using System.Windows;

namespace SortingAlgorithmVisualizer
{
    class BubbleSort : ISortAlo
    {
        private int[] randomInts;
        private readonly MainWindowViewModel mainWindowViewModel;

        public BubbleSort(MainWindowViewModel mainWindowViewModel, int[] randomInts)
        {
            this.randomInts = randomInts;
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public bool IsSorted()
        {
            for (int i = 0; i < randomInts.Count() - 1; i++)
                if (randomInts[i] > randomInts[i + 1])
                    return false;

            return true;
        }

        public void NextStep()
        {
            for (int i = 0; i < randomInts.Count() - 1; i++)
                if (randomInts[i] > randomInts[i + 1])
                {
                    Swap(i, i + 1);
                    Thread.Sleep(10);
                }
        }

        private void Swap(int i, int v)
        {
            int temp = randomInts[i + 1];
            randomInts[i+1] = randomInts[i];
            randomInts[i] = temp;

            DrawBar(i, v);
        }
        
        private void DrawBar(int tag, int tag2)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                mainWindowViewModel.DrawRectangles(tag, tag2);
            }));
        }
    }
}