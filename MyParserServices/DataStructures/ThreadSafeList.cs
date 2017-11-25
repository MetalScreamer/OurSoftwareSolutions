using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.DataStructures
{
    public class ThreadSafeList<T> : IList<T>
    {
        private object _lock = new object();
        private List<T> internalList = new List<T>();

        public T this[int index]
        {
            get
            {
                lock (_lock)
                {
                    return internalList[index];
                }                
            }
            set
            {
                lock (_lock)
                {
                    internalList[index] = value;
                }
            }
        }

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return internalList.Count;
                }
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            lock (_lock)
            {
                internalList.Add(item);
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                internalList.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (_lock)
            {
                return internalList.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_lock)
            {
                internalList.CopyTo(array, arrayIndex);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                //ToList will create a new list so that we take a snapshot of the list and we don't try to enumerate the original, after the lock is released
                return internalList.ToList().GetEnumerator();
            }
        }

        public int IndexOf(T item)
        {
            lock (_lock)
            {
                return internalList.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (_lock)
            {
                internalList.Insert(index, item);
            }
        }

        public void ReLoad(IEnumerable<T> items)
        {
            lock (_lock)
            {
                internalList.Clear();
                internalList.AddRange(items);
            }
        }

        public bool Remove(T item)
        {
            lock (_lock)
            {
                return internalList.Remove(item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (_lock)
            {
                internalList.RemoveAt(index);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
