using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    public class ListeChainee<T> : IEnumerable, IEnumerator
    {
        public Node<T> tete;
        private int Position;


        public T this[int i]
        {
            get => GetAt(i);
            set => SetAt(i, value);
        }

        public int Count
        {
            get => GetLength(tete);
        }

        int GetLength(Node<T> n)
        {
            if( n == null ) return 0;
            return 1 + GetLength(n.next);
        }

        void SetAt(int i, T v)
        {
            int counter = 0;
            Node<T> ret = tete;

            while (counter < i)
            {
                if (ret == null)
                    throw new IndexOutOfRangeException();
                ret = ret.next;
                counter++;
            }

            ret.value = v;
        }

        T GetAt(int i)
        {
            int counter = 0;
            Node<T> ret = tete;

            while( counter < i ) 
            {
                if(ret == null) 
                    throw new IndexOutOfRangeException();
                ret = ret.next;
                counter++;
            }

            return ret.value;
        }

        public void Add(T val)
        {
            if(tete == null)
            {
                tete = new Node<T>(val);
                return;
            }

            Node<T> ret = tete;
            while (ret.next != null)
            {
                ret = ret.next;
            }

            ret.next = new Node<T>(val);  
        }

        public double Sum(Func<T, double> f)
        {
            double sum = 0;
            Node<T> ret = tete;

            while (ret.next != null)
            {
                sum += f(ret.value);
                ret = ret.next;
            }
            sum += f(ret.value);
            return sum;
        }

        public bool MoveNext()
        {
            if(Position < Count)
            {
                Position++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            Position = -1;
        }
        public object Current
        {
            get
            {
                return this[Position];
            }
        }

        // Implementing IEnumerable
        public IEnumerator GetEnumerator() {
            return (IEnumerator)this;
        }
    }
}
