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
            this.fieldAttributes.AddRange(attributes);

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

        private void setFieldType(int type)
        {
            Contract.Requires(type >= 0);
            Contract.Requires(type <= 4);
            //try
            //{
            //    if (type < 0 || type > 4)
            //    {
            //        throw new ArgumentException("The 'Type' Variable needs to have a value of 0 - 4!");
            //    }

            //    if (this.fieldTypeOne == null)
            //    {
            //        this.fieldTypeOne = type;
            //    }
            //    else
            //    {
            //        switch (type)
            //        {
            //            case Field.WALKABLE:
            //                if (fieldTypeOne == Field.WATER || fieldTypeOne == Field.WALL || fieldTypeOne == Field.WALKABLE)
            //                {
            //                    throw new ArgumentException("Type not available in combination with type 'WALKABLE'");
            //                }
            //                else
            //                {
            //                    this.fieldTypeTwo = type;
            //                }
            //                break;

            //            case Field.WALL:
            //                 if (fieldTypeOne != Field.WALKABLE || fieldTypeOne == Field.WATER || fieldTypeOne == Field.HUNTABLE)
            //                {
            //                    throw new ArgumentException("Type not available in combination with type 'WALL'");
            //                }
            //                else
            //                {
            //                    this.fieldTypeTwo = type;
            //                }
            //                break;

            //            case Field.FOREST:
            //                 if (fieldTypeOne == Field.WALL || fieldTypeOne == Field.WATER || fieldTypeOne == Field.FOREST)
            //                {
            //                    throw new ArgumentException("Type not available in combination with type 'FOREST'");
            //                }
            //                else
            //                {
            //                    this.fieldTypeTwo = type;
            //                }
            //                break;

            //            case Field.WATER:
            //                if ( )
            //                {
            //                    throw new ArgumentException("Type not available in combination with type 'WATER'");
            //                }
            //                else
            //                {
            //                    this.fieldTypeTwo = type;
            //                }
            //                break;

            //            case Field.HUNTABLE:
            //                 if (fieldTypeOne == Field.WATER || fieldTypeOne == Field.WALL)
            //                {
            //                    throw new ArgumentException("Type not available in combination with type 'HUNTABLE'");
            //                }
            //                else
            //                {
            //                    this.fieldTypeTwo = type;
            //                }
            //                break;

            //        }

            //    }
            //}
            //catch (Exception exception)
            //{
            //    Console.Out.WriteLine(exception.Message);
            //}

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
