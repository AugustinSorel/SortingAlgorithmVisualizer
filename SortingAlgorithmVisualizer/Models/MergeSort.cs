using System;
using System.Linq;
using System.Threading;
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
        }

        public int[] MergeSort1(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];
            //As this is a recursive algorithm, we need to have a base case to 
            //avoid an infinite recursion and therfore a stackoverflow
            if (array.Length <= 1)
                return array;
            // The exact midpoint of our array  
            int midPoint = array.Length / 2;
            //Will represent our 'left' array
            left = new int[midPoint];

            //if array has an even number of elements, the left and right array will have the same number of 
            //elements
            if (array.Length % 2 == 0)
                right = new int[midPoint];
            //if array has an odd number of elements, the right array will have one more element than left
            else
                right = new int[midPoint + 1];
            //populate left array
            for (int i = 0; i < midPoint; i++)
                left[i] = array[i];
            //populate right array   
            int x = 0;
            //We start our index from the midpoint, as we have already populated the left array from 0 to midpont

            for (int i = midPoint; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }
            //Recursively sort the left array

            left = MergeSort1(left);
            //Recursively sort the right array
            right = MergeSort1(right);
            //Merge our two sorted arrays
            result = Merge(left, right);

            DrawBar(0, 0, result);

            return result;
        }

        //This method will be responsible for combining our two sorted arrays into one giant array
        public int[] Merge(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];
            //
            int indexLeft = 0, indexRight = 0, indexResult = 0;
            //while either array still has an element
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                //if both arrays have elements  
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    //If item on left array is less than item on right array, add that item to the result array 
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;

                    }
                    // else the item in the right array wll be added to the results array
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                //if only the left array still has elements, add all its items to the results array
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                //if only the right array still has elements, add all its items to the results array
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }

        public void NextStep()
        {
            randomInts =  MergeSort1(randomInts);
            //Swap(i, i + 1);
            //Thread.Sleep(10);
        }

        private void DrawBar(int midPoint, int x, int[] array)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                //mainWindowViewModel.HandleMergeSortDrawing(array, midPoint, x);
            }));
            Thread.Sleep(10);
        }


        public bool IsSorted()
        {
            for (int i = 0; i < randomInts.Count() - 1; i++)
                if (randomInts[i] > randomInts[i + 1])
                    return false;

            foreach (var item in randomInts)
            {
//                MessageBox.Show(item.ToString());
            }

            return true;
        }
    }
}