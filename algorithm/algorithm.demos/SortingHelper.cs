using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.demos
{
    public static class SortingHelper<T> where T : IComparable
    {
        public static void StraightInsertSort(T[] arr)
        {
        }

        public static void ShellSort(T[] arr)
        {
        }

        public static void BubbleSort(T[] arr)
        {
            int i, j;
            T temp;

            for (j = 1; j < arr.Length; j++)
            {
                for (i = 0; i < arr.Length - j; i++)
                {
                    if (arr[i].CompareTo(arr[i + 1]) > 0)
                    {
                        // 核心操作：交换两个元素
                        temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                    }
                }
            }
        }


        public static void BubbleSort_v2(T[] arr)
        {
            int i, j;
            T temp;
            bool isExchanged = true;

            for (j = 1; j < arr.Length && isExchanged; j++)
            {
                isExchanged = false;
                for (i = 0; i < arr.Length - j; i++)
                {
                    if (arr[i].CompareTo(arr[i + 1]) > 0)
                    {
                        // 核心操作：交换两个元素
                        temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        // 附加操作：改变标志
                        isExchanged = true;
                    }
                }
            }
        }

        public static void QuickSort(T[] arr)
        {
        }

        public static void SimpleSelectSort(T[] arr)
        {
        }

        public static void HeapSort(T[] arr)
        {
        }

        public static void MergeSort(T[] arr)
        {
        }
    }
}
