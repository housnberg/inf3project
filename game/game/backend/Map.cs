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


        public Map(int height, int width)
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
           
            this.width = width;
        }

        /// <summary>
        /// Checks wether the height is allowed or not
        /// </summary>
        /// <param name="height">Height of the map</param>
        private void setHeight(int height)
        {
          
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

        public void setField(Field field)
        {
            if (field.getColumn() < 0 || field.getRow() > width - 1 || field.getRow() < 0 || field.getColumn() > height - 1)
            {
                throw new Exception("you cannot place a field outside the map");
            }
            else
            {
                fields[field.getColumn(), field.getRow()] = field;
            }
        }

        public Field findField(Field field)
        {
            int row = field.getRow();
            int col = field.getColumn();

            Field f = fields[row, col];

            return f;
        }
    }
}
