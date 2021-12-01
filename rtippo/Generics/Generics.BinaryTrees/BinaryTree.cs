using System;

using System.Collections;

using System.Collections.Generic;

using System.Linq;





namespace Generics.BinaryTrees
{
    public class BinaryNode<T> : IEnumerable<T>
        where T : IComparable
    {
        public T Value { get; set; }
        public BinaryNode<T> Left;
        public BinaryNode<T> Right;

        public void Add(T newValue)
        {
            if (Value == null) Value = newValue;
            else if (newValue.CompareTo(Value) > 0)
            {
                if (Right == null) Right = new BinaryNode<T> { Value = newValue };
                else Right.Add(newValue);
            }

            else
            {
                if (Left == null) Left = new BinaryNode<T> { Value = newValue };
                else Left.Add(newValue);
            };
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Left != null)
            {
                foreach (var node in Left)
                {
                    yield return node;
                }
            }
            if (Value != null) yield return Value;

            if (Right != null)
            {
                foreach (var node in Right)
                {
                    yield return node;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }



    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable
    {
        private BinaryNode<T> root;
        public T Value => root.Value;
        public BinaryNode<T> Left => root.Left;
        public BinaryNode<T> Right => root.Right;

        public void Add(T newValue)
        {
            if (root == null) root = new BinaryNode<T> { Value = newValue };
            else  root.Add(newValue);
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (root == null) return Enumerable.Empty<T>().GetEnumerator();
            return root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class BinaryTree
    {
        public static BinaryTree<T> Create<T>(params T[] list)
            where T : IComparable
        {
            var binaryTree = new BinaryTree<T>();

            foreach (var item in list)
            {
                binaryTree.Add(item);
            }
            return binaryTree;
        }
    }
}