using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.client
{
    class Buffer
    {
       // Es ist noch ein kleiner Fehler drin, wenn man die Liste komplett leert


        private Knot root;


        public Buffer(String s)
        {
            root = new Knot(s, null);
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
            }
            else
            {
                Knot k = new Knot(s, root);
                root = k;
            }
        }

        // get the strings from the list
        public String get()
        {
            if (root == null)
            {
                return "leer";
            }
            else
            {
                Knot k = root;
                Knot next;
                Knot previous = null;
                do
                {
                    next = k.getNext();
                    if (next != null)
                    {
                        previous = k;
                        k = next;
                    }
                }
                while (next != null);
                String tmp = k.getData();
                if (previous.getNext() == null)
                {
                    root = null;
                }
                else
                {
                    previous.setNext(null);
                }

                return tmp;


            }

        }

    }
}
