using System;
using System.Windows;

namespace SortingAlgorithmVisualizer
{
    class MergeSort : ISortAlo
    {
        private readonly int[] arrayInt;

        public MergeSort(int[] arrayInt)
        {
            MessageBox.Show(this.GetType().Name);
            this.arrayInt = arrayInt;
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