using System;
using System.Collections;
using System.Collections.Generic;

namespace PriorityQueue
{
    public class OneDirectionList<T> : IList<T>
    {
        private Node head;
        private int count;

        public void Add(T value)
        {
            if (head == null)
            {
                var newNode = new Node(value);
                head = newNode;
            }
            else
            {
                var node = head;
                while (node.Next != null)
                {
                    node = node.Next;
                }
                node.Next = new Node(value);
            }
            count++;
        }

        public void Clear()
        {
            head = null;
            count = 0;
        }

        public bool Contains(T value)
        {
            if (head == null)
                return false;

            var currentNode = head;

            for (int i = 0; i < count; i++)
            {
                if (currentNode.Value.Equals(value))
                    return true;
                currentNode = currentNode.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (head == null)
                throw new IndexOutOfRangeException();

            var currentNode = head;
            int index = arrayIndex;
            if (index == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    array.SetValue(currentNode.Value, index);
                    index++;
                    currentNode = currentNode.Next;
                }
            }
            else
                throw new IndexOutOfRangeException();
        }

        public int IndexOf(T value)
        {
            if (head == null)
                return -1;

            var currentNode = head;
            int i;

            for (i = 0; i < count; i++)
            {
                if (currentNode.Value.Equals(value))
                    return i;
                currentNode = currentNode.Next;
            }
            return i;
        }

        public void Insert(int index, T value)
        {
            if (index >= count)
                throw new IndexOutOfRangeException();

            if (head.Next == null && index == 0)
            {
                var temp = head;
                head = new Node(value);
                head.Next = temp;
                count++;
                return;
            }

            if (index == 0)
            {
                var temp = head;
                head = new Node(value);
                head.Next = temp;
                count++;
                return;
            }

            var previousNode = head;
            var currentNode = head.Next;

            for (int i = 0; i <= index - 1; i++)
            {
                if (i == index - 1)
                {
                    var temp = currentNode;
                    previousNode.Next = new Node(value);
                    previousNode.Next.Next = temp;
                    count++;
                    break;
                }
                if (currentNode == null)
                    throw new IndexOutOfRangeException();

                previousNode = currentNode;
                currentNode = currentNode.Next;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T value)
        {
            if (head == null)
                return false;

            if (count == 1 && head.Value.Equals(value))
            {
                head = null;
                count--;
                return true;
            }

            var previousNode = head;
            var currentNode = head.Next;

            for (int i = 1; i < count; i++)
            {
                if (currentNode != null && (currentNode.Value.Equals(value) || previousNode.Value.Equals(value)))
                {
                    if (previousNode.Value.Equals(value))
                        head = currentNode;

                    if (currentNode.Value.Equals(value))
                        previousNode.Next = currentNode.Next;

                    count--;
                    return true;
                }
                if (currentNode == null)
                    throw new IndexOutOfRangeException();

                previousNode = currentNode;
                currentNode = currentNode.Next;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (head == null)
                throw new IndexOutOfRangeException();

            if (count == 1 && index == 0)
            {
                head = null;
                count--;
                return;
            }

            var previousNode = head;
            var currentNode = head.Next;

            if (count == 2 && index == 0)
            {
                head = currentNode;
                count--;
                return;
            }
            if (count == 2 && index == 1)
            {
                head.Next = null;
                count--;
                return;
            }
            if (count > 2 && index <= count - 1)
            {
                if (index == 0)
                {
                    head = currentNode;
                    count--;
                    return;
                }
                for (int i = 1; i <= index; i++)
                {
                    if (i == index && currentNode != null && previousNode != null)
                    {
                        previousNode.Next = currentNode.Next;
                        count--;
                    }

                    if (currentNode == null)
                        throw new IndexOutOfRangeException();

                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }
            }
            else
                throw new IndexOutOfRangeException();
        }

        public T this[int index]
        {
            get
            {
                if (index == 0)
                {
                    if (head == null)
                        throw new IndexOutOfRangeException();

                    return head.Value;
                }
                if (index >= count || index < 0)
                    throw new IndexOutOfRangeException();

                var currentNode = head;

                for (int i = 1; i <= index; i++)
                    currentNode = currentNode.Next;

                return currentNode.Value;
            }
            set
            {
                if (index == 0)
                {
                    if (head == null)
                        throw new IndexOutOfRangeException();

                    head.Value = value;
                }

                if (index >= count || index < 0)
                    throw new IndexOutOfRangeException();

                var currentNode = head;

                for (int i = 1; i <= index; i++)
                    currentNode = currentNode.Next;

                currentNode.Value = value;
            }
        }

        public int Count
        {
            get { return count; }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new OneDirectionListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        private class Node
        {
            public T Value;
            public Node Next;

            public Node(T value)
            {
                Value = value;
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        private class OneDirectionListEnumerator : IEnumerator<T>
        {
            private OneDirectionList<T> list;
            private Node currentNode;

            public OneDirectionListEnumerator(OneDirectionList<T> list)
            {
                this.list = list;
            }

            public bool MoveNext()
            {
                if (currentNode != null)
                    currentNode = currentNode.Next;
                else
                    currentNode = list.head;

                return currentNode != null;
            }

            public void Reset()
            {
                currentNode = null;
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public T Current
            {
                get { return currentNode.Value; }
            }

            public void Dispose()
            {
            }
        }
    }
}