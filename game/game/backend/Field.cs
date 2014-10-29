using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Field
    {
        public static const int WALKABLE = 0;
        public static const int WALL = 1;
        public static const int FOREST = 2;
        public static const int WATER = 3;
        public static const int HUNTABLE = 4;

        private int row;
        private int column;
        private int fieldTypeOne;
        private int fieldTypeTwo;

        public Field(int row, int column, int typeOne, int typeTwo)
        {
            this.setRow(row);
            this.setColumn(column);
            this.setFieldType(typeOne);
            this.setFieldType(typeTwo);

        }

        private void setFieldType(int type)
        {
            Contract.Requires(type != null);
            Contract.Requires(type >= 0);
            Contract.Requires(type <= 4);

            if (type < 0 || type > 4)
            {
                throw new ArgumentException("The 'Type' Variable needs to have a value of 0 - 4!");
            }

            if (this.fieldTypeOne == null)
            {
                this.fieldTypeOne = type;
            }
            else
            {
                //if (this.fieldTypeOne == Field.WALKABLE)
                //{
                //    if()
                //}
            }

        }

        private void setRow(int row)
        {
            int mapHeight = GameManager.getGameManagerInstance().getMapHeight();
            Contract.Requires(row >= 0 && row < mapHeight);
            if (row < 0 || row > mapHeight)
            {
                throw new ArgumentException("The height of this Field is not allowed to be smaller than 0 or greater than the Map-Height!");
            }
            this.row = row;

        }

        private void setColumn(int column)
        {
            int mapWidth = GameManager.getGameManagerInstance().getMapWidth();
            Contract.Requires(column >= 0 && row < mapWidth);
            if (column < 0 || column > mapWidth)
            {
                throw new ArgumentException("The width of this field is not allowed to be smaller than 0 or greater than the Map-Height!");
            }
            this.column = column;

        }


    }
}
