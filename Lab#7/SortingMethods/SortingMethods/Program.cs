﻿using System;
using System.Collections.Generic;
using System.IO;

namespace SortingMethods
{
    class Program
    {
        static void Swap(ref int first, ref int second)
        {
            int tmp = first;
            first = second;
            second = tmp;
        }
        static void SelectionSort(ref int[] array, int left, int right)
        {
            long numberOfComparisons = 0;
            long numberOfPermutations = 0;
            var start = DateTime.Now;
            for (int i = left; i < right; i++)
            {
                int min = i;
                for (int j = i + 1; j <= right; j++)
                {
                    numberOfComparisons++;
                    if (array[j] < array[min])
                    {
                        min = j;
                    }

                }
                numberOfPermutations++;
                Swap(ref array[i], ref array[min]);
            }
            var end = DateTime.Now;
            var interval = end - start;
            ShowResult(numberOfComparisons, numberOfPermutations, interval);
            WriteInfo(array);
        }
        static void InsertionSort(ref int[] array, int left, int right)
        {
            long numberOfComparisons = 0;
            long numberOfPermutations = 0;
            var start = DateTime.Now;

            for (int i = left + 1; i <= right; i++)
            {
                int j = i;
                int tmp = array[i];

                while (j > left && tmp < array[j - 1])
                {
                    numberOfComparisons++;
                    array[j] = array[j - 1];
                    j--;
                }

                array[j] = tmp;
                numberOfPermutations++;
            }

            var end = DateTime.Now;
            var interval = end - start;
            ShowResult(numberOfComparisons, numberOfPermutations, interval);
            WriteInfo(array);
        }
        static void BubbleSort(ref int[] array, int left, int right)
        {
            long numberOfComparisons = 0;
            long numberOfPermutations = 0;
            var start = DateTime.Now;
            var wasPermutation = false;

            for (int i = left; i < right; i++)
            {
                for (int j = right; j > i; j--)
                {
                    numberOfComparisons++;
                    if (array[j - 1] > array[j])
                    {
                        numberOfPermutations++;
                        Swap(ref array[j - 1], ref array[j]);
                        wasPermutation = true;
                    }
                }
                if (!wasPermutation)
                {
                    break;
                }
            }

            var end = DateTime.Now;
            var interval = end - start;
            ShowResult(numberOfComparisons, numberOfPermutations, interval);
            WriteInfo(array);
        }
        static void ShakerSort(ref int[] array, int left, int right)
        {
            long numberOfComparisons = 0;
            long numberOfPermutations = 0;
            var start = DateTime.Now;

            do
            {
                for (int i = left; i < right; i++)
                {
                    numberOfComparisons++;
                    if (array[i] > array[i + 1])
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        numberOfPermutations++;
                    }
                }

                right--;

                for (int i = right; i > left; i--)
                {
                    numberOfComparisons++;
                    if (array[i] < array[i - 1])
                    {
                        Swap(ref array[i], ref array[i - 1]);
                        numberOfPermutations++;
                    }
                }

                left++;
            }
            while (left <= right);

            var end = DateTime.Now;
            var interval = end - start;
            ShowResult(numberOfComparisons, numberOfPermutations, interval);
            WriteInfo(array);
        }
        static void ShellSort(ref int[] array, int left, int right)
        {
            long numberOfComparisons = 0;
            long numberOfPermutations = 0;
            var numbers = new List<int>();
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    double x = Math.Pow(2, i) * Math.Pow(3, j);
                    if (x < length / 2)
                    {
                        numbers.Add((int)x);   
                    }
                    else
                    {
                        break;
                    }
                }
                if ((int)Math.Pow(2, i) > length/2)
                {
                    break;
                }
            }
            int[] H = numbers.ToArray();
            Array.Sort(H);
            Array.Reverse(H);
            var start = DateTime.Now;
            foreach (int step in H)
            {
                for (int i = left + step; i <= right; i++)
                {
                    int j = i;
                    int tmp = array[i];

                    while (j >= left + step && tmp < array[j - step])
                    {
                        numberOfComparisons++;
                        numberOfPermutations++;
                        array[j] = array[j - step];
                        j -= step;
                    }
                    array[j] = tmp;
                    numberOfPermutations++;
                }
            }

            var end = DateTime.Now;
            var interval = end - start;
            ShowResult(numberOfComparisons, numberOfPermutations, interval);
            WriteInfo(array);
        }
        static void ShowResult(long comparisons, long permutations, TimeSpan interval)
        {
            Console.WriteLine($"Время работы - {interval.ToString(),21}\n"
                             + $"Количество сравнений:{comparisons,15}\n"
                             + $"Количество перестановок:{permutations,12}\n");
        }
        static void WriteInfo(int[] array)
        {
            string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).ToString()).ToString()).ToString() + "\\sorted.dat";
            using (var sw = new StreamWriter(path, append: true))
            {
                foreach (int x in array)
                {
                    sw.Write(x + " ");
                }
                sw.WriteLine();
            }
        }
        static void CheckSortedArray(int[] array)
        {
            int left = 0;
            int right = array.Length - 1;
            var numbers = new List<int>();
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    double x = Math.Pow(2, i) * Math.Pow(3, j);
                    if (x < length / 2)
                    {
                        numbers.Add((int)x);
                    }
                    else
                    {
                        break;
                    }
                }
                if ((int)Math.Pow(2, i) > length / 2)
                {
                    break;
                }
            }
            int[] H = numbers.ToArray();
            Array.Sort(H);
            Array.Reverse(H);
            bool isSorted = true;
            foreach (int step in H)
            {
                for (int i = left + step; i <= right; i++)
                {
                    int j = i;
                    int tmp = array[i];

                    while (j >= left + step && tmp < array[j - step])
                    {
                        isSorted = false;
                        break;
                    }
                    if (!isSorted)
                    {
                        Console.WriteLine("Массив не отсортирован");
                        break;
                    }
                    array[j] = tmp;
                }
                if (!isSorted)
                {
                    Console.WriteLine("Массив не отсортирован");
                    break;
                }
            }
            if (isSorted)
            {
                Console.WriteLine("Массив отсортирован");
            }
        }
        static int[] ConvertArray(string[] array)
        {
            var outArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                outArray[i] = Convert.ToInt32(array[i]);
            }
            return outArray;
        }

        static void Main(string[] args)
        {
            string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).ToString()).ToString()).ToString() + "\\sorted.dat";
            if (File.Exists(path))
            {
                File.Delete(path); 
            }
            var arrayLength = 100000;
            var randomArray = new int[arrayLength];
            var arrayToSort = new int[arrayLength];
            var rnd = new Random();
            for (int i = 0; i < randomArray.Length; i++)
            {
                randomArray[i] = rnd.Next(1000);
            }

            Array.Copy(randomArray, arrayToSort, randomArray.Length);
            #region SelectionSort
            Console.WriteLine("Сортировка выбором массива, заполненного случайными числами");
            SelectionSort(ref arrayToSort, 0, arrayToSort.Length - 1);

            Console.WriteLine("Сортировка выбором массива, заполненного числами в порядке возрастания");
            SelectionSort(ref arrayToSort, 0, arrayToSort.Length - 1);

            Array.Reverse(arrayToSort);
            Console.WriteLine("Сортировка выбором массива, заполненного числами в порядке убывания");
            SelectionSort(ref arrayToSort, 0, arrayToSort.Length - 1);
            #endregion


            Array.Copy(randomArray, arrayToSort, randomArray.Length);
            #region InsertionSort
            Console.WriteLine("Сортировка вставками массива, заполненного случайными числами");
            InsertionSort(ref arrayToSort, 0, arrayToSort.Length - 1);

            Console.WriteLine("Сортировка вставками массива, заполненного числами в порядке возрастания");
            InsertionSort(ref arrayToSort, 0, arrayToSort.Length - 1);

            Array.Reverse(arrayToSort);
            Console.WriteLine("Сортировка вставками массива, заполненного числами в порядке убывания");
            InsertionSort(ref arrayToSort, 0, arrayToSort.Length - 1);
            #endregion


            Array.Copy(randomArray, arrayToSort, randomArray.Length);
            #region BubbleSort
            Console.WriteLine("Сортировка пузырьком массива, заполненного случайными числами");
            BubbleSort(ref arrayToSort, 0, arrayToSort.Length - 1);

            Console.WriteLine("Сортировка пузырьком массива, заполненного числами в порядке возрастания");
            BubbleSort(ref arrayToSort, 0, arrayToSort.Length - 1);

            Array.Reverse(arrayToSort);
            Console.WriteLine("Сортировка пузырьком массива, заполненного числами в порядке убывания");
            BubbleSort(ref arrayToSort, 0, arrayToSort.Length - 1);
            #endregion


            Array.Copy(randomArray, arrayToSort, randomArray.Length);
            #region ShellSort
            Console.WriteLine("Сортировка массива алгоритмом Шелла, заполненного случайными числами");
            ShellSort(ref arrayToSort, 0, arrayToSort.Length - 1);

            Console.WriteLine("Сортировка массива алгоритмом Шелла, заполненного числами в порядке возрастания");
            ShellSort(ref arrayToSort, 0, arrayToSort.Length - 1);

            Array.Reverse(arrayToSort);
            Console.WriteLine("Сортировка массива алгоритмом Шелла, заполненного числами в порядке убывания");
            ShellSort(ref arrayToSort, 0, arrayToSort.Length - 1);
            #endregion

            Array.Copy(randomArray, arrayToSort, randomArray.Length);
            #region ShakerSort
            Console.WriteLine("Сортировка шейкером массива, заполненного случайными числами");
            ShakerSort(ref arrayToSort, 0, arrayToSort.Length - 1);

            Console.WriteLine("Сортировка шейкером массива, заполненного числами в порядке возрастания");
            ShakerSort(ref arrayToSort, 0, arrayToSort.Length - 1);

            Array.Reverse(arrayToSort);
            Console.WriteLine("Сортировка шейкером массива, заполненного числами в порядке убывания");
            ShakerSort(ref arrayToSort, 0, arrayToSort.Length - 1);
            #endregion

            #region CheckArray
            using (var sr = new StreamReader(path))
            {
                while (sr.Peek() != -1)
                {
                    CheckSortedArray(ConvertArray(sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)));
                }
            }
            #endregion
        }
    }
}
