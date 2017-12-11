using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.DataStructures
{
    public class ThreadSafeList<T> : IList<T>, INotifyCollectionChanged, INotifyPropertyChanged
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
                bool changed = false;
                T oldVal = default(T);
                lock (_lock)
                {
                    if (!EqualityComparer<T>.Default.Equals(internalList[index], value))
                    {
                        oldVal = internalList[index];
                        internalList[index] = value;
                        changed = true;
                    }                                       
                }

                if (changed)
                {
                    CollectionChanged?.Invoke(
                        this, 
                        new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Replace, 
                                value, 
                                oldVal));
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

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public void Add(T item)
        {
            lock (_lock)
            {
                internalList.Add(item);
            }
            CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add, 
                    item));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        }

        public void Clear()
        {
            lock (_lock)
            {
                internalList.Clear();
            }
            CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
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
            CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        }

        public void ReLoad(IEnumerable<T> items)
        {
            lock (_lock)
            {
                internalList.Clear();
                internalList.AddRange(items);
            }
            CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        }

        public bool Remove(T item)
        {
            bool result;
            lock (_lock)
            {
                result = internalList.Remove(item);
            }

            CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));

            return result;
        }

        public void RemoveAt(int index)
        {
            T removedItem;
            lock (_lock)
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                removedItem = internalList[index];
                internalList.RemoveAt(index);
            }

            CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
