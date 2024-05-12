using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    public class ListeChainee<T> : IEnumerable
    {
        public Node<T> tete;


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
            if (tete == null)
                throw new IndexOutOfRangeException();

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

        public ListeChainee<T> FindAll(Predicate<T> match)
        {
            ListeChainee<T> newList = new ListeChainee<T>();

            foreach(T t in this)
            {
                if (match(t))
                {
                    newList.Add(t);
                }
            }
            
            return newList;
        }

        public void ForEach(Action<T> a)
        {
            foreach(T t in this)
            {
                a(t);
            }
        }

        public void Reverse()
        {
            Node<T> current = tete;
            Node<T> prev = null, next = null;

            while(current != null)
            {
                next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }

            tete = prev;
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


        public void Sort(Comparison<T> comparison)
        {
            bool swapped;
            do
            {
                swapped = false;
                Node<T> current = tete;
                while (current != null && current.next != null)
                {
                    if (comparison(current.value, current.next.value) > 0)
                    {
                        T temp = current.value;
                        current.value = current.next.value;
                        current.next.value = temp;
                        swapped = true;
                    }
                    current = current.next;
                }
            } while (swapped);
        }

        private class LCEnumerator : IEnumerator
        {
            private int Position = -1;
            private ListeChainee<T> instance;

            public LCEnumerator(ListeChainee<T> inst)
            {
                instance = inst;
            }

            public bool MoveNext()
            {
                if (Position < instance.Count - 1)
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
                get { return instance[Position]; }
            }
        }

        // Implementing IEnumerable
        public IEnumerator GetEnumerator() {
            return new LCEnumerator(this);
        }
    }
}
