using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.backend;

namespace game
{
    public class Field
    {

        private int row;
        private int column;
        private List<FieldType> fieldAttributes = new List<FieldType>();

        public Field(int row, int column, List<FieldType> attributes)
        {
            this.setFieldAttributes(attributes);
            this.setRow(row);
            this.setColumn(column);

        }

        public void setFieldAttributes(List<FieldType> attributes)
        {
            if (attributes == null)
            {
                throw new ArgumentNullException("the parameter cannot be null");
            }
            else
            {
                fieldAttributes = attributes;
            }
        }

        private void setRow(int row)
        {
            int mapHeight = GameManager.getGameManagerInstance().getMapHeight();
            Contract.Requires(row >= 0 && row < mapHeight);
            //if (row < 0 || row > mapHeight)
            //{
                //throw new ArgumentException("The height of this Field is not allowed to be smaller than 0 or greater than the Map-Height!");
            //}
            this.row = row;

        }

        private void setColumn(int column)
        {
            int mapWidth = GameManager.getGameManagerInstance().getMapWidth();
            Contract.Requires(column >= 0 && row < mapWidth);
            //if (column < 0 || column > mapWidth)
            //{
                //throw new ArgumentException("The width of this field is not allowed to be smaller than 0 or greater than the Map-Height!");
            //}
            this.column = column;

        }

        public int getColumn()
        {
            return column;
        }

        public int getRow()
        {
            return row;
        }

        public Boolean isForest()
        {
            return this.fieldAttributes.Contains(FieldType.FOREST);
        }

        public Boolean isWater()
        {
            return this.fieldAttributes.Contains(FieldType.WATER);
        }

        public Boolean isWalkable()
        {
            return this.fieldAttributes.Contains(FieldType.WALKABLE);
        }

        public Boolean isHuntable()
        {
            return this.fieldAttributes.Contains(FieldType.HUNTABLE);
        }

        public Boolean isWall()
        {
            return this.fieldAttributes.Contains(FieldType.WALL);
        }

    }
}
