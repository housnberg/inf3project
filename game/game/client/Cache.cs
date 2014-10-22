using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.client
{
    class Cache
    {
        private Knot root;
        private int size = 0;


        public Cache(String s)
        {
            root = new Knot(s, null);
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

        // Insert strings into the list
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
    }
}
