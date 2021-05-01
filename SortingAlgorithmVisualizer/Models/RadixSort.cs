using System;
using System.Linq;
using System.Threading;
using System.Windows;

namespace SortingAlgorithmVisualizer
{
    class RadixSort : ISortAlo
    {
        private int[] randomInts;
        private readonly MainWindowViewModel mainWindowViewModel;

        public RadixSort(MainWindowViewModel mainWindowViewModel, int[] randomInts)
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
            int n = randomInts.Length;
            // Find the maximum number to know number of digits
            int m = getMax(randomInts, n);

            // Do counting sort for every digit. Note that
            // instead of passing digit number, exp is passed.
            // exp is 10^i where i is current digit number
            for (int exp = 1; m / exp > 0; exp *= 10)
                countSort(n, exp);
        }

        public int getMax(int[] arr, int n)
        {
            int mx = arr[0];
            for (int i = 1; i < n; i++)
                if (arr[i] > mx)
                    mx = arr[i];
            return mx;
        }

        // A function to do counting sort of arr[] according to
        // the digit represented by exp.
        public void countSort(int n, int exp)
        {
            int[] output = new int[n]; // output array
            int i;
            int[] count = new int[10];

            // initializing all elements of count to 0
            for (i = 0; i < 10; i++)
                count[i] = 0;

            // Store count of occurrences in count[]
            for (i = 0; i < n; i++)
                count[(randomInts[i] / exp) % 10]++;

            // Change count[i] so that count[i] now contains
            // actual
            //  position of this digit in output[]
            for (i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Build the output array
            for (i = n - 1; i >= 0; i--)
            {
                output[count[(randomInts[i] / exp) % 10] - 1] = randomInts[i];
                Draw(output, count[randomInts[i] / exp % 10] - 1, randomInts);
                Thread.Sleep(10);
                count[(randomInts[i] / exp) % 10]--;
            }
            
            // Copy the output array to arr[], so that arr[] now
            // contains sorted numbers according to current
            // digit
            for (i = 0; i < n; i++)
            {
                randomInts[i] = output[i];
            }
        }

        private void Draw(int[] output, int v, int[] exp)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                mainWindowViewModel.HandleRadixSortDrawing(output, v, exp);
            }));
        }
    }
}
