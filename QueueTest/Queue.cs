using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTest
{
    class Queue<TYPE>
    {
        #region Node class
        private class Node<T>
        {
            private T _Data;
            private Node<T> _Prev;
            private Node<T> _Next;

            public Node<T> Prev
            {
                get
                {
                    return _Prev;
                }
                set
                {
                    _Prev = value;
                }
            }

            public Node<T> Next
            {
                get
                {
                    return _Next;
                }
                set
                {
                    _Next = value;
                }
            }

            public T Data
            {
                get
                {
                    return _Data;
                }
            }

            public Node(T data)
            {
                _Data = data;

            }
        }
        #endregion  

        private Node<TYPE> _Head;
        private Node<TYPE> _Tail;
        private int _Count;

        // The number of items in the queue
        public int Count
        {
            get
            {
                return _Count;
            }
        }

        // Queue constructor
        public Queue()
        {
            _Head = null;
            _Tail = null;
            _Count = 0;
        }

        // Add a new item to the end of the queue
        public void Enqueue(TYPE data)
        {
            // create a new node
            Node<TYPE> newNode = new Node<TYPE>(data);

            // if no items on the queue then this is the first
            if ( _Count == 0 )
            {
                // this node is both the head and the tail of the queue
                _Head = newNode;
                _Tail = newNode;
            }
            else
            {
                // add this node to the end of the queue
                newNode.Prev = _Tail;
                _Tail.Next = newNode;
                _Tail = newNode;
            }

            // one new item added to the queue
            _Count++;
        }

        // remove first item from the queue
        public TYPE Dequeue()
        {
            TYPE data = default(TYPE);

            if (_Count == 0)
                throw new InvalidOperationException("No data on the queue");

            // this is a queue so take the item off the top
            data = _Head.Data;

            // take one off the queue
            _Count--;

            if (_Count == 0)
            {
                // set head and tail to null
                _Head = null;
                _Tail = null;
            }
            else
            {
                // remove Head from the queue
                _Head = _Head.Next;
                _Head.Prev.Next = null;
                _Head.Prev = null;
            }

            return data;
        }

        // Used to deterine if the queue is empty
        public bool Empty()
        {
            bool result = _Count == 0;

            return result;
        }
    }
}
