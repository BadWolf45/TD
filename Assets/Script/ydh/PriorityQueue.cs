using System;
using System.Collections.Generic;

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}

public class PriorityQueue<T> where T : IHeapItem<T>//���ʸ� Ÿ��. � Ÿ���� �޾Ƽ� ������. IHeapItem�������̽��� �����ؾ���.
    //where : ��������. ������ ������Ʈ ���� �� ����� �ݵ�� �������㰡 �־�� ��!�� public class Project<T> where T : IDriver
{
    //F = G + H ���� ���� ���� Node�� ���� ���� �� �ֵ��� �����ݴϴ�.
    private List<T> items = new();//�ּ� �� ������ ����Ǹ�,���� ���� ���� items[0]�� ��ġ�մϴ�.

    public int Count => items.Count;//ť�� ����ִ� ��� ������ ��ȯ�մϴ�.

    public void Enqueue(T item)//�� �׸��� �� �ڿ� �߰��մϴ�. Binary Heap
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

    private void SortUp(T item)//�θ� ���� ���� ������ ���� �ö�.
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