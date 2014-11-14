using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class MapCell : ITile
    {
        private int x,y;
        private List<MapCellAttribute> attributes;
        public MapCell(int x, int y, List<MapCellAttribute> attributes)
        {
            this.x = x;
            this.y = y;
            this.attributes = new List<MapCellAttribute>();
            this.attributes.AddRange(attributes);
        }

        public int getXPosition()
        {
            return this.x;
        }

        public int getYPosition()
        {
            return this.y;
        }

        public bool isWalkable()
        {
            return !this.attributes.Contains(MapCellAttribute.UNWALKABLE);
        }

        public bool isForest()
        {
            return this.attributes.Contains(MapCellAttribute.FOREST);
        }

        public bool isHuntable()
        {
            return this.attributes.Contains(MapCellAttribute.HUNTABLE);
        }

        public bool isWater()
        {
            return this.attributes.Contains(MapCellAttribute.WATER);
        }
    }

    public enum MapCellAttribute {UNWALKABLE, WATER, FOREST, HUNTABLE}
}
