using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
[Serializable]
public class Node<T>
{
    public T Data { get; set; }
    public Node<T>? Next { get; set; }
    public Node(T data)
    {
        Data = data;
        Next = null;
    }
}
public interface ILinkedListADT<T>
{
    void AddFirst(T item);
    void AddLast(T item);
    void Replace(int index, T item);
    void RemoveFirst();
    void RemoveLast();
    void Remove(int index);
    T GetValue(int index);
    int IndexOf(T item);
    bool Contains(T item);
    bool IsEmpty();
    void Clear();
    int Count();
    void Reverse();
    void Sort();
    T[] CopyToArray();
    void Join(ILinkedListADT<T> listToJoin);
    (ILinkedListADT<T>, ILinkedListADT<T>) Divide(int index);
}
[Serializable]
public class SLL<T> : ILinkedListADT<T> where T : IComparable<T>
{
    private Node<T>? head;
    private int count;
    public SLL()
    {
        head = null;
        count = 0;
    }
    public void Serialize(string filePath)
    {
        using (FileStream stream = File.Create(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }
    }
    public static SLL<T> Deserialize(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (SLL<T>)formatter.Deserialize(stream);
        }
    }
    public void AddFirst(T item)
    {
        Node<T> newNode = new Node<T>(item);
        newNode.Next = head;
        head = newNode;
        count++;
    }
    public void AddLast(T item)
    {
        Node<T> newNode = new Node<T>(item);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T>? current = head;
            while (current?.Next != null)
            {
                current = current?.Next;
            }
            current!.Next = newNode;
        }
        count++;
    }
    public void Replace(int index, T item)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException();
        Node<T>? current = head;
        for (int i = 0; i < index; i++)
        {
            current = current?.Next;
        }
        current!.Data = item;
    }
    public void RemoveFirst()
    {
        if (head == null)
            throw new InvalidOperationException("Cannot remove from an empty list.");
        head = head.Next;
        count--;
    }
    public void RemoveLast()
    {
        if (head == null)
            throw new InvalidOperationException("Cannot remove from an empty list.");
        if (count == 1)
        {
            head = null;
            count = 0;
            return;
        }
        Node<T>? current = head;
        while (current?.Next?.Next != null)
        {
            current = current?.Next;
        }
        current!.Next = null;
        count--;
    }
    public void Remove(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException();
        if (index == 0)
        {
            head = head?.Next;
            count--;
            return;
        }
        Node<T>? current = head;
        for (int i = 0; i < index - 1; i++)
        {
            current = current?.Next;
        }
        current!.Next = current?.Next?.Next;
        count--;
    }
    public T GetValue(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException();
        Node<T>? current = head;
        for (int i = 0; i < index; i++)
        {
            current = current?.Next;
        }
        return current!.Data;
    }
    public int IndexOf(T item)
    {
        Node<T>? current = head;
        int index = 0;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, item))
            {
                return index;
            }
            current = current.Next;
            index++;
        }
        return -1;
    }
    public bool Contains(T item)
    {
        return IndexOf(item) != -1;
    }
    public bool IsEmpty()
    {
        return count == 0;
    }
    public void Clear()
    {
        head = null;
        count = 0;
    }
    public int Count()
    {
        return count;
    }
    public void Reverse()
    {
        if (head == null || head.Next == null)
            return;
        Node<T>? prev = null;
        Node<T>? current = head;
        Node<T>? next = null;
        while (current != null)
        {
            next = current?.Next;
            current.Next = prev;
            prev = current;
            current = next;
        }
        head = prev;
    }
    public void Sort()
    {
        List<T> list = CopyToArray().ToList();
        list.Sort();
        Clear();
        foreach (var item in list)
        {
            AddLast(item);
        }
    }
    public T[] CopyToArray()
    {
        T[] array = new T[count];
        Node<T>? current = head;
        int index = 0;
        while (current != null)
        {
            array[index++] = current.Data;
            current = current.Next;
        }
        return array;
    }
    public void Join(ILinkedListADT<T> listToJoin)
    {
        if (listToJoin == null)
            throw new ArgumentNullException(nameof(listToJoin));
        if (head == null)
        {
            head = ((SLL<T>)listToJoin).head;
        }
        else
        {
            Node<T>? current = head;
            while (current?.Next != null)
            {
                current = current?.Next;
            }
            current!.Next = ((SLL<T>)listToJoin).head;
        }
        count += ((SLL<T>)listToJoin).count;
        listToJoin.Clear();
    }
    public (ILinkedListADT<T>, ILinkedListADT<T>) Divide(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException();
        SLL<T> list1 = new SLL<T>();
        SLL<T> list2 = new SLL<T>();
        Node<T>? current = head;
        for (int i = 0; i < index; i++)
        {
            list1.AddLast(current.Data);
            current = current?.Next;
        }
        while (current != null)
        {
            list2.AddLast(current.Data);
            current = current?.Next;
        }
        return (list1, list2);
    }
 
}
