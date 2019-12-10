using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Simulator.Map
{
    public class MapPoint
    {
        public int X { get; }
        public int Y { get; }

        public MapPoint(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public bool IsAdjacent(MapPoint Other)
        {
            int XDistance = X - Other.X;
            int YDistance = Y - Other.Y;

            return Math.Abs(XDistance) <= 1 && Math.Abs(YDistance) <= 1;
        }
        public override bool Equals(object Other)
        {
            if(Other.GetType() != typeof(MapPoint))
            {
                throw new ArgumentException("Other must be typeof MapPoint");
            }
            MapPoint other = (MapPoint)Other;
            return other.X == X && other.Y == Y;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public bool IsSame(MapPoint coordinates)
        {
            return coordinates.X == X && coordinates.Y == Y;
        }
    }
}
