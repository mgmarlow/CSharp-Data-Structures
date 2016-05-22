using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Queue
{
    public class Queue<T> : IEnumerable<T>
    {
        T[] _items = new T[0];

        int _size = 0;

        // Index of oldest item in queue
        int _head = 0;

        // Index of newest item in queue
        int _tail = -1;

        /// <summary>
        /// Number of items in the queue.
        /// </summary>
        public int Count => _size;

        /// <summary>
        /// Add a new item into the queue.
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue (T item)
        {
            // If we need to grow the array
            if (_items.Length == _size)
            {
                int newLength = (_size == 0) ? 4 : _size * 2;
                T[] newArray = new T[newLength];

                if (_size > 0)
                {
                    // Copy contents
                    int targetIndex = 0;
                    if (_tail < _head)
                    {
                        // Wrap
                        for (int index = _head; index < _items.Length; index++)
                        {
                            newArray[targetIndex] = _items[index];
                            targetIndex++;
                        }

                        for (int index = 0; index <= _tail; index++)
                        {
                            newArray[targetIndex] = _items[index];
                            targetIndex++;
                        }
                    }
                    else
                    {
                        // No wrap
                         for (int index = _head; index < _items.Length; index++)
                        {
                            newArray[targetIndex] = _items[index];
                            targetIndex++;
                        }                       
                    }

                    _head = 0;
                    _tail = targetIndex - 1;  // Compensate for extra bump
                }

                _items = newArray;
            }

            // If the tail is at the end of the array, begin wrapping
            if (_tail == _items.Length - 1)
            {
                _tail = 0;
            }
            else
            {
                _tail++;
            }

            _items[_tail] = item;
        }

        /// <summary>
        /// Remove and return the head item from the queue.
        /// </summary>
        /// <returns></returns>
        public T Dequeue ()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            T value = _items[_head];

            if (_head == _items.Length - 1)
            {
                // Wrap around array
                _head = 0;
            }
            else
            {
                _head++;
            }

            _size--;
            return value;
        }

        /// <summary>
        /// Return the head item of the queue.
        /// </summary>
        /// <returns></returns>
        public T Peek ()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _items[_head];
        }

        /// <summary>
        /// Clear the queue.
        /// </summary>
        public void Clear ()
        {
            _size = 0;
            _head = 0;
            _tail = -1;
        }

        /// <summary>
        /// Enumerate over the contents of the queue.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator ()
        {
            if (_size > 0)
            {
                // If the queue wraps
                if (_tail < _head)
                {
                    for (int index = _head; index < _items.Length; index++)
                    {
                        yield return _items[index];
                    }

                    for (int index = 0; index <= _tail; index++)
                    {
                        yield return _items[index];
                    }
                }
                else
                {
                    for (int index = _head; index <= _tail; index++)
                    {
                        yield return _items[index];
                    }
                }
            }
        }

        /// <summary>
        /// Enumerate over the contents of the queue.
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
        {
            return GetEnumerator();
        }
    }
}
