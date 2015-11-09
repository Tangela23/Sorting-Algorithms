using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoritmosDeOrdenamiento
{
    public class TablaHash<T>
    {
        public int Count = 0, Capacity = 2000000, LoadFactor = 0, NumberOfHashing = 0;
        private Comparer<T> comp = Comparer<T>.Default;
        T[] data = new T[2000000];

        public void Add(T toAdd)
        {
            // Keep the useage of the hash table below 72 percent
            if (LoadFactor > 72)
                IncreaseTable();

            // Calculate the hash key for the given element
            int index = toAdd.GetHashCode();

            if (index >= Capacity)
                index = index % Capacity;

            // Check for something at this position
            int n = 0;
            int PossibleHashings = 0;
            while (n <= Capacity)
            {
                if (data[index] != null)
                {
                    index = reHash(index);
                    PossibleHashings++;
                }
                else
                {
                    data[index] = toAdd;
                    Count++;
                    if (PossibleHashings > NumberOfHashing)
                        NumberOfHashing = PossibleHashings;
                    return;
                }
            } // End of while
        } // End of Add

        public void Remove(T auto2remove)
        {
            int index = auto2remove.GetHashCode();

            while (data[index] != null)
            {
                // Object found, removing and searching for next rehashed objects
                if (comp.Compare(auto2remove, data[index]) == 0)
                {
                    int saveRemovedIndex = index;
                    Console.WriteLine("Object Successfully removed");
                    Count--;

                    int forReindex = reHash(index);
                    int i = 0;
                    while ((data[forReindex] != null) && i < NumberOfHashing)
                    {
                        if (data[forReindex].GetHashCode() == data[index].GetHashCode())
                        {
                            Add(data[forReindex]);
                        }
                        index = forReindex;
                        forReindex = reHash(forReindex);
                    }

                    data[index] = default(T);
                    return;
                }
                else
                    index = reHash(index);
            }
            Console.WriteLine("Object not found");
            return;
        } // End of Remove

        public int reHash(int toRehash)
        {
            toRehash = toRehash + 1527;
            return toRehash;
        }

        public String getOnIndex(int _hashCode)
        {
            return data[_hashCode].ToString();
        }

        public Boolean find(T object2search)
        {
            int index = object2search.GetHashCode();
            for (int i = 0; i <= NumberOfHashing; i++)
            {
                if (comp.Compare(object2search, data[index]) == 0)
                    return true;
                else index = reHash(index);
            }
            return false;
        }

        public String getLoadFactor()
        {
            float factor = (Count * 100) / Capacity;
            String lFActor = factor + "% of the hash table is used";
            return lFActor;
        }

        public void IncreaseTable()
        {
            int oldCapacity = Capacity;
            Capacity = oldCapacity * 2;
            Count = 0;
            T[] tmpData = new T[oldCapacity];
            T[] newData = new T[Capacity];
            tmpData = data;
            data = newData;
            for (int i = 0; i < oldCapacity; i++)
            {
                if (tmpData[i] != null)
                    this.Add(tmpData[i]);
            }
        }
    }
}
