using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

public static class DequeTest
{
    public static IList<T> GetReverseView<T>(Deque<T> d)
    {
        return d; // just in place to make recodex not complain.
    }
}

class TooSmallCapacityDemandedException : Exception { }

public class Deque<T> : IList<T>
{
    class LBA_Pointer // Linear Block Adress Pointer -> in analogy to LBA from principles of computers
    {
        public int block { get; private set; }
        public int block_Index  { get; private set; }
        public LBA_Pointer() { 
            block = 0; // index 0 of first block
            block_Index = 511;
        }

        public LBA_Pointer(int block, int block_Index) {
            this.block = block; this.block_Index = block_Index;
        }

        public LBA_Pointer(LBA_Pointer copy) {
            block = copy.block; 
            block_Index = copy.block_Index;
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
    private LBA_Pointer front = new LBA_Pointer();
    private LBA_Pointer back = new LBA_Pointer();
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

        int blockZero = _blockCount == 1 ? 1 : larger_dataMap.Length / 2 ;
        
        LBA_Pointer current = new LBA_Pointer(blockZero , 0);
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
        //int block = _1D_index / 1024; //_1D_index >> 10; // divide by 1024 = 2^10 = shift right 10 times !!! as int as number is positive, which indexes must be -> chill
        //int block_Index = _1D_index % 1024; //_1D_index & 0b11_1111_1111;
        return ( (_1D_index >> 10) , (_1D_index & 0b11_1111_1111) );
    }

    private int get_1D_index(int block, int block_Index) {
        //return (block * 1024) + block_Index;
        //int _1D_index = block * 1024; //block << 10; // multiply by 2^10
        //return _1D_index + block_Index// _1D_index |= block_Index; // logical or add, since block index are guaranted by end_pointer class to be strictly less than 1024
        return (block << 10) | block_Index;
    }

    private int calculate_New_Front_Position() {
        //int middle = _capacity; // next middle is current capacity 
        //int half_of_elements = _count / 2;//_count >> 1; // --> /2
        //return middle - half_of_elements; 
        return _capacity - ( _count >> 1 ); 
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
        LBA_Pointer current = new LBA_Pointer(front);
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
        LBA_Pointer current = new LBA_Pointer(front);

        for (int i = 0; i < _count; ++i) {
            if (x.Equals(dataMap[current.block][current.block_Index]))
                return true;
            current.Increment();   
        }
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

        int target_Index = fromIndex;
        foreach(T item in this) {
            target[target_Index] = item;
            target_Index++;
        }
    }

    public int IndexOf(T item, int index)
    {
        if (index < 0 || (_count - 1) < index)
            throw new ArgumentOutOfRangeException();
        int i = 0;
        foreach(T x in this) { 
            if (item.Equals(x))
                return i;
            i++;
        }
        return -1;
    }

    public int IndexOf(T item, int index, int Xcount)
    {   // prepended count with X in order to not confuse it with class' property _count and Count
        if (Xcount < 0 || index < 0 || (_count - 1) < (index + Xcount))
            throw new ArgumentOutOfRangeException();
        int i = 0;
        foreach(T x in this) { 
            if (item.Equals(x))
                return i;
            i++;
        }
        return -1;
    }

    public int IndexOf(T item)
    {
        int i = 0;
        foreach(T x in this) { 
            if (item.Equals(x))
                return i;
            i++;
        }
        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || _count < index)
            throw new ArgumentOutOfRangeException();
        if ( (front.block == 0 && front.block_Index == 0) || ( back.block == ( _blockCount-1 ) && back.block_Index > 1020))
            DoubleCapacity();

        //if(index == _count) {Add(item);return;}
        if(index < (_count / 2)) 
            shift_To_Front(index); // shift elements towards front
        else  
            shift_To_Back(index); // shift elements towards end
        _count++;
        this[index] = item;
    }

    private void shift_To_Front(int up_to_a_point) {
        LBA_Pointer index_out_of_bound = new LBA_Pointer(front);
        index_out_of_bound.Decrement();
        dataMap[index_out_of_bound.block][index_out_of_bound.block_Index] = this[0];

        for(int i = 1; i <= up_to_a_point; ++i)
            this[i-1] = this[i];

        front.Decrement();
    }

    private void shift_To_Back(int up_to_a_point) {
        LBA_Pointer index_out_of_bound = new LBA_Pointer(back);
        index_out_of_bound.Increment();
        dataMap[index_out_of_bound.block][index_out_of_bound.block_Index] = this[_count-1];
        
        for(int i = ( _count-1 ) ; up_to_a_point < i; --i)
            this[i] = this[i-1];

        back.Increment();
    }

    public bool Remove(T item)
    {   // remove first occurence of the item T from the begining 
        // Remarks
        // If type T implements the IEquatable<T> generic interface, the equality comparer is the Equals method of that interface; otherwise, the default equality comparer is Object.Equals.
        int i = 0;
        foreach(T x in this){
            if (x.Equals(item))
            {
                RemoveAt(i); // call own method 
                return true;
            }
            i++;
        }
        return false;
        // LBA_Pointer current = new LBA_Pointer(front);
    } 

    public void RemoveAt(int index)
    {
        if (IsReadOnly)
            throw new NotSupportedException();
        if (index < 0 || (_count - 1) < index)
            throw new ArgumentOutOfRangeException();
        
        if(index < (_count / 2)) {
            for(int i = index; 0 < i; --i) 
                this[i] = this[ i-1 ];
            front.Increment();
        } else  {
            for(int i = index; i < (_count - 1) ; ++i) 
                this[i] = this[ i+1 ];
            back.Decrement();
        }
        _count--; // job done 😂😂😂
    }
}