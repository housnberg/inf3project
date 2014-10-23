using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace game.client
{
    public class Fifo
    {
        private Knot root;
        private int size = 0;


        public Fifo(String s)
        {
            Contract.Requires(s != null);
            root = new Knot(s, null);
            Contract.Ensures(s.Length > 0);
        }
        
        public Boolean isFull()
        {
            if (size > 1000)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>



        public Boolean isEmpty()
        {
            if (root == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Insert strings into the list 
        /// </summary>
        public void put(String s)
        {
            if (root == null)
            {
                root = new Knot(s, null);
                size++;
            }
            else
            {
                Knot k = new Knot(s, root);
                root = k;
                size++;
            }
        }

        // get the strings from the list
        public String get()
        {
            String data = "";
            if (root == null)
            {
               data = "keine Daten vorhanden";
            }
            else if (root.getNext() == null)// exist one root
            {
                data = root.getData();
                root = null;
                size--;
            }
            else// exist more than one root
            {
                Knot k = root;
                Knot previous = root;
                while (k.getNext() != null)
                {
                    previous = k;
                    k = k.getNext();
                }
                data = k.getData();
                k = null;
                previous.setNext(null);
            }
            size--;
            return data;
        }


        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(this.buffer != null);
            
        }







    }
}
