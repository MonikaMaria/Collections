using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Collections.Tests
{
    [TestFixture]
    public class PriorityQueueTests
    {
        [Test]
        public void Empty_queue_count_is_equal_to_zero()
        {
            // arrange
            var queue = new PriorityQueue<int>();

            // act
            // tym razem nic nie robimy

            // assert
            Assert.AreEqual(0, queue.Count); // "stara" składnia
            Assert.That(queue.Count, Is.EqualTo(0)); // "nowa"/"fluentowa" składnia
        }

        [Test]
        public void Empty_queue_enumerator_is_empty()
        {
            // arrange
            var queue = new PriorityQueue<int>();

            // act
            // tym razem nic nie robimy

            // assert
            CollectionAssert.IsEmpty(queue);
            Assert.That(queue, Is.Empty);
        }

        [Test]
        public void Enqueue_two_elements()
        {
            //arrange
            var queue = new PriorityQueue<int>();

            //act
            queue.Enqueue(1, 2);
            queue.Enqueue(2, 2);
            queue.Enqueue(3, 1);

            //assert
            Assert.AreEqual(3, queue.Count);

            var value = queue.Dequeue();
            Assert.That(value, Is.EqualTo(1));

            value = queue.Dequeue();
            Assert.That(value, Is.EqualTo(2));

            value = queue.Dequeue();
            Assert.That(value, Is.EqualTo(2));

            Assert.AreEqual(0, queue.Count);
        }

        #region Enumerator

        [Test]
        public void Enumerator()
        {
            //arrange
            var queue = new PriorityQueue<int>();

            //act
            queue.Enqueue(1, 2);
            queue.Enqueue(2, 2);
            queue.Enqueue(3, 1);

            //assert
            Assert.AreEqual(3, queue.Count);
            var value = queue.Dequeue();
            Assert.That(value, Is.EqualTo(1));
        }

        [Test]
        public void Enumerator_foreach()
        {
            //arrange
            var queue = new PriorityQueue<int>();

            //act
            queue.Enqueue(1, 2);
            queue.Enqueue(2, 2);
            queue.Enqueue(3, 1);
            queue.Enqueue(3, 5);


            //assert
            Assert.AreEqual(4, queue.Count);
            foreach (var item in queue)
                Debug.WriteLine(item);
        }

        #endregion

        #region PeekTests

        [Test]
        public void Peek_from_empty_queue()
        {
            //arrange
            var queue = new PriorityQueue<int>();

            //act

            //assert
            Assert.AreEqual(0, queue.Count);
            Assert.That(() => queue.Peek(), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Peek()
        {
            //arrange
            var queue = new PriorityQueue<int>();

            //act
            queue.Enqueue(1, 2);
            queue.Enqueue(2, 2);
            queue.Enqueue(3, 1);

            //assert
            Assert.AreEqual(3, queue.Count);
            Assert.That(queue.Peek().ToString(), Is.EqualTo("1"));

            queue.Dequeue();
            Assert.That(queue.Peek().ToString(), Is.EqualTo("2"));
            Assert.AreEqual(2, queue.Count);

            queue.Enqueue(5, 5);
            Assert.That(queue.Peek().ToString(), Is.EqualTo("5"));
            Assert.AreEqual(3, queue.Count);

        }
    }

    #endregion
}


