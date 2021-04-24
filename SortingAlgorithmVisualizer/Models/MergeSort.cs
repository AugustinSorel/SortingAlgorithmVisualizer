using System;
using System.Windows;

namespace SortingAlgorithmVisualizer
{
    class MergeSort : ISortAlo
    {
        private int[] randomInts;
        private readonly MainWindowViewModel mainWindowViewModel;

        public MergeSort(MainWindowViewModel mainWindowViewModel, int[] randomInts)
        {
            this.randomInts = randomInts;
            this.mainWindowViewModel = mainWindowViewModel;
            MessageBox.Show(this.GetType().Name);
        }

        public void NextStep()
        {
            throw new NotImplementedException();
        }

        public void ReDraw()
        {
            throw new NotImplementedException();
        }

        public bool IsSorted()
        {
            throw new NotImplementedException();
        }
    }
}