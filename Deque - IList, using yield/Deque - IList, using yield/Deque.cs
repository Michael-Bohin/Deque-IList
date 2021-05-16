using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

public static class DequeTest {
    public static IList<T> GetReverseView<T>(Deque<T> d) {
        Deque<T> ShallowCopy = (Deque<T>) d.GetShallowCopy();
        ShallowCopy.Reverse_North_Pole();
        return ShallowCopy;
    }
}

interface IDeque<T> : IList<T> {
    // there is equivalent english and czech part of the interface
    // english part:
    void Reverse_North_Pole();

    T RemoveFront ();
    void EnqueueFront (T item);
    T RemoveBack();
    void EnqueueBack (T item);

    bool TryPeekFront (out T result);
    bool TryPeekBack (out T result);
    T PeekFront ();
    T PeekBack ();

    // czech part:
    T OdeberZacatek ();
    void ZafrontiZacatek (T item);
    T OdeberKonec();
    void ZafrontiKonec (T item);

    bool ZkusNahlednoutZacatek (out T result);
    bool ZkusNahlednoutKonec (out T result);
    T NahledniZacatek ();
    T NahledniKonec ();
}

class InvalidOperationException: Exception { }

public class LBA_Pointer { // Linear Block Adress Pointer -> in analogy to LBA from principles of computers
    public int block { get; private set; }
    public int block_Offset { get; private set; }

    public LBA_Pointer(int offset) {
        block = 0; // index 0 of first block
        block_Offset = offset;
    }

    public LBA_Pointer(int block, int block_Offset) {
        this.block = block; 
        this.block_Offset = block_Offset;
    }

    public void Increment() {
        block_Offset++;
        if (block_Offset == 1024) {
            block++;
            block_Offset = 0;
        }
    }

    public void Decrement() {
        block_Offset--;
        if (block_Offset == -1) {
            block--;
            block_Offset = 1023;
        }
    }

    public void Set_Coordinates(int block, int offset) {
        this.block = block;
        block_Offset = offset;
    }
}

public class Deque<T> : IDeque<T> {
    private LBA_Pointer front = new LBA_Pointer(510);
    private LBA_Pointer back = new LBA_Pointer(511);
    private T[][] dataMap = new T[1][];
    private bool north_Is_North = true;
    private bool enumeration_In_Process = false;

    public int Count {
        get {
            return ((back.block - front.block) << 10 ) + back.block_Offset - front.block_Offset - 1;
        }
    }

    public Deque() { 
        dataMap[0] = new T[0b_100_0000_0000]; 
    }

    public bool IsReadOnly => false; // nobody told us to forbid addition and removal of elements after creation
    public Deque<T> GetShallowCopy() => (Deque<T>) this.MemberwiseClone();

    public void Add(T x) {
        if (enumeration_In_Process)
            throw new InvalidOperationException();

        if(back.block == dataMap.Length || front.block == -1) // in future implement circular version -> only double capacity once all boxes had been used 
            DoubleCapacity();

        if (north_Is_North) {
            dataMap[back.block][back.block_Offset] = x;
            back.Increment();
        } else {
            dataMap[front.block][front.block_Offset] = x;
            front.Decrement();
        }
    }

    private void DoubleCapacity() {
        if (enumeration_In_Process)
            throw new InvalidOperationException();

        T[][] larger_dataMap = new T[ dataMap.Length << 1][]; // double the count using shift operator // notice this would eventualy run out at 2^31, which for most purposes is ok, but definitelly makes it be not infinite 
        for (int i = 0; i < larger_dataMap.Length; ++i)
            larger_dataMap[i] = new T[0b_100_0000_0000];

        int blockZero = north_Is_North ? (dataMap.Length - (dataMap.Length >> 1)) : (dataMap.Length + (dataMap.Length >> 1)) ;
        LBA_Pointer current = new LBA_Pointer(blockZero, 0);

        foreach (T item in this) {
            if(north_Is_North)
                current.Increment();
            else 
                current.Decrement();
            larger_dataMap[current.block][current.block_Offset] = item;
        }

        if(north_Is_North) {
            current.Increment();
        } else { 
            current.Decrement();
        }

        dataMap = larger_dataMap;

        if (north_Is_North) {
            back.Set_Coordinates( current.block, current.block_Offset );
            front.Set_Coordinates( blockZero , 0 );
        } else {
            front.Set_Coordinates( current.block, current.block_Offset );
            back.Set_Coordinates( blockZero , 0 );
        }
    }

    public void Reverse_North_Pole() {
        if (enumeration_In_Process)
            throw new InvalidOperationException();

        north_Is_North = north_Is_North ? false : true;
    }

    public T this[int index] { // indexer declaration
        get {
            if (index < 0 || (Count - 1) < index)
                throw new ArgumentOutOfRangeException();

            int head_with_offset = north_Is_North ? (( (front.block << 10) | front.block_Offset) + index + 1 ) : (( (back.block << 10) | back.block_Offset) - index - 1 );
            return dataMap[head_with_offset >> 10][head_with_offset & 0b11_1111_1111];
        }

        set {
            if (enumeration_In_Process)
                throw new InvalidOperationException();
            if (index < 0 || (Count - 1) < index)
                throw new ArgumentOutOfRangeException();

            int head_with_offset = north_Is_North ? (( (front.block << 10) | front.block_Offset) + index + 1 ) : (( (back.block << 10) | back.block_Offset) - index - 1 );
            dataMap[head_with_offset >> 10][head_with_offset & 0b11_1111_1111] = value;
        }
    }

    public IEnumerator<T> GetEnumerator() {
        enumeration_In_Process = true;
        for(int i = 0; i < Count; ++i)
            yield return this[i];
        enumeration_In_Process = false;
    }

    IEnumerator IEnumerable.GetEnumerator() => dataMap.GetEnumerator();

    public void Clear() {
        if (enumeration_In_Process)
            throw new InvalidOperationException();

        dataMap = new T[1][];
        dataMap[0] = new T[0b_100_0000_0000];
        front.Set_Coordinates(0, 511);
        back.Set_Coordinates(0, 512);
    }

    public void CopyTo(T[] target, int fromIndex) {
        // exception: 0. if target is null 1. index is negative, 2. there is not enough space for all elements from start index to end of given array
        if (target == null)
            throw new ArgumentNullException();
        if (fromIndex < 0)
            throw new ArgumentOutOfRangeException();
        if (target.Length < Count + fromIndex)
            throw new ArgumentException();

        int target_Index = fromIndex;
        foreach (T item in this) {
            target[target_Index] = item;
            target_Index++;
        }
    }

    public bool Contains(T x) => IndexOf(x) != -1;

    public int IndexOf(T item) {
        if (item == null) {
            for (int i = 0; i < Count; ++i)
                if (this[i] == null)
                    return i;
        } else {
            for (int i = 0; i < Count; ++i)
                if (item.Equals(this[i]))
                    return i;
        }
        return -1;
    }

    public void Insert(int index, T item) {
        if (index == Count) { // in order to evade argument out of range exception, if count is equal to index, redirect to add and return 
            Add(item); 
            return;
        }

        if (index < 0 || Count < index)
            throw new ArgumentOutOfRangeException();

        if (enumeration_In_Process)
            throw new InvalidOperationException();

        if( back.block == dataMap.Length || front.block == -1 )
            DoubleCapacity();

        if (index < (Count >> 1))
            shift_To_Front(index); // shift elements towards front
        else
            shift_To_Back(index); // shift elements towards end
        this[index] = item;
    }

    private void shift_To_Front(int up_to_a_point) {
        if (north_Is_North)
            front.Decrement();
        else 
            back.Increment();

        for (int i = 0; i <= up_to_a_point; ++i)
            this[i] = this[ i+1 ];
    }

    private void shift_To_Back(int up_to_a_point) {
        if (north_Is_North)
            back.Increment();
        else
            front.Decrement();

        for (int i = (Count-1 ); up_to_a_point < i; --i)
            this[i] = this[ i-1 ];
    }

    public bool Remove(T item) {
        if (enumeration_In_Process)
            throw new InvalidOperationException();

        if (item == null) {
            for (int i = 0; i < Count; ++i)
                if (this[i] == null) {
                    RemoveAt(i); // call own method 
                    return true;
                }
            return false;
        }

        for (int i = 0; i < Count; ++i)
            if (this[i].Equals(item)) {
                RemoveAt(i); // call own method 
                return true;
            }
        return false;
    }

    public void RemoveAt(int index) {
        if (enumeration_In_Process)
            throw new InvalidOperationException();

        if (IsReadOnly)
            throw new NotSupportedException();

        if (index < 0 || (Count - 1) < index)
            throw new ArgumentOutOfRangeException();

        if (index < (Count / 2)) {
            for (int i = index; 0 < i; --i)
                this[i] = this[i - 1];
            if (north_Is_North)
                front.Increment();
            else
                back.Decrement();
        } else {
            for (int i = index; i < (Count - 1); ++i)
                this[i] = this[i + 1];
            if (north_Is_North)
                back.Decrement();
            else
                front.Increment();
        }
    } // job done 😂😂😂

    public T RemoveFront() => OdeberZacatek();
    public void EnqueueFront(T item) => ZafrontiZacatek(item);
    public T RemoveBack() => OdeberKonec();
    public void EnqueueBack(T item) => ZafrontiKonec(item);
    public bool TryPeekFront(out T result) => ZkusNahlednoutZacatek(out result);
    public bool TryPeekBack(out T result) => ZkusNahlednoutKonec(out result);
    public T PeekFront() => NahledniZacatek();
    public T PeekBack() => NahledniKonec();

    public T OdeberZacatek() {
        if(Count == 0)
            throw new InvalidOperationException();

        T front = this[0];
        RemoveAt(0);
        return front;
    }

    public void ZafrontiZacatek(T item) => Insert(0, item);
    public void ZafrontiKonec(T item) => Add(item);

    public T OdeberKonec() {
        if (Count == 0)
            throw new InvalidOperationException();

        T back = this[Count-1];
        RemoveAt(Count-1);
        return back;
    }

    public bool ZkusNahlednoutZacatek(out T result) {
        if(Count == 0) { // set result to default value and return false 
            result = default(T);
            return false;
        }
        result = this[0];
        return true;
    }

    public bool ZkusNahlednoutKonec(out T result) {
        if (Count == 0) { // set result to default value and reutnr false 
            result = default(T);
            return false;
        }
        result = this[Count-1];
        return true;
    }

    public T NahledniZacatek() {
        if (Count == 0)
            throw new InvalidOperationException();
        return this[0];
    }

    public T NahledniKonec() {
        if (Count == 0)
            throw new InvalidOperationException();
        return this[Count-1];
    }
}