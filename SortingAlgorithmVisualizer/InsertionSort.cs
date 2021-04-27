using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SortingAlgorithmVisualizer
{
    class InsertionSort : ISortAlo
    {
        private readonly MainWindowViewModel mainWindowViewModel;
        private readonly int[] randomInts;

        public InsertionSort(MainWindowViewModel mainWindowViewModel, int[] randomInts)
        {
            this.randomInts = randomInts;
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public bool IsSorted()
        {
            for (int i = 0; i < randomInts.Count() - 1; i++)
                if (randomInts[i] > randomInts[i + 1])
                    return false;

            foreach (var item in randomInts)
            {
                MessageBox.Show(item.ToString());
            }

            return true;
        }

        public void NextStep()
        {
            for (int i = 0; i < randomInts.Count(); i++)
            {
                var item = randomInts[i];
                var currentIndex = i;

                while (currentIndex > 0 && randomInts[currentIndex - 1] > item)
                {
                    DrawBar(currentIndex - 1, currentIndex);
                    Thread.Sleep(100);
                    randomInts[currentIndex] = randomInts[currentIndex - 1];
                    currentIndex--;
                }
                randomInts[currentIndex] = item;
            }
        }

        private void Swap(int i, int v)
        {
            DrawBar(i, v);
        }

        private void DrawBar(int tag, int tag2)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                mainWindowViewModel.Test(tag, tag2);
            }));
        }
    }
}