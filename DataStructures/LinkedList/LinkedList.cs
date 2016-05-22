using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public class LinkedList<T> : ICollection<T>
    {
        public LinkedListNode<T> Head { get; private set; }
        public LinkedListNode<T> Tail { get; private set; }
        public int Count { get; set; }

        /// <summary>
        /// Create a node from a value and add it to the front of a list.
        /// </summary>
        /// <param name="value"></param>
        public void AddFirst (T value)
        {
            AddFirst(new LinkedListNode<T>(value));
        }

        /// <summary>
        /// Add a node to the front of the list.
        /// </summary>
        /// <param name="node"></param>
        public void AddFirst (LinkedListNode<T> node)
        {
            LinkedListNode<T> temp = Head;
            Head = node;
            Head.Next = temp;
            Count++;

            if (Count == 1)
            {
                Tail = Head;
            }
        }

        /// <summary>
        /// Create a node from a value and add it to the end of a list.
        /// </summary>
        /// <param name="value"></param>
        public void AddLast (T value)
        {
            AddLast(new LinkedListNode<T>(value));
        }

        /// <summary>
        /// Add a node to the end of the list.
        /// </summary>
        /// <param name="node"></param>
        public void AddLast (LinkedListNode<T> node)
        {
            if (Count == 0)
            {
                Head = node;
            }
            else
            {
                Tail.Next = node;
            }
            Tail = node;
            Count++;
        }

        /// <summary>
        /// Remove the first element of the list.
        /// </summary>
        public void RemoveFirst ()
        {
            if (Count != 0)
            {
                Head = Head.Next;
                Count--;

                if (Count == 0)
                {
                    Tail = null;
                }
            }
        }

        /// <summary>
        /// Remove the last element of the list.
        /// </summary>
        public void RemoveLast ()
        {
            if (Count != 0)
            {
                if (Count == 1)
                {
                    Head = null;
                    Tail = null;
                }
                else
                {
                    LinkedListNode<T> current = Head;
                    while (current.Next != Tail)
                    {
                        current = current.Next;
                    }

                    current.Next = null;
                    Tail = current;
                }

                Count--;
            }
        }

        // ICollection
        /// <summary>
        /// Add a new element to the front of the list.
        /// </summary>
        /// <param name="item"></param>
        public void Add (T item)
        {
            AddFirst(item);
        }

        /// <summary>
        /// Check if the list contains the given element.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains (T item)
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// Copy the values of the linked list into an array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo (T[] array, int arrayIndex)
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        /// <summary>
        /// Return the true if the list is readonly.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Remove the first occurance of an element from the list.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove (T item)
        {
            LinkedListNode<T> previous = null;
            LinkedListNode<T> current = Head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    // A node in the middle or end
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        if (current.Next == null)
                        {
                            Tail = previous;
                        }

                        Count--;
                    }
                    else
                    {
                        RemoveFirst();
                    }

                    return true;
                }

                previous = current;
                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// Enumerates over the linked list values from Head to Tail.
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator ()
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        /// <summary>
        /// Enumerates over the linked list values from Head to Tail.
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
        {
            return ((System.Collections.Generic.IEnumerable<T>) this).GetEnumerator();
        }

        /// <summary>
        /// Removes all the nodes from the list.
        /// </summary>
        public void Clear ()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }
    }
}
