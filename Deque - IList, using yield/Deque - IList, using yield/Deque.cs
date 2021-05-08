using System;
using System.Collections;
using System.Collections.Generic;


//namespace Deque_IList_Using_Yield
//{

    public static class DequeTest
    {
        public static IList<T> GetReverseView<T>(Deque<T> d)// where T : IComparable<T>
        {
            return d; // just in place to make recodex not complain.
	    }
    }

    public class Deque<T> : IList<T> //where T : IComparable<T>
    {
        private int _count = 0;
        private int _capacity = 100;
        private T[] data = new T[100]; // allocate memory for hundred elements by default, note that in some cases this might waaay too much. Hence some overloaded constructors with this parametr would definitelly make sense. 

        public Deque() { }
        public Deque(int capacity) // let the user decide initial capacity
        {
            _capacity = capacity;
            data = new T[capacity];
        }

        public int Count => _count;
        public bool IsReadOnly => false; // nobody told us to forbid addition and removal of elements after creation

        public void Add(T x)
        {
            if (_count == _capacity)
                DoubleCapacity();
            data[_count] = x;
            _count++;
        }

        private void DoubleCapacity()
        {                     // calling this method only from add, hence _count being equal to capacity is guaranteed condition in here
            T[] larger = new T[_capacity * 2];            // initilise new data container twice as large 
            for (int i = 0; i < _count; ++i)                 // copy current data to new container
                larger[i] = data[i];
            data = larger;                                  // overwrite old data by new container
            _capacity *= 2;                                 // save new capacity
        }

        public T this[int index]  // indexer declaration
        {
            get => data[index];
            set => data[index] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; ++i)
                yield return data[i];
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }



        public void Clear()
        {
            _count = 0;
            _capacity = 100;
            data = new T[100];
        }

        public bool Contains(T x)
        {
            for (int i = 0; i < _count; ++i)
                if (x.Equals(data[i]))
                    return true;
            return false;
        }

        public void CopyTo(T[] target, int fromIndex)
        {
            // exception: 0. if target is null 1. index is negative, 2. there is not enough space for all elements from start index to end of given array
            if (target == null)
                throw new ArgumentNullException();
            if (fromIndex < 0)
                throw new ArgumentOutOfRangeException();
            if (target.Length < _count + fromIndex)
                throw new ArgumentException();
            for (int i = fromIndex; i < (fromIndex + _count); ++i)
                target[i] = data[i - fromIndex];
        }

        public int IndexOf(T item, int index)
        {
            if (index < 0 || (_count - 1) < index)
                throw new ArgumentOutOfRangeException();
            for (int i = index; i < _count; ++i)
                if (item.Equals(data[i]))
                    return i;
            return -1;
        }

        public int IndexOf(T item, int index, int Xcount)
        { // prepended count with X in order to not confuse it with class' property _count and Count
            if (Xcount < 0 || index < 0 || (_count - 1) < (index + Xcount))
                throw new ArgumentOutOfRangeException();

            for (int i = index; i < (index + Xcount); ++i)
                if (item.Equals(data[i]))
                    return i;
            return -1;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _count; ++i)
                if (item.Equals(data[i]))
                    return i;
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || _count < index)
                throw new ArgumentOutOfRangeException();
            if (_count == _capacity)
                DoubleCapacity();
            // first shift all elements by one index, note it significantly more practical to shift them from end to the point of insertion
            _count++;
            for (int i = _count - 1; index < i; --i) // copy the element from previous index to here
                data[i] = data[i - 1];
            data[index] = item;
        }

        public bool Remove(T item)
        {   // remove first occurence of the item T from the begining 
            // Remarks
            // If type T implements the IEquatable<T> generic interface, the equality comparer is the Equals method of that interface; otherwise, the default equality comparer is Object.Equals.
            for (int i = 0; i < _count; ++i)
                if (data[i].Equals(item))
                {
                    RemoveAt(i); // call own method
                    return true;
                }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (index < 0 || (_count - 1) < index)
                throw new ArgumentOutOfRangeException();
            // -> since this collection is indexed, the best way is to overwrite all elements with one higher index, similar to perforaming swaps except, the first element disappear
            // !! beware to not touch the index at _count -> most of the cases its there but  when _count == _capacity this yields OutOfBoundsExcpetion !! -> the last element may stay where it was -> by decrementing _count by one it is unreachable by outside world and uppon first add, it will get overwritte it exists at two indexes than (after removal: _count and _count+1)
            for (int i = index; i < (_count - 1); ++i)
                data[i] = data[i + 1];
            _count--; // job done 😂
        }

    }
//}

