using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.backend.search
{
    public abstract class BinarySearch<C>
    {

        /// <summary>
        /// binary search trough a sorted list. 
        /// </summary>
        /// <param name="list">the list to search trough</param>
        /// <param name="reference">the element we want to find</param>
        /// <param name="comparison">the function we want to compare our elements</param>
        /// <returns>the element we search for if we fount it, otherwise the default value of C</returns>
        public static C find(List<C> list, C reference, Func<C, C, int> comparison)
        {
            int middle;
            int left = 0;
            int right = list.Count - 1;
            C result = default(C);
            bool found = false;
            while (!found)
            {
                if (left <= right)
                {
                    middle = (left + right) / 2;
                    if (comparison(list[middle], reference) > 0)
                    {
                        right = middle - 1;
                    }
                    else if (comparison(list[middle], reference) < 0)
                    {
                        left = middle + 1;
                    }
                    else
                    {
                        result = list[middle];
                        found = true;
                    }
                }
                else
                {
                    found = true;
                }
            }
            return result;
        }
    }
}
