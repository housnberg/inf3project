using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public class Map
    {
        private int width;
        private int height;
        private Field[,] fields; //2Dimensional Array


        public Map(int width, int height)
        {
            this.setWidth(width);
            this.setHeight(height);
            fields = new Field[height,width]; // [x,y] --> NOT [x][y]
        }

        /// <summary>
        /// Checks wether the width is allowed or not
        /// </summary>
        /// <param name="width">Width of the map</param>
        private void setWidth(int width)
        {
            if (width < 5 || width > 30)
            {
                throw new Exception("Breite kann nicht kleiner als 5 oder größer als 30 sein!");
            }
            this.width = width;
        }

        /// <summary>
        /// Checks wether the height is allowed or not
        /// </summary>
        /// <param name="height">Height of the map</param>
        private void setHeight(int height)
        {
            if (height < 5 || height > 30)
            {
                throw new Exception("Höhe kann nicht kleiner als 5 oder größer als 30 sein!");
            }
            this.height = height;
        }

        /// <summary>
        /// Return the height of the Map as an int Value
        /// </summary>
        /// <returns>Heigth of the Map </returns>
        public int getHeight()
        {
            return this.height;
        }

        /// <summary>
        /// Return the width of the Map as an int Value
        /// </summary>
        /// <returns>Width of the Map </returns>
        public int getWidth()
        {
            return this.width;
        }

        public Field[,] getFields()
        {
            return this.fields;
        }
    }
}
