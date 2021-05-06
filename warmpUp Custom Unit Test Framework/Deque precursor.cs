using System;
using static System.Console;
using static System.Random;
using System.Collections.Generic;
using System.IO;
using static System.Environment;

namespace warmUp {/*
    Interface IList<T>

    Properties:
    int Count { get; }                      : ICollection
    bool IsReadOnly                         : ICollection
    public T this[int index] { get; set; }  : own

    Methods: 
    Add(T)                                  : ICollection 
    Clear()                                 : ICollection
    Contains(T)                             : ICollection
    CopyTo(T[], Int32)                      : ICollection
    GetEnumerator()                         : IEnumerable
    IndexOf(T)                              : own
    Insert(Int 32, T)                       : own
    Remove(T)                               : ICollection
    RemoveAt(Int32)                         : own
    */

    class Program {
        static void Main(string[] args) {
            UnitTestFramework ut = new UnitTestFramework();
            ut.loadValidTests(); // ut.loadLanguage()
            ut.runTests();
            ut.compareResults();
        }
    }

    class MyList<T> /*: IList<T>*/ where T : IComparable<T>    {
        private int _count = 0;
        private int _capacity = 100;
        private T[] data = new T[100]; // allocate memory for hundred elements by default, note that in some cases this might waaay too much. Hence some constructor with this parametr would definitelly make sense. 
        
        public MyList() {}

        public int Count => _count;
        public bool IsReadOnly => false; // nobody told us to forbid addition and removal of elements after creation

        public void Add(T x) {
            if( _count == _capacity) 
                DoubleCapacity();
            data[_count] = x;
            _count++;
        }

        private void DoubleCapacity() {                     // calling this method only from add, hence _count being equal to capacity is guaranteed condition in here
            T[] larger = new T[ _capacity * 2 ];            // initilise new data container twice as large 
            for(int i = 0; i < _count; ++i)                 // copy current data to new container
                larger[i] = data[i];
            data = larger;                                  // overwrite old data by new container
            _capacity *= 2;                                 // save new capacity
        }

        public IEnumerator<T> GetEnumerator() {
            for(int i = 0; i < _count; ++i)
                yield return data[i];
        }

        public void Clear() {
            _count = 0;
            _capacity = 100;
            data = new T[100];
        }

        public bool Contains(T x) {
            for(int i = 0; i < _count; ++i)
                if(x.CompareTo(data[i]) == 0)
                    return true;
            return false;
        }

        public void CopyTo(T[] target, int fromIndex) {
            // exception: 0. if target is null 1. index is negative, 2. there is not enough space for all elements from start index to end of given array
            if(target == null)
                throw new ArgumentNullException();
            if(fromIndex < 0) 
                throw new ArgumentOutOfRangeException();
            if(target.Length < _count + fromIndex) 
                throw new ArgumentException();
            for(int i = fromIndex; i < (fromIndex + _count) ; ++i )
                target[i] = data[ i - fromIndex ];
        }

        public int IndexOf(T item, int index) {
            if(index < 0 || (_count - 1) < index)
                throw new ArgumentOutOfRangeException();
            for(int i = index; i < _count; ++i)
                if(item.CompareTo(data[i]) == 0)
                    return i;
            return -1;
        }

        public int IndexOf (T item, int index, int Xcount) { // prepended count with X in order to not confuse it with class' property _count and Count
            if(Xcount < 0 || index < 0 || (_count - 1) < (index + Xcount) )
                throw new ArgumentOutOfRangeException();

            for(int i = index; i < ( index + Xcount ) ; ++i)
                if(item.CompareTo(data[i]) == 0)
                    return i;
            return -1;
        }

        public int IndexOf (T item) {
            for(int i = 0; i < _count; ++i)
                if(item.CompareTo(data[i]) == 0)
                    return i;
            return -1;
        }

        public void Insert(int index, T item) {
            if(index < 0 || _count < index)
                throw new ArgumentOutOfRangeException();
            if( _count == _capacity) 
                DoubleCapacity();
            // first shift all elements by one index
            // note it significantly more practical to shift them from end to the point of insertion
            _count++;
            for(int i = _count-1; index < i ; --i) // copy the element from previous index to here
                data[i] = data[i-1];
            data[index] = item;
        }



    }
}
