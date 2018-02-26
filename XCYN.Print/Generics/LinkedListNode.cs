using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Generics
{
    /// <summary>
    /// 链表节点
    /// </summary>
    public class LinkedListNode
    {

        public LinkedListNode(object value)
        {
            Value = value;
        }

        public object Value { get;private set; }

        public LinkedListNode Next { get; set; }

        public LinkedListNode Prev { get; set; }

    }

    /// <summary>
    /// 链表
    /// </summary>
    public class LinkedList:IEnumerable
    {
        public LinkedListNode First { get; set; }

        public LinkedListNode Last { get; set; }

        public LinkedListNode AddList(object node)
        {
            var newNode = new LinkedListNode(node);
            if(First == null)
            {
                First = newNode;
                Last = First;
            }
            else
            {
                LinkedListNode previous = Last;
                Last.Next = newNode;
                Last = newNode;
                Last.Prev = previous;
            }
            return newNode;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            LinkedListNode current = First;
            while(current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
    }

    /// <summary>
    /// 链表节点
    /// </summary>
    public class LinkedListNode<T>
    {

        public LinkedListNode(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode<T> Prev { get; set; }

    }

    /// <summary>
    /// 链表
    /// </summary>
    public class LinkedList<T> : IEnumerable<T>
    {
        public LinkedListNode<T> First { get; set; }

        public LinkedListNode<T> Last { get; set; }

        public LinkedListNode<T> AddList(T node)
        {
            var newNode = new LinkedListNode<T>(node);
            if (First == null)
            {
                First = newNode;
                Last = First;
            }
            else
            {
                LinkedListNode<T> previous = Last;
                Last.Next = newNode;
                Last = newNode;
                Last.Prev = previous;
            }
            return newNode;
        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
