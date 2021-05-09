using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

public static class DequeTest
{
    public static IList<T> GetReverseView<T>(Deque<T> d)
    {
        d.Reverse_North_Pole();
        return d; 
    }
}

class TooSmallCapacityDemandedException : Exception { }

public class Deque<T> : IList<T>
{
    class LBA_Pointer // Linear Block Adress Pointer -> in analogy to LBA from principles of computers
    {
        public int block { get; private set; }
        public int block_Offset  { get; private set; }
        public bool north_Is_North = true;

        public LBA_Pointer() { 
            block = 0; // index 0 of first block
            block_Offset = 511;
        }
        public LBA_Pointer(int block, int block_Offset) {
            this.block = block; this.block_Offset = block_Offset;
        }

        public LBA_Pointer(LBA_Pointer copy) {
            block = copy.block; 
            block_Offset = copy.block_Offset;
            north_Is_North = copy.north_Is_North;
        }
        
        public void Increment() {
            if(north_Is_North) 
                _Increment();
            else 
                _Decrement();
        }

        public void Decrement() {
            if(north_Is_North) 
                _Decrement();
            else 
                _Increment();
        }

        public void _Increment() {
            block_Offset++;
            if(block_Offset == 1024) {
                block++;
                block_Offset = 0;
            }
        }

        public void _Decrement() {
            block_Offset--;
            if(block_Offset == -1) {
                block--;
                block_Offset = 1023;
            }
        }

        public void Set_New_Coordinates(int block, int block_Offset) {
            this.block = block; this.block_Offset = block_Offset;
        }
    }
    
    private int _count = 0;
    private int _capacity = 1024;
    private int _blockCount = 1;
    private LBA_Pointer front = new LBA_Pointer();
    private LBA_Pointer back = new LBA_Pointer();
    private T[][] dataMap = new T[1][];
    private bool north_Is_North = true;


    public Deque() {
        dataMap[0] = new T[0b_100_0000_0000]; // allocate memory for hundred elements by default, note that in some cases this might waaay too much. Hence some overloaded constructors with this parametr would definitelly make sense. 
    }

    public Deque(int capacity) // let the user decide initial capacity must not be smaller than 1024, which is predefined size of one block 
    {
        if(capacity < 1024)
            throw new TooSmallCapacityDemandedException();
        dataMap[0] = new T[0b_100_0000_0000];
        while(_capacity < capacity)
            DoubleCapacity();
    }

    public int Count => _count;
    public bool IsReadOnly => false; // nobody told us to forbid addition and removal of elements after creation

    public void Add(T x)
    {
        if ( back.block_Offset == 1023 && back.block == (_blockCount - 1)  ) // in future implement version that needs to double only once front and rear meet at same place -> rear will be allowed to port to index 0 and front will be allowed to port to last index, whoever of them gets there first
            DoubleCapacity();

        if(_count != 0)  {
            if(north_Is_North)
                back.Increment();
            else 
                front._Decrement();
        }

        if(north_Is_North) {
            dataMap[back.block][back.block_Offset] = x;
        } else {
            dataMap[front.block][front.block_Offset] = x;
        }
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
            larger_dataMap[current.block][current.block_Offset] = item;
            current.Increment();
        }
        current.Decrement(); // after exiting foreach loop return to real back index (no new element came)
        
        dataMap = larger_dataMap;
        _capacity <<= 1;
        _blockCount <<= 1;
        back.Set_New_Coordinates(current.block, current.block_Offset); // for sure I could have used the construcor here, but semantics wise I do want to differentiate these two actions, hence this approach
        front.Set_New_Coordinates(blockZero, 0);
    }

    public void Reverse_North_Pole() {
        // swap front and back
        //int block = front.block;
        //int block_Offset = front.block_Offset;
        //front.Set_New_Coordinates(back.block, back.block_Offset);
        //back.Set_New_Coordinates(block, block_Offset);
        // swap north pole
        if(north_Is_North)
            north_Is_North = false;
        else 
            north_Is_North = true;

        front.north_Is_North = north_Is_North;
        back.north_Is_North = north_Is_North;
    }

    public T this[int index]  // indexer declaration
    {   
        // variable front_plus_offset changed to head_with_offset in order to better catch its semantics with reverse view
        get {
            //WriteLine($"Getter reporting! Recieved call for index: {index}");
            int head_with_offset = north_Is_North ? ( ( ( front.block << 10 ) | front.block_Offset ) + index  ) : ( ( ( back.block << 10 ) | back.block_Offset ) - index ) ;
            return dataMap[ head_with_offset >> 10 ][ head_with_offset & 0b11_1111_1111 ]; 
        }

        set {
            int head_with_offset = north_Is_North ? ( ( ( front.block << 10 ) | front.block_Offset ) + index  ) : ( ( ( back.block << 10 ) | back.block_Offset ) - index ) ;
            dataMap[ head_with_offset >> 10 ][ head_with_offset & 0b11_1111_1111 ] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        //WriteLine($"Enumartor reporting!");
        LBA_Pointer current = north_Is_North ? new LBA_Pointer(front) : new LBA_Pointer(back);
        for (int i = 0; i < _count; ++i) {
            //WriteLine($"Enumartor reporting! i: {i}");
            yield return dataMap[current.block][current.block_Offset];
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
        LBA_Pointer current = north_Is_North ? new LBA_Pointer(front) : new LBA_Pointer(back);

        for (int i = 0; i < _count; ++i) {
            if (x.Equals(dataMap[current.block][current.block_Offset]))
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
        if ( (front.block == 0 && front.block_Offset == 0) || ( back.block == ( _blockCount-1 ) && back.block_Offset > 1020))
            DoubleCapacity();

        if(index < (_count / 2)) 
            shift_To_Front(index); // shift elements towards front
        else  
            shift_To_Back(index); // shift elements towards end
        _count++;
        this[index] = item;
    }

    private void shift_To_Front(int up_to_a_point) {

        if(north_Is_North) {
            LBA_Pointer index_out_of_bound = new LBA_Pointer(front); 
            index_out_of_bound.Decrement();
            dataMap[index_out_of_bound.block][index_out_of_bound.block_Offset] = this[0];
            for(int i = 1; i <= up_to_a_point; ++i)
                this[i-1] = this[i];
            front.Decrement();

        } else {
            LBA_Pointer index_out_of_bound = new LBA_Pointer(back); 
            index_out_of_bound.Decrement();
            dataMap[index_out_of_bound.block][index_out_of_bound.block_Offset] = this[0];
            for(int i = 1; i <= up_to_a_point; ++i)
                this[i-1] = this[i];
            back.Decrement();

        }  
            
    }



    private void shift_To_Back(int up_to_a_point) {
        LBA_Pointer index_out_of_bound = north_Is_North ? new LBA_Pointer(back) : new LBA_Pointer(front);
        index_out_of_bound.Increment();
        dataMap[index_out_of_bound.block][index_out_of_bound.block_Offset] = this[_count-1];
        
        for(int i = ( _count-1 ) ; up_to_a_point < i; --i)
            this[i] = this[i-1];

        if(north_Is_North)
            back.Increment();
        else 
            front.Increment();
    }

    public bool Remove(T item)
    {
        // Remarks:If type T implements the IEquatable<T> generic interface, the equality comparer is the Equals method of that interface; otherwise, the default equality comparer is Object.Equals.
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

            if(north_Is_North)
                front.Increment();
            else 
                back.Decrement();
            
        } else  {
            for(int i = index; i < (_count - 1) ; ++i) 
                this[i] = this[ i+1 ];
            
            if(north_Is_North)
                back.Decrement();
            else 
                front.Increment();
        }
        _count--; // job done 😂😂😂
    }
}

/*
    public T this[int index]  // indexer declaration
    {
        get {
            int front_1D = get_1D_index(front.block, front.block_Offset);
            (int block, int block_Offset) = get_2D_index(index + front_1D);
            return dataMap[block][block_Offset]; 
        }

        set {
            int front_1D = get_1D_index(front.block, front.block_Offset);
            (int block, int block_Offset) = get_2D_index(index + front_1D);
            dataMap[block][block_Offset] = value;
        }
    }
*/

/*
    private (int block, int block_Offset) get_2D_index(int _1D_index) {
        //int block = _1D_index / 1024; //_1D_index >> 10; // divide by 1024 = 2^10 = shift right 10 times !!! as int as number is positive, which indexes must be -> chill
        //int block_Offset = _1D_index % 1024; //_1D_index & 0b11_1111_1111;
        return ( (_1D_index >> 10) , (_1D_index & 0b11_1111_1111) );
    }

    private int get_1D_index(int block, int block_Offset) {
        //return (block * 1024) + block_Offset;
        //int _1D_index = block * 1024; //block << 10; // multiply by 2^10
        //return _1D_index + block_Offset// _1D_index |= block_Offset; // logical or add, since block index are guaranted by end_pointer class to be strictly less than 1024
        return (block << 10) | block_Offset;
    }

    private int calculate_New_Front_Position() {
        //int middle = _capacity; // next middle is current capacity 
        //int half_of_elements = _count / 2;//_count >> 1; // --> /2
        //return middle - half_of_elements; 
        return _capacity - ( _count >> 1 ); 
    }
*/

/*
public T this[int index]  // indexer declaration
    {   
        // variable front_plus_offset changed to head_with_offset in order to 
        // better catch its semantics with reverse view
        get {
            int head_with_offset;
            if(north_Is_North)
                head_with_offset = ( ( front.block << 10 ) | front.block_Offset ) + index;
            else 
                head_with_offset = ( ( back.block << 10 ) | back.block_Offset ) - index;
            return dataMap[ head_with_offset >> 10 ][ head_with_offset & 0b11_1111_1111 ]; 
        }

        set {
            int head_with_offset;
            if(north_Is_North)
                head_with_offset = ( ( front.block << 10 ) | front.block_Offset ) + index;
            else 
                head_with_offset = ( ( back.block << 10 ) | back.block_Offset ) - index;
            dataMap[ head_with_offset >> 10 ][ head_with_offset & 0b11_1111_1111 ] = value;
        }
    }

*/

//if(index == _count) {Add(item);return;}
    //(int block, int block_Offset) = get_2D_index(front_plus_offset);

/*
IEnumerable<string> combinations(int k, int n) {
        if (k == 0)
            yield return "";
        else if (n > 0) { 
            foreach (string s in combinations(k - 1, n - 1))
                yield return s + " " + n;
            foreach (string s in combinations(k, n - 1))
                yield return s;
        }
    }*/