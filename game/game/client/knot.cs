using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.client
{
    class Knot
    {
        private String s;
        private Knot next;


        public Knot(String s, Knot next)
        {
            this.setData(s);
            this.setNext(next);
        }

        public String getData()
        {
            return s;
        }

        public void setData(String s)
        {
            this.s = s;
        }

        public Knot getNext()
        {
            return next;
        }

        public void setNext(Knot next)
        {
            this.next = next;
        }










    }
}
