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

        /// <summary>
        /// this function created a new Fifo, the Value is String s and it shows to null
        /// </summary>
        public Fifo(String s)
        {
            Contract.Requires(s != null);
            Contract.Requires(s.Length > 0);
            root = new Knot(s, null);
            Contract.Ensures(root != null);
            Contract.Ensures(root.getNext() == null);
        }

        /// <summary>
        /// check the status of memory from the Fifo
        /// </summary>
        /// <returns> Value: Boolean </returns>
        public Boolean isFull()
        {
            //Vorbedingung
            Contract.Requires(size >= 0);
            if (size > 1000)
            {
                return true;
            }
            else
            {
                return false;
            }
            //Nachbedinung
            Contract.Ensures(size > 0);
        }

        /// <summary>
        /// check the status of memory from the Fifo
        /// </summary>
        /// <returns> Value: Boolean </returns>
        public Boolean isEmpty()
        {
            /*
            if (root == null)
            {
                return true;
            }
            else
            {
                return false;
            }
            */
        }

        /// <summary>
        /// Insert strings into the list 
        /// </summary>
        public void put(String s)
        {
            Contract.Requires(s != null);
            Contract.Requires(s.Length > 0);
            /*
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
            */
            Contract.Ensures(root != null);
            Contract.Ensures(root.getNext() == null);
        }

        /// <summary>
        /// get the strings from the list
        /// </summary>
        /// <returns>Value: String </returns>
        public String get()
        {
           
            String data = "";
            Knot k = null;
            Contract.Requires(root != null);
            Contract.Requires(data != null);
            /*
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
                k = root;
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
            */
            Contract.Ensures(data.Length > 0);
            Contract.Ensures(k.getNext() == null);
        }


        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(this.Fifo != null);
            
        }
    }
}
