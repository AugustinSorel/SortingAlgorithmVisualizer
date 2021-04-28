using System;
using System.Linq;
using System.Threading;
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

            return true;
        }

        int currentIndex = 0;
        public void NextStep()
        {
            int temp = randomInts[currentIndex];

            while (currentIndex > 0 && randomInts[currentIndex - 1] > temp)
            {
                Swap();
                Thread.Sleep(10);
            }
            randomInts[currentIndex] = temp;
            currentIndex++;
        }

        private void Swap()
        {
            DrawBar(currentIndex - 1, currentIndex);
            randomInts[currentIndex] = randomInts[currentIndex - 1];
            currentIndex--;
        }

        private void DrawBar(int tag, int tag2)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                mainWindowViewModel.UpdateRectangles(tag, tag2, 1, -1);
            }));
        }
    }
}