using System;
using System.Collections;
using System.Collections.Generic;
using Assn2;

namespace Assn2
{
    public class CustomHashTable<TKey, TValue>
    {
        private const double LoadFactorThreshold = 0.75;
        private const int InitialCapacity = 10;

        private Entry<TKey, TValue>[] table;
        private int size;

        public CustomHashTable()
        {
            table = new Entry<TKey, TValue>[InitialCapacity];
            size = 0;
        }

        public void Insert(TKey key, TValue value)
        {
            if ((double)size / table.Length >= LoadFactorThreshold)
            {
                ResizeTable();
            }

            int index = GetHashIndex(key);
            while (table[index] != null && !table[index].IsDeleted)
            {
                if (table[index].Key.Equals(key))
                {
                    // Key already exists, update the value
                    table[index].Value = value;
                    return;
                }
                index = (index + DoubleHash(key)) % table.Length;
            }

            table[index] = new Entry<TKey, TValue>(key, value);
            size++;
        }

        public bool Remove(TKey key)
        {
            int index = FindIndex(key);
            if (index != -1)
            {
                table[index].IsDeleted = true;
                size--;
                return true;
            }
            return false;
        }

        public TValue Get(TKey key)
        {
            int index = FindIndex(key);
            if (index != -1)
            {
                return table[index].Value;
            }
            throw new KeyNotFoundException("Key not found in hash table.");
        }

        public void PrintEntries()
        {
            foreach (var entry in table)
            {
                if (entry != null && !entry.IsDeleted)
                {
                    Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
                }
                else
                {
                    Console.WriteLine("-");
                }
            }
        }


        private int FindIndex(TKey key)
        {
            int index = GetHashIndex(key);
            int originalIndex = index;
            while (table[index] != null)
            {
                if (!table[index].IsDeleted && table[index].Key.Equals(key))
                {
                    return index;
                }
                index = (index + DoubleHash(key)) % table.Length;
                if (index == originalIndex)
                {
                    break;
                }
            }
            return -1;
        }

        private void ResizeTable()
        {
            int newCapacity = table.Length * 2;
            Entry<TKey, TValue>[] newTable = new Entry<TKey, TValue>[newCapacity];
            foreach (var entry in table)
            {
                if (entry != null && !entry.IsDeleted)
                {
                    int index = GetHashIndex(entry.Key, newCapacity);
                    while (newTable[index] != null)
                    {
                        index = (index + DoubleHash(entry.Key)) % newCapacity;
                    }
                    newTable[index] = entry;
                }
            }
            table = newTable;
        }

        private int GetHashIndex(TKey key, int capacity = -1)
        {
            int hash = key.GetHashCode();
            if (capacity == -1)
            {
                capacity = table.Length;
            }
            return (hash % capacity + capacity) % capacity; // Ensure positive index
        }

        private int DoubleHash(TKey key)
        {
            // Was unable to find a way to stop collisions through method two
            // Hence, linear probing
            return 1;
        }

        private class Entry<TKey, TValue> : IEnumerable<Entry<TKey, TValue>>
        {
            public TKey Key { get; }
            public TValue Value { get; set; }
            public bool IsDeleted { get; set; }

            public Entry(TKey key, TValue value)
            {
                Key = key;
                Value = value;
                IsDeleted = false;
            }

            public IEnumerator<Entry<TKey, TValue>> GetEnumerator()
            {
                yield return this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}