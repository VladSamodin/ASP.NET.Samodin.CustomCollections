using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollections
{
    public class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private Container[] items;
        private EqualityComparer<TKey> equalityComparer;
        private Func<TKey, int> hashFunction;
        public int Capacity { get; private set; }
        public HashTable(int capacity = 100, Func<TKey, int> hashFunction = null, EqualityComparer<TKey> equalityComparer = null)
        {
            if (capacity < 10)
            {
                throw new ArgumentOutOfRangeException("capacity", "Capacity must be >= 10");
            }
            Capacity = capacity;
            items = new Container[Capacity];
            this.hashFunction = hashFunction ?? DefaultHashFunction;
            this.equalityComparer = equalityComparer ?? EqualityComparer<TKey>.Default;
        }

        public void Add(TKey key, TValue value)
        {
            int index = Math.Abs(hashFunction(key)) % Capacity;
            if (Collision(items[index], key))
            {
                throw new ArgumentException("This key already exists in table", "key");
            }
            AddNode(ref items[index], new Container(key, value));      
        }

        public TValue Get(TKey key)
        {
            int index = Math.Abs(hashFunction(key)) % Capacity;
            Container current = items[index];
            while (current != null && !equalityComparer.Equals(current.Key, key))
            {
                current = current.Next;
            }
            if (current == null)
            {
                throw new ArgumentException("This key doesn't exist", "key");
            }
            return current.Value;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < Capacity; i++)
            {
                Container current = items[i];
                while (current != null)
                {
                    yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private void AddNode(ref Container head, Container toAdd)
        {
            toAdd.Next = head;
            head = toAdd;
        }

        private bool Collision(Container head, TKey key)
        {
            Container current = head;
            while (current != null)
            {
                if (equalityComparer.Equals(current.Key, key))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        private int DefaultHashFunction(TKey key)
        {
            return key.GetHashCode();
        }

        private class Container
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Container Next { get; set; }

            public Container(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        
    }
}
