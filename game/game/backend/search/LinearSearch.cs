using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.backend.search
{
    public abstract class LinearSearch<C>
    {
        /// <summary>
        /// Linear search through a list. Searches as long as the list is not exhausted and the searched element is not found yet.
        /// </summary>
        /// <param name="list">the list to search through</param>
        /// <param name="criterion">the criterion from which we determine, whether we have found our element</param>
        /// <returns>the element, if it is found, or the default value of type C, if not</returns>
        public static C find(List<C> list, Func<C, bool> criterion)
        {
            C result = default(C);
            int i = 0;
            while (i < list.Count)
            {
                if(criterion(list[i])) {
                    result = list[i];
                }
                i++;
            }
            return result;
        }
    }
}
