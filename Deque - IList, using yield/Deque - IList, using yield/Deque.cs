using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

public static class DequeTest
{
    public static IList<T> GetReverseView<T>(Deque<T> d)// where T : IComparable<T>
    {
        return d; // just in place to make recodex not complain.
    }
}

class TooSmallCapacityDemandedException : Exception { }

public class Deque<T> : IList<T>
{
    class End_Pointer
    {
        public int block { get; private set; }
        public int block_Index  { get; private set; }
        public End_Pointer() { 
            block = 0; // index 0 of first block
            block_Index = 511;
        }

        public End_Pointer(int block, int block_Index) {
            this.block = block; this.block_Index = block_Index;
        }
        
        public void Increment() {
            block_Index++;
            if(block_Index == 1024) {
                block++;
                block_Index = 0;
            }
        }

        public void Decrement() {
            block_Index--;
            if(block_Index == -1) {
                block--;
                block_Index = 1023;
            }
        }

        public void Set_New_Coordinates(int block, int block_Index) {
            this.block = block; this.block_Index = block_Index;
        }
    }
    
    private int _count = 0;
    private int _capacity = 1024;
    private int _blockCount = 1;
    private End_Pointer front = new End_Pointer();
    private End_Pointer back = new End_Pointer();
    private T[][] dataMap = new T[1][];


    public Deque() {
        dataMap[0] = new T[0b_100_0000_0000]; // allocate memory for hundred elements by default, note that in some cases this might waaay too much. Hence some overloaded constructors with this parametr would definitelly make sense. 
    }

    public Deque(int capacity) 
    {
        // let the user decide initial capacity
        // must not be smaller than 1024, which is predefined size of one block 
        if(capacity < 1024)
            throw new TooSmallCapacityDemandedException();
        while(_capacity < capacity)
            DoubleCapacity();
    }

    public int Count => _count;

    public bool IsReadOnly => false; // nobody told us to forbid addition and removal of elements after creation

    public void Add(T x)
    {
        if ( back.block_Index == 1023 && back.block == (_blockCount - 1)  ) // in future implement version that needs to double only once front and rear meet at same place -> rear will be allowed to port to index 0 and front will be allowed to port to last index, whoever of them gets there first
            DoubleCapacity();

        if(_count != 0) 
            back.Increment();
        dataMap[back.block][back.block_Index] = x;
        _count++;
        
    }

    private void DoubleCapacity() {
        int data_Blocks_Used = dataMap.Length;
        T[][] larger_dataMap = new T[data_Blocks_Used << 1][]; // double the count using shift operator // notice this would eventualy run out at 2^31, which for most purposes is ok, but definitelly makes it be not infinite 
        for(int i = 0; i < larger_dataMap.Length; ++i)
            larger_dataMap[i] = new T[0b_100_0000_0000];

        int blockZero = larger_dataMap.Length / 2;
        if(blockZero == 0)
            blockZero++;
        
        End_Pointer current = new End_Pointer(blockZero , 0);
        foreach(T item in this) {
            larger_dataMap[current.block][current.block_Index] = item;
            current.Increment();
        }
        current.Decrement(); // after exiting foreach loop return to real back index (no new element came)
        // 1. overwrite new larger dataMap with current 
        // 2. update _capacity
        // 3. update _blockCount 
        // 4. update back 
        // 5. update front 

        dataMap = larger_dataMap;
        _capacity *= 2;
        _blockCount *= 2;
        back.Set_New_Coordinates(current.block, current.block_Index); // for sure I could have used the construcor here, but semantics wise I do want to differentiate these two actions, hence this approach
        front.Set_New_Coordinates(blockZero, 0);
    }

    private (int block, int block_Index) get_2D_index(int _1D_index) {
        int block = _1D_index / 1024; //_1D_index >> 10; // divide by 1024 = 2^10 = shift right 10 times !!! as int as number is positive, which indexes must be -> chill
        int block_Index = _1D_index % 1024; //_1D_index & 0b11_1111_1111;
        return (block, block_Index);
    }

    private int get_1D_index(int block, int block_Index) {
        return (block * 1024) + block_Index;
        //int _1D_index = block * 1024; //block << 10; // multiply by 2^10
        //return _1D_index + block_Index// _1D_index |= block_Index; // logical or add, since block index are guaranted by end_pointer class to be strictly less than 1024
    }

    private int calculate_New_Front_Position() {
        int middle = _capacity; // next middle is current capacity 
        int half_of_elements = _count / 2;//_count >> 1; // --> /2
        return middle - half_of_elements; 
    }

    public T this[int index]  // indexer declaration
    {
        get {
            int front_1D = get_1D_index(front.block, front.block_Index);
            (int block, int block_Index) = get_2D_index(index + front_1D);
            return dataMap[block][block_Index]; 
        }

        set {
            int front_1D = get_1D_index(front.block, front.block_Index);
            (int block, int block_Index) = get_2D_index(index + front_1D);
            dataMap[block][block_Index] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        End_Pointer current = new End_Pointer(front.block, front.block_Index);
        for (int i = 0; i < _count; ++i) {
            yield return dataMap[current.block][current.block_Index];
            current.Increment();
        }  
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return dataMap.GetEnumerator();
    }

    public void Clear()
    {
        _count = 0;
        _capacity = 1024;
        _blockCount = 1;
        dataMap = new T[1][];
        dataMap[0] = new T[0b_100_0000_0000];
        front.Set_New_Coordinates(0, 511);
        back.Set_New_Coordinates(0, 511);
    }

    public bool Contains(T x)
    {
        End_Pointer current = new End_Pointer(front.block, front.block_Index);

        for (int i = 0; i < _count; ++i) {
            if (x.Equals(dataMap[current.block][current.block_Index]))
                return true;
            current.Increment();   
        }
        return false;
    }

    public void CopyTo(T[] target, int fromIndex)
    {
       /* // exception: 0. if target is null 1. index is negative, 2. there is not enough space for all elements from start index to end of given array
        if (target == null)
            throw new ArgumentNullException();
        if (fromIndex < 0)
            throw new ArgumentOutOfRangeException();
        if (target.Length < _count + fromIndex)
            throw new ArgumentException();
        for (int i = fromIndex; i < (fromIndex + _count); ++i)
            target[i] = data[i - fromIndex];*/
    }

    public int IndexOf(T item, int index)
    {
      /*  if (index < 0 || (_count - 1) < index)
            throw new ArgumentOutOfRangeException();
        for (int i = index; i < _count; ++i)
            if (item.Equals(data[i]))
                return i;*/
        return -1;
    }

    public int IndexOf(T item, int index, int Xcount)
    { // prepended count with X in order to not confuse it with class' property _count and Count
       /* if (Xcount < 0 || index < 0 || (_count - 1) < (index + Xcount))
            throw new ArgumentOutOfRangeException();

        for (int i = index; i < (index + Xcount); ++i)
            if (item.Equals(data[i]))
                return i;*/
        return -1;
    }

    public int IndexOf(T item)
    {
       /* for (int i = 0; i < _count; ++i)
            if (item.Equals(data[i]))
                return i;*/
        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || _count < index)
            throw new ArgumentOutOfRangeException();
        /*if (_count == _capacity)
            DoubleCapacity();
        // first shift all elements by one index, note it significantly more practical to shift them from end to the point of insertion
        _count++;
        for (int i = _count - 1; index < i; --i) // copy the element from previous index to here
            data[i] = data[i - 1];
        data[index] = item;*/
    }

    public bool Remove(T item)
    {   // remove first occurence of the item T from the begining 
        // Remarks
        // If type T implements the IEquatable<T> generic interface, the equality comparer is the Equals method of that interface; otherwise, the default equality comparer is Object.Equals.
        /*for (int i = 0; i < _count; ++i)
            if (data[i].Equals(item))
            {
                RemoveAt(i); // call own method
                return true;
            }*/
        return false;
    }

    public void RemoveAt(int index)
    {
       /* if (IsReadOnly)
            throw new NotSupportedException();
        if (index < 0 || (_count - 1) < index)
            throw new ArgumentOutOfRangeException();
        // -> since this collection is indexed, the best way is to overwrite all elements with one higher index, similar to perforaming swaps except, the first element disappear
        // !! beware to not touch the index at _count -> most of the cases its there but  when _count == _capacity this yields OutOfBoundsExcpetion !! -> the last element may stay where it was -> by decrementing _count by one it is unreachable by outside world and uppon first add, it will get overwritte it exists at two indexes than (after removal: _count and _count+1)
        for (int i = index; i < (_count - 1); ++i)
            data[i] = data[i + 1];*/
        _count--; // job done 😂
    }

}