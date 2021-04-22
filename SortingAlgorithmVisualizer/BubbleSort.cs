using System.Windows;

namespace SortingAlgorithmVisualizer
{
    class BubbleSort : ISortAlo
    {
        public BubbleSort()
        {
            MessageBox.Show(this.GetType().Name);
        }

        public bool IsSorted()
        {
            return false;
        }

        public void NextStep()
        {
            MessageBox.Show("Next set");
        }

        public void ReDraw()
        {

        }
    }
}