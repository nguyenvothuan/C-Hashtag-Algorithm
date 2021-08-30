using System.Collections.Generic;

class MinHeap
{
    public int[] HeapArray { get; }
    public int Capacity { get; }
    private int Count { get; set; }
    public MinHeap(int n)
    {
        HeapArray = new int[n];
        Capacity = n;
        Count = 0;
    }
    public int Parent(int key)
    {
        return (key - 1) / 2;
    }

    public int Left(int key)
    {
        return 2 * key + 1;
    }

    public int Right(int key)
    {
        return 2 * key + 2;
    }
    private void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    public bool Insert(int key)
    {
        if (Count == Capacity) return false;
        int i = Count;
        HeapArray[i] = key;
        while (i != 0 && HeapArray[i] < HeapArray[Parent(i)])
        {
            Swap<int>(ref HeapArray[i], ref HeapArray[Parent(i)]);
            i = Parent(i);
        }
        return true;
    }

    public bool Delete(int key)
    {
        if (Count < key + 1) return false;
        DecreaseKey(key, int.MinValue);
        ExtractMin();
        return true;
    }


    public void DecreaseKey(int key, int val)
    {
        if (key >= Count)
            throw new System.Exception("Index out of bound, bitch");


        while (key != 0 && HeapArray[key] <
                           HeapArray[Parent(key)])
        {
            Swap(ref HeapArray[key],
                 ref HeapArray[Parent(key)]);
            key = Parent(key);
        }
    }

    public void IncreaseKey(int key, int val)
    {
        if (key >= Count)
            throw new System.Exception("Index out of bound, bitch");
        HeapArray[key] = val;
        MinHeapify(key);
    }

    public int GetMin() { return HeapArray[0]; }
    public int ExtractMin()
    {
        if (Count == 0) return int.MaxValue;
        if (Count == 1)
        {
            Count = 0;
            int temp = HeapArray[0];
            HeapArray[0] = int.MaxValue;
        }
        int root = HeapArray[0];
        HeapArray[0] = HeapArray[Count - 1];
        HeapArray[Count - 1] = int.MaxValue;
        MinHeapify(0);
        return root;
    }


    private void MinHeapify(int root)//heapify at the given index
    {
        int l = Left(root);
        int r = Right(root);
        int smallest = root;
        if (HeapArray[l] < smallest && l < Count)
        {
            smallest = l;
        }
        if (HeapArray[r] < smallest && r < Count)
        {
            smallest = r;
        }
        if (smallest != root)
        {
            Swap<int>(ref HeapArray[root], ref HeapArray[smallest]);

            MinHeapify(smallest);
        }
    }

    public void SetValue(int key, int val)
    {
        if (HeapArray[key] == val)
        {
            return;
        }
        if (HeapArray[key] < val)
        {
            IncreaseKey(key, val);
        }
        else
        {
            DecreaseKey(key, val);
        }
    }

    
}