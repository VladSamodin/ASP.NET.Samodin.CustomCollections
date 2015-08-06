using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollections
{

    public class LinkedListNode<T>
    {
        public T Value { get; set; }
        public LinkedListNode<T> Previous { get; set; }
        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T value, LinkedListNode<T> previous, LinkedListNode<T> next)
        {
            Value = value;
            Previous = previous;
            Next = next;
        }
    }

    public class LinkedList<T> : IEnumerable<T>
    {
        private LinkedListNode<T> head;
        public int Count { get; private set; }

        public LinkedListNode<T> First
        {
            get
            {
                return head;
            }
        }

        public LinkedListNode<T> Last
        {
            get
            {
                if (head == null)
                {
                    return null;
                }
                LinkedListNode<T> last = head;
                while (last.Next != null)
                {
                    last = last.Next;
                }
                return last;
            }
        }

        public LinkedList() 
        {
            head = null;
        }

        public void AddBefore(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> previous = node.Previous;
            LinkedListNode<T> toAdd = new LinkedListNode<T>(value, previous, node);
            if (previous != null)
            {
                previous.Next = toAdd;
            }
            else
            {
                head = toAdd;
            }
            node.Previous = toAdd;
            Count++;
        }

        public void AddAfter(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> next = node.Next;
            LinkedListNode<T> toAdd = new LinkedListNode<T>(value, node, next);
            if (next != null)
            {
                next.Previous = toAdd;
            }
            node.Next = toAdd;
            Count++;
        }

        public void AddHead(T value)
        {
            LinkedListNode<T> toAdd = new LinkedListNode<T>(value, null, head);
            if (head != null)
            {
                head.Previous = toAdd;
            }
            head = toAdd;
            Count++;
        }

        public void AddTail(T value)
        {
            if (head == null)
            {
                head = new LinkedListNode<T>(value, null, null);
                return;
            }
            LinkedListNode<T> last = head;
            while (last.Next != null)
            {
                last = last.Next;
            }
            LinkedListNode<T> toAdd = new LinkedListNode<T>(value, last, null);
            last.Next = toAdd;
            Count++;
        }


        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        public LinkedListNode<T> Find(T value)
        {
            LinkedListNode<T> current = head;
            while (current != null && !current.Value.Equals(value))
            {
                current = current.Next;
            }
            return current;
        }

        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        public void Remove(LinkedListNode<T> node)
        {
            if (node == null)
            {
                new ArgumentNullException("node");
            }
            LinkedListNode<T> previous = node.Previous;
            LinkedListNode<T> next = node.Next;
            if (previous != null)
            {
                previous.Next = next;
            }
            else
            {
                head = next;
            }
            if (next != null)
            {
                next.Previous = previous;
            }
            Count--;
        }

        public void Clear()
        {
            head = null;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
