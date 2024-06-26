﻿using System.Collections;

namespace List
{
    public class MyList<T> : ICollection<T>
    {
        private T[] _array;
        private int _size;
        private readonly object _locker = new object();

        public int Count => _size;
        public bool IsReadOnly => false;

        public MyList()
        {
            _array = Array.Empty<T>();
        }

        public MyList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("The capacity must not be less than or equal to zero");
            }
            _array = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                if (index >= _size)
                {
                    throw new IndexOutOfRangeException
                        ("Index was out of range. Must be non-negative and less than the size of the collection");
                }
                lock (_locker)
                {
                    return _array[index];
                }
            }

            set
            {
                if (index >= _size)
                {
                    throw new IndexOutOfRangeException
                        (" Index was out of range. Must be non-negative and less than the size of the collection");
                }
                lock (_locker)
                {
                    _array[index] = value;
                }
            }
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            lock (_locker)
            {
                if (_size == _array.Length)
                {
                    Grow(_size + 1);
                }

                _array[_size] = item;
                _size++;
            }
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (_size != 0)
            {
                lock (_locker)
                {
                    int index = Array.IndexOf(_array, item);
                    if (index >= 0)
                    {
                        RemoveAt(index);                      
                    }
                    return true;
                }         
            }
            return false;
        }

        public void RemoveAt(int index)
        {

            if (index >= _size)
            {
                throw new IndexOutOfRangeException
                    ("Index was out of range. Must be non-negative and less than the size of the collection");
            }
            lock (_locker)
            {
                _size--;

                if (index < _size)
                {
                    Array.Copy(_array, index + 1, _array, index, _size - index);
                }
                _array[_size] = default!;
            }
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (_size == 0)
            {
                return false;
            }

            lock (_locker)
            {
                return Array.IndexOf(_array, item, 0, _size) >= 0;
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                Array.Clear(_array, 0, _size);
                _size = 0;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ArgumentNullException.ThrowIfNull(array);
            if (arrayIndex < 0)
            {
                throw new IndexOutOfRangeException
                    ("Index was out of range. Must be non-negative and less than the size of the collection");
            }

            lock (_locker)
            {
                Array.Copy(_array, 0, array, arrayIndex, _size);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_locker)
            {
                return new ListEnumerator<T>(_array, _size);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Grow(int capacity)
        {
            const int GrowFactor = 2;
            int newCapacity = Math.Max(GrowFactor * capacity, 1);

            lock (_locker)
            {
                T[] newArray = new T[newCapacity];
                Array.Copy(_array, 0, newArray, 0, _size);
                _array = newArray;
            }
        }
    }
}