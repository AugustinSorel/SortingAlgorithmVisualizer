using System;
using System.Windows;

namespace SortingAlgorithmVisualizer
{
    class MergeSort : ISortAlo
    {
        public MergeSort()
        {
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