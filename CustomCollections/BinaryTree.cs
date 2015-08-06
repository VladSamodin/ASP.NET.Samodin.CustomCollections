using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollections
{
    public class BinaryTree<T> : IEnumerable<T>
    {
       
        private TreeNode root;
        private Comparer<T> comparer;
        public int Count { get; private set; }

        public BinaryTree(Comparer<T> comparer = null)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
            Count = 0;
        }

        public void Add(T value)
        {
            if (root == null)
            {
                root = new TreeNode(value, null);
                Count++;
                return;
            }
            TreeNode previous = null;
            TreeNode current = root;
            while (current != null)
            {
                previous = current;
                if (comparer.Compare(value, current.Value) < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }
            if (comparer.Compare(value, previous.Value) < 0)
                previous.Left = new TreeNode(value, previous);
            else
                previous.Right = new TreeNode(value, previous);
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetPreorderEnumerator();
        }

        public IEnumerator<T> GetPreorderEnumerator()
        {
            return GetPreorderEnumerator(root);
        }

        public IEnumerator<T> GetInorderEnumerator()
        {
            return GetInorderEnumerator(root);
        }

        public IEnumerator<T> GetPostorderEnumerator()
        {
            return GetPostorderEnumerator(root);
        }

        private IEnumerator<T> GetPreorderEnumerator(TreeNode current)
        {
            if (current != null)
            {
                yield return current.Value;
                IEnumerator<T> enumerator = GetPreorderEnumerator(current.Left);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
                enumerator = GetPreorderEnumerator(current.Right);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        private IEnumerator<T> GetInorderEnumerator(TreeNode current)
        {
            if (current != null)
            {
                IEnumerator<T> enumerator = GetPreorderEnumerator(current.Left);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
                yield return current.Value;
                enumerator = GetPreorderEnumerator(current.Right);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        private IEnumerator<T> GetPostorderEnumerator(TreeNode current)
        {
            if (current != null)
            {
                IEnumerator<T> enumerator = GetPreorderEnumerator(current.Left);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }  
                enumerator = GetPreorderEnumerator(current.Right);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
                yield return current.Value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class TreeNode
        {
            public T Value { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
            public TreeNode Parent { get; set; }

            public TreeNode(T value, TreeNode parent)
            {
                Value = value;
                Left = null;
                Right = null;
                Parent = parent;
            }
        }
    }
}
