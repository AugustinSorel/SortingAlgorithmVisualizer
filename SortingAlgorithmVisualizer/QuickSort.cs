using System.Linq;
using System.Threading;
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

            int index = Partition(arr, start, end);

            Draw(start, index, end);
            Thread.Sleep(100);
            
            QuickSort1(arr, start, index - 1);
            QuickSort1(arr, index + 1, end);
        }

        private void Draw(int start, int index, int end)
        {
            Application.Current.Dispatcher.Invoke(new System.Action(() => {
                mainWindowViewModel.HandleQuickSortAnimation(randomInts, start, index, end);
                
            }));
        }

        private int Partition(int[] arr, int start, int end)
        {
            int pivotIndex = start;
            int pivotValue = arr[end];
            for (int i = start; i < end; i++)
            {
                if (arr[i] < pivotValue)
                {
                    Swap(arr, i, pivotIndex);
                    pivotIndex++;
                }
            }
            Swap(arr, pivotIndex, end);

            return pivotIndex;
        }

        private void Swap(int[] arr, int i, int pivotIndex)
        {
            int temp = arr[i];
            arr[i] = arr[pivotIndex];
            arr[pivotIndex] = temp;
        }

        public void NextStep()
        {
            QuickSort1(randomInts, 0, randomInts.Length - 1);
        }

        public bool IsSorted()
        {
            for (int i = 0; i < randomInts.Count() - 1; i++)
                if (randomInts[i] > randomInts[i + 1])
                    return false;

            return true;
        }
    }
}
