using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace Collections.Tests
{
    [TestFixture]
    public class OneDirectionListTests
    {
        #region OtherTests

        [Test]
        public void Empty_List()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act

            //assert
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(() => list[0], Throws.TypeOf<IndexOutOfRangeException>());
            Assert.That(() => list[0] = "aaa", Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Access_to_element_out_of_range()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");

            //assert
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(() => list[4], Throws.TypeOf<IndexOutOfRangeException>());

        }

        #endregion

        #region AddTests

        [Test]
        public void Add_one_element()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");

            //assert
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list[0], Is.EqualTo("xxx"));
        }

        [Test]
        public void Add_two_elements()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");

            //assert
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0], Is.EqualTo("xxx"));
            Assert.That(list[1], Is.EqualTo("yyy"));
            Assert.That(() => list[2], Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Add_and_modify_two_elements()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");

            //assert
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0], Is.EqualTo("xxx"));
            Assert.That(list[1], Is.EqualTo("yyy"));

            list[0] = "aaa";
            Assert.That(list[0], Is.EqualTo("aaa"));
            Assert.That(list[1], Is.EqualTo("yyy"));

            list[1] = "bbb";
            Assert.That(list[0], Is.EqualTo("aaa"));
            Assert.That(list[1], Is.EqualTo("bbb"));

            Assert.That(() => list[2] = "ccc", Throws.TypeOf<IndexOutOfRangeException>());
        }

        #endregion

        #region ClearTests

        [Test]
        public void Clear_empty_list()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Clear();

            //assert
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(() => list[0], Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Clear()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");

            //assert
            Assert.That(list.Count, Is.EqualTo(2));

            list.Clear();
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(() => list[0], Throws.TypeOf<IndexOutOfRangeException>());
        }

        #endregion

        #region ContainsTests

        [Test]
        public void Contains_on_empty_list()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act

            //assert
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(list.Contains("xxx"), Is.False);
        }

        [Test]
        public void Contains()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");

            //assert
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.Contains("xxx"), Is.True);
            Assert.That(list.Contains("yyy"), Is.True);
        }

        #endregion

        #region InsertTests

        [Test]
        public void Insert_to_empty_list_at_first_position()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act

            //assert
            Assert.That(() => list.Insert(0, "bbb"), Throws.TypeOf<IndexOutOfRangeException>());
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test]
        public void Insert_to_empty_list_at_not_first_position()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act

            //assert
            Assert.That(() => list.Insert(2, "bbb"), Throws.TypeOf<IndexOutOfRangeException>());
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test]
        public void Insert_two_elements()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");

            //assert
            Assert.That(list.Count, Is.EqualTo(2));
            list.Insert(1, "bbb");
            Assert.That(list[0], Is.EqualTo("xxx"));
            Assert.That(list[1], Is.EqualTo("bbb"));
            Assert.That(list[2], Is.EqualTo("yyy"));
            Assert.That(list.Count, Is.EqualTo(3));
            list.Insert(2, "ddd");
            Assert.That(list[0], Is.EqualTo("xxx"));
            Assert.That(list[1], Is.EqualTo("bbb"));
            Assert.That(list[2], Is.EqualTo("ddd"));
            Assert.That(list[3], Is.EqualTo("yyy"));
            Assert.That(list.Count, Is.EqualTo(4));
            Assert.That(() => list[4], Throws.TypeOf<IndexOutOfRangeException>());

        }

        [Test]
        public void Insert_at_first_position()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");

            //assert
            Assert.That(list.Count, Is.EqualTo(2));
            list.Insert(0, "bbb");
            Assert.That(list[0], Is.EqualTo("bbb"));
            Assert.That(list[1], Is.EqualTo("xxx"));
            Assert.That(list[2], Is.EqualTo("yyy"));
            Assert.That(list.Count, Is.EqualTo(3));
        }

        [Test]
        public void Insert_inside_list()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");
            list.Add("zzz");

            //assert
            Assert.That(list.Count, Is.EqualTo(3));
            list.Insert(2, "bbb");
            Assert.That(list[0], Is.EqualTo("xxx"));
            Assert.That(list[1], Is.EqualTo("yyy"));
            Assert.That(list[2], Is.EqualTo("bbb"));
            Assert.That(list[3], Is.EqualTo("zzz"));
            Assert.That(list.Count, Is.EqualTo(4));
        }

        [Test]
        public void Insert_at_last_position()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");
            list.Add("zzz");

            //assert
            Assert.That(list.Count, Is.EqualTo(3));
            list.Insert(2, "bbb");
            Assert.That(list[0], Is.EqualTo("xxx"));
            Assert.That(list[1], Is.EqualTo("yyy"));
            Assert.That(list[2], Is.EqualTo("bbb"));
            Assert.That(list[3], Is.EqualTo("zzz"));
            Assert.That(list.Count, Is.EqualTo(4));
        }

        [Test]
        public void Insert_to_list_out_of_range()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");

            //assert
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(() => list.Insert(2, "bbb"), Throws.TypeOf<IndexOutOfRangeException>());
            Assert.That(list.Count, Is.EqualTo(2));

        }

        #endregion

        #region CopyTo

        [Test]
        public void CopyTo_empty_list()
        {
            //arrange
            var list = new OneDirectionList<string>();
            string[] array = new string[list.Count];

            //act

            //assert
            Assert.That(() => list.CopyTo(array, 0), Throws.TypeOf<IndexOutOfRangeException>());
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(array.Length, Is.EqualTo(0));
        }

        [Test]
        public void CopyTo_two_element_list_on_first_position()
        {
            //arrange
            var list = new OneDirectionList<string>();
            list.Add("xxx");
            list.Add("yyy");
            string[] array = new string[list.Count];

            //act
            Assert.That(list.Count, Is.EqualTo(2));
            list.CopyTo(array, 0);

            //assert
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(array[0], Is.EqualTo("xxx"));
            Assert.That(array[1], Is.EqualTo("yyy"));
            Assert.That(array.Length, Is.EqualTo(2));
        }

        [Test]
        public void CopyTo_two_element_list_on_position_out_of_range()
        {
            //arrange
            var list = new OneDirectionList<string>();
            list.Add("xxx");
            list.Add("yyy");
            string[] array = new string[list.Count];

            //act
            Assert.That(list.Count, Is.EqualTo(2));

            //assert
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(() => list.CopyTo(array, 2), Throws.TypeOf<IndexOutOfRangeException>());
        }

        #endregion

        #region RemoveTests

        [Test]
        public void Remove_element_from_empty_list()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act

            //assert
            Assert.That(() => list.Remove("xxx"), Is.False);
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test]
        public void Remove_not_existed_element()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");

            //assert
            Assert.That(() => list.Remove("zzz"), Is.False);
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void Remove_element_from_first_position()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");
            list.Add("zzz");
            list.Add("dfg");

            //assert
            Assert.That(list.Count, Is.EqualTo(4));
            list.Remove("xxx");
            Assert.That(list[0], Is.EqualTo("yyy"));
            Assert.That(list[1], Is.EqualTo("zzz"));
            Assert.That(list[2], Is.EqualTo("dfg"));
            Assert.That(list.Count, Is.EqualTo(3));
        }

        [Test]
        public void Remove_element_from_inside()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");
            list.Add("zzz");
            list.Add("dfg");

            //assert
            Assert.That(list.Count, Is.EqualTo(4));
            list.Remove("yyy");
            Assert.That(list[0], Is.EqualTo("xxx"));
            Assert.That(list[1], Is.EqualTo("zzz"));
            Assert.That(list[2], Is.EqualTo("dfg"));
            Assert.That(list.Count, Is.EqualTo(3));
        }

        [Test]
        public void Remove_element_from_last_position()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");
            list.Add("zzz");
            list.Add("dfg");

            //assert
            Assert.That(list.Count, Is.EqualTo(4));
            list.Remove("dfg");
            Assert.That(list[0], Is.EqualTo("xxx"));
            Assert.That(list[1], Is.EqualTo("yyy"));
            Assert.That(list[2], Is.EqualTo("zzz"));
            Assert.That(list.Count, Is.EqualTo(3));
        }

        [Test]
        public void Remove_each_element()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");
            list.Add("zzz");
            list.Add("dfg");

            //assert
            Assert.That(list.Count, Is.EqualTo(4));
            list.Remove("yyy");
            Assert.That(list[0], Is.EqualTo("xxx"));
            Assert.That(list[1], Is.EqualTo("zzz"));
            Assert.That(list[2], Is.EqualTo("dfg"));
            Assert.That(list.Count, Is.EqualTo(3));
            list.Remove("xxx");
            Assert.That(list[0], Is.EqualTo("zzz"));
            Assert.That(list[1], Is.EqualTo("dfg"));
            Assert.That(list.Count, Is.EqualTo(2));
            list.Remove("dfg");
            Assert.That(list[0], Is.EqualTo("zzz"));
            Assert.That(list.Count, Is.EqualTo(1));
            list.Remove("zzz");
            Assert.That(list.Count, Is.EqualTo(0));
        }

        #endregion

        #region RemoveAtTests

        [Test]
        public void RemoveAt_from_empty_list_at_first_position()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act

            //assert
            Assert.That(() => list.RemoveAt(0), Throws.TypeOf<IndexOutOfRangeException>());
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveAt_from_empty_list_index_out_of_range()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act

            //assert
            Assert.That(() => list.RemoveAt(3), Throws.TypeOf<IndexOutOfRangeException>());
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveAt_element_from_one_element_list()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.RemoveAt(0);

            //assert
            Assert.That(list.Contains("xxx"), Is.False);
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveAt_element_from_multi_element_list_valid_index()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");

            //assert
            Assert.That(() => list.RemoveAt(3), Throws.TypeOf<IndexOutOfRangeException>());
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void RemoveAt_element_from_multi_element_list_index_out_of_range()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");
            list.RemoveAt(0);

            //assert
            Assert.That(list.Contains("xxx"), Is.False);
            Assert.That(list.Count, Is.EqualTo(1));
        }

        #endregion

        #region EnumeratorTests

        [Test]
        public void Enumerator()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");
            list.Add("zzz");

            //assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First(), Is.EqualTo("xxx"));
            Assert.That(list.Skip(1).First(), Is.EqualTo("yyy"));
            Assert.That(list.Skip(2).First(), Is.EqualTo("zzz"));
        }

        [Test]
        public void Enumerator_foreach()
        {
            //arrange
            var list = new OneDirectionList<string>();

            //act
            list.Add("xxx");
            list.Add("yyy");
            list.Add("zzz");

            //assert
            foreach (var item in list)
                Debug.WriteLine(item);
        }

        #endregion
    }
}
