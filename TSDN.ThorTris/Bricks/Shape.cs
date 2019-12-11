using System;
using System.Collections.Generic;
using System.Text;

namespace TSDN.ThorTris.Bricks
{


    public class Shape
    {

        private bool[,] matrix;

        public Shape(bool[,] matrix)
        {
            this.matrix = matrix;
        }

        public List<Coordinate> GetCoordinates(Coordinate coordinate)
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    if (matrix[y, x])
                    {
                        coordinates.Add(coordinate + new Coordinate(x, y));
                    }
                }
            }
            return coordinates;
        }

        public List<Coordinate> GetCoordinates()
        {
            return GetCoordinates(new Coordinate(0, 0));
        }

    }


}
