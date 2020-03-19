using System;
using System.Threading;

namespace algorithm.demos
{
    class Program
    {
        private object func_1_obj;
        private object func_2_obj;
        static void Main(string[] args)
        {
            

        }


        public static void quickSort(int[] arr, int L, int R)
        {
            int i = L;
            int j = R;

            int pivot = arr[(L + R) / 2];

            while (i <= j)
            {

                while (pivot > arr[i])
                    i++;

                while (pivot < arr[j])
                    j--;

                if (i <= j)
                {
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    i++;
                    j--;
                }
            }
            if (L < j)
                quickSort(arr, L, j);

            if (i < R)
                quickSort(arr, i, R);
        }
    }
}
