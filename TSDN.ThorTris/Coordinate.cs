using System;
using System.Collections.Generic;
using System.Text;

namespace TSDN.ThorTris
{

    public class Coordinate
    {
        private int x, y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public bool InList(List<Coordinate> coordinates)
        {
            foreach (Coordinate coordinate in coordinates)
            {
                if (Equal(coordinate))
                {
                    return true;
                }
            }
            return false;
        }

        private bool Equal(Coordinate coordinate)
        {
            return (x == coordinate.x && y == coordinate.y);
        }

        public static Coordinate operator +(Coordinate coordinate1, Coordinate coordinate2)
        {
            return new Coordinate(coordinate1.X + coordinate2.X, coordinate1.Y + coordinate2.Y);
        }

        public static Coordinate Highest(List<Coordinate> coordinates)
        {
            int highestY = 9999; //something high
            Coordinate highestCoordinate = new Coordinate(0, 0);
            foreach (Coordinate coordinate in coordinates)
            {
                if (coordinate.y < highestY)
                {
                    highestY = coordinate.y;
                    highestCoordinate = coordinate;
                }
            }
            return highestCoordinate;
        }

    }

}
