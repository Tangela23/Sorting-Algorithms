using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoritmosDeOrdenamiento
{
    public class Algoritmos<T>
    {
        private Comparer<T> comp = Comparer<T>.Default;

        public bool BinarySearch(T[] data, int l, int r, T target)
        {
            ShellSort(data, 0, data.Length);


            if (comp.Compare(data[(r + l) / 2], target) == 0)
                return true;
            else if (r <= l || (r - l) == 1)
                return false;
            else if (comp.Compare(data[(r + l) / 2], target) == 1)
            {
                if (BinarySearch(data, l, (r + l) / 2, target) == true) return true; else return false;
            }
            else if (comp.Compare(data[(r + l) / 2], target) == -1)
            {
                if (BinarySearch(data, (r + l) / 2, r, target) == true) return true; else return false;
            }
            return false;
        }

        public bool LinearSearch(T[] data, T target)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (comp.Compare(data[i], target) == 0)
                    return true;
            }
            return false;
        }

        public bool LinearInformedSearch(T[] data, T target)
        {
            ShellSort(data, 0, data.Length - 1);

            if (comp.Compare(data[data.Length / 2], target) == 1)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (comp.Compare(data[i], target) == 0)
                        return true;
                    if (comp.Compare(data[i], target) == 1)
                        return false;
                }
            }
            else
            {
                for (int i = (data.Length / 2); i < data.Length; i++)
                {
                    if (comp.Compare(data[i], target) == 0)
                        return true;
                    if (comp.Compare(data[i], target) == 1)
                        return false;
                }
            }
            return false;
        }

        public T[] ShellSort(T[] a, int l, int r)
        {
            int j = l, h;
            T temp;

            // Check if it is sorted
            while (j < (r - 1))
            {
                if (comp.Compare(a[j], a[j + 1]) <= 0)
                    j++;
                else
                    break;
            }
            if (j == (r - 1))
                return a;

            for (h = l; h < r; h = h * 3 + 1) ;

            while (h > 0)
            {
                for (int i = h; i < r; i++)
                {
                    j = i;
                    temp = a[i];
                    while ((j >= h) && (comp.Compare(a[j - h], temp) == 1))
                    {
                        a[j] = a[j - h];
                        j = j - h;
                    }
                    a[j] = temp;
                }
                h /= 2;
            }
            return a;
        }
    }
}
