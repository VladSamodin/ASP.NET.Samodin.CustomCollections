using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomCollections;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(0);
            tree.Add(2);
            tree.Add(1);
            tree.Add(-2);
            tree.Add(3);
            tree.Add(-3);
            tree.Add(-1);
            tree.Add(-4);
            PrintTree(tree);
             
            /*
            Random r = new Random(13);
            HashTable<int, string> hashTable = new HashTable<int, string>(10);
            for (int i = 0; i < 20; i++)
            {
                hashTable.Add(r.Next(), i.ToString());
            }
            r = new Random(13);
            for (int i = 0; i < 20; i++)
            {
                int j = r.Next();
                Console.WriteLine("Key = {0}\nValue = {1} ", j, hashTable.Get(j));
            }
            Console.WriteLine();
            foreach (var item in hashTable)
            {
                Console.WriteLine("Key = {0}\nValue = {1} ", item.Key, item.Value);
            }
            */
        }

        private static void PrintTree<T>(BinaryTree<T> tree)
        {
            foreach (var item in tree)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
        }
    }
}
