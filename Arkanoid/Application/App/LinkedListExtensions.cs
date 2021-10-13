using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public static class LinkedListExtensions
    {
        public static void ForAll<T>(this LinkedList<T> list, Action<LinkedListNode<T>> action)
        {
            LinkedListNode<T> current = list.First;
            LinkedListNode<T> next;
            while (current != null)
            {
                next = current.Next;
                action(current);
                current = next;
            }
        }
    }
}
