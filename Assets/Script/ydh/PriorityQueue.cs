using System;
using System.Collections.Generic;

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}

public class PriorityQueue<T> where T : IHeapItem<T>//제너릭 타입. 어떤 타입을 받아서 동작함. IHeapItem인터페이스를 구현해야함.
    //where : 제약조건. “나랑 프로젝트 같이 할 사람은 반드시 운전면허가 있어야 해!” public class Project<T> where T : IDriver
{
    //F = G + H 값이 가장 작은 Node를 먼저 꺼낼 수 있도록 도와줍니다.
    private List<T> items = new();//최소 힙 구조로 저장되며,가장 적은 값이 items[0]에 위치합니다.

    public int Count => items.Count;//큐에 들어있는 요소 개수를 반환합니다.

    public void Enqueue(T item)//새 항목을 맨 뒤에 추가합니다. Binary Heap
    {
        item.HeapIndex = items.Count;
        items.Add(item);
        SortUp(item);
    }

    public T Dequeue()
    {
        T firstItem = items[0];
        int lastIndex = items.Count - 1;
        items[0] = items[lastIndex];
        items[0].HeapIndex = 0;
        items.RemoveAt(lastIndex);
        SortDown(items[0]);
        return firstItem;
    }

    private void SortUp(T item)//부모 노드와 비교해 작으면 위로 올라감.
    {
        int parentIndex;
        while ((parentIndex = (item.HeapIndex - 1) / 2) >= 0)
        {
            T parent = items[parentIndex];
            if (item.CompareTo(parent) < 0)
                Swap(item, parent);
            else
                break;
        }
    }

    private void SortDown(T item)
    {
        while (true)
        {
            int leftChild = item.HeapIndex * 2 + 1;
            int rightChild = item.HeapIndex * 2 + 2;
            int swapIndex = item.HeapIndex;

            if (leftChild < Count && items[leftChild].CompareTo(items[swapIndex]) < 0)
                swapIndex = leftChild;

            if (rightChild < Count && items[rightChild].CompareTo(items[swapIndex]) < 0)
                swapIndex = rightChild;

            if (swapIndex != item.HeapIndex)
                Swap(item, items[swapIndex]);
            else
                break;
        }
    }

    private void Swap(T a, T b)
    {
        items[a.HeapIndex] = b;
        items[b.HeapIndex] = a;
        (a.HeapIndex, b.HeapIndex) = (b.HeapIndex, a.HeapIndex);
    }
}