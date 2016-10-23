using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    public class PriorityQueue<T> : IEnumerable<T> where T : IEquatable<T>
    {
        private SortedList<int, Queue<T>> innerQueue;
        private int count;

        public PriorityQueue()
        {
            innerQueue = new SortedList<int, Queue<T>>(new ReversedIntComparer());
        }

        public T Dequeue()
        {
            if (innerQueue.Count == 0)
                throw new InvalidOperationException();

            var priority = innerQueue.Keys[0];
            var queue = innerQueue[priority];
            var value = queue.Dequeue();

            if (queue.Count == 0)
                innerQueue.Remove(priority);

            count--;
            return value;
        }

        public void Enqueue(int priority, T value)
        {
            if (innerQueue.ContainsKey(priority))
            {
                innerQueue[priority].Enqueue(value);
            }
            else
            {
                innerQueue.Add(priority, new Queue<T>());
                innerQueue[priority].Enqueue(value);
            }
            count++;
        }

        public T Peek()
        {
            if (innerQueue.Count == 0)
                throw new InvalidOperationException();

            var priority = innerQueue.Keys[0];
            var queue = innerQueue[priority];
            return queue.Peek();

        }

        public void Clear()
        {
            innerQueue.Clear();
        }

        public int Count => count;

        #region IEnumerable<TValue> members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new PriorityQueueEnumerator(this);
        }

        #endregion

        private class PriorityQueueEnumerator : IEnumerator<T>
        {
            private PriorityQueue<T> priorityQueue;

            private IEnumerator<KeyValuePair<int, Queue<T>>> outerEnumerator;
            private IEnumerator<T> innerEnumerator;

            public PriorityQueueEnumerator(PriorityQueue<T> priorityQueue)
            {
                this.priorityQueue = priorityQueue;
            }

            public bool MoveNext()
            {
                if (outerEnumerator == null)
                {
                    outerEnumerator = priorityQueue.innerQueue.GetEnumerator();

                    if (!outerEnumerator.MoveNext())
                        return false;
                }

                if (innerEnumerator == null)
                    innerEnumerator = outerEnumerator.Current.Value.GetEnumerator();

                if (innerEnumerator.MoveNext())
                    return true;
                else
                {
                    if (!outerEnumerator.MoveNext())
                    {
                        return false;
                    }
                    else
                    {
                        innerEnumerator = null;
                        return MoveNext();
                    }
                }
            }

            public void Reset()
            {
                outerEnumerator = null;
                innerEnumerator = null;
            }

            public T Current => innerEnumerator.Current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        private class ReversedIntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (x == y)
                    return 0;

                if (x > y)
                    return -1;

                return 1;
            }
        }
    }
}
