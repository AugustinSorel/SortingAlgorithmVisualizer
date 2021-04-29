using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SortingAlgorithmVisualizer
{
    class QuickSort : ISortAlo
    {
        private int[] randomInts;
        private readonly MainWindowViewModel mainWindowViewModel;

        public QuickSort(MainWindowViewModel mainWindowViewModel, int[] randomInts)
        {
            this.randomInts = randomInts;
            this.mainWindowViewModel = mainWindowViewModel;

        }

        private void QuickSort1(int[] arr, int start, int end)
        {
            if (start >= end)
                return;

            var index = Partition(arr, start, end);
            QuickSort1(arr, start, index - 1);
            QuickSort1(arr, index + 1, end);
        }



        public void NextStep()
        {
            QuickSort1(randomInts, 0, randomInts.Length - 1);
        }

        public bool IsSorted()
        {
            return true;
        }
    }
}
