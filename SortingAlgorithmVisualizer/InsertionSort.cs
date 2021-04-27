using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            int n = randomInts.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = randomInts[i];
                int j = i - 1;

                // Move elements of arr[0..i-1],
                // that are greater than key,
                // to one position ahead of
                // their current position
                while (j >= 0 && randomInts[j] > key)
                {
                    randomInts[j + 1] = randomInts[j];
                    j--;
                }
                randomInts[j + 1] = key;
            }
        }
    }
}