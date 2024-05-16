using System.Collections;

namespace List
{
    public class ListEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] _array;
        private readonly int _size;
        private int position = -1;
        public ListEnumerator(T[] array, int size)
        {
            ArgumentNullException.ThrowIfNull(array);
            if (_size < 0)
            {
                throw new ArgumentException("The size must not be less than or equal to zero");
            }
            _array = array;
            _size = size;
        }
        public T Current
        {
            get
            {
                if (position >= _size || position == -1)
                {
                    throw new ArgumentException();
                }
                return _array[position];
            }
        }
        object IEnumerator.Current => Current!;
        public bool MoveNext()
        {
            while (position < _size)
            {
                position++;
                return position < _size;
            }

            return false;
        }
        public void Dispose() { }
        public void Reset() => position = -1;
    }
}
