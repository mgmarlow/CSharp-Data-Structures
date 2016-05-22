using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Stack
{
    public class Stack<T> : IEnumerable<T>
    {
        // The array will grow as needed during Push
        T[] _items = new T[0];

        // Current number of items in stack
        int _size;

        public int Count => _size;

        /// <summary>
        /// Adds an item to the stack.
        /// </summary>
        /// <param name="item"></param>
        public void Push (T item)
        {
            if (_size == _items.Length)
            {
                // Double current length (initial size of 4)
                int newLength = _size == 0 ? 4 : _size * 2;

                T[] newArray = new T[newLength];
                _items.CopyTo(newArray, 0);
                _items = newArray;
            }

            _items[_size] = item;
            _size++;
        }

        /// <summary>
        /// Removes and returns the top item from the stack.
        /// </summary>
        /// <returns></returns>
        public T Pop ()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            _size--;
            return _items[_size];
        }

        /// <summary>
        /// Returns the top items from the stack.
        /// </summary>
        /// <returns></returns>
        public T Peek ()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            return _items[_size - 1];
        }

        /// <summary>
        /// Removes all items from the stack.
        /// Note: This doesn't clear references, so not recommended for stacks that hold
        /// reference data.
        /// </summary>
        public void Clear ()
        {
            _size = 0;
        }

        /// <summary>
        /// Enumerates each item in the stack.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator ()
        {
            for (int i = _size - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        /// <summary>
        /// Enumerates each item in the stack.
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
        {
            return GetEnumerator();
        } 
    }
}
