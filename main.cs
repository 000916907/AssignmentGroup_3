using System;
class Program
{
    static void Main()
    {
        // Example usage of SLL with int
        SLL<int> intList = new SLL<int>();
        // Add elements
        intList.AddLast(1);
        intList.AddLast(2);
        intList.AddLast(3);
        // Display elements
        Console.WriteLine("Original List:");
        DisplayList(intList);
        // Reverse the list
        intList.Reverse();
        Console.WriteLine("\nReversed List:");
        DisplayList(intList);
        // Sort the list
        intList.Sort();
        Console.WriteLine("\nSorted List:");
        DisplayList(intList);
        // Serialize and deserialize the list
        string filePath = "list.bin";
        intList.Serialize(filePath);
        SLL<int> deserializedList = SLL<int>.Deserialize(filePath);
        Console.WriteLine("\nDeserialized List:");
        DisplayList(deserializedList);
        
    }
    static void DisplayList<T>(ILinkedListADT<T> list)
    {
        for (int i = 0; i < list.Count(); i++)
        {
            Console.Write(list.GetValue(i) + " ");
            Console.ReadLine();
        }
        Console.WriteLine();

       
    }
}
