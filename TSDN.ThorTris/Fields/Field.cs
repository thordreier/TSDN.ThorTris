using System;
using System.Collections.Generic;
using System.Text;

using TSDN.ThorTris.Bricks;


namespace TSDN.ThorTris.Fields
{


    public class Field
    {

        //private GameDraw draw;

        private FieldMatrix fieldMatrix;

        //public Field(GameDraw draw, int cols, int rows)
        public Field(int cols, int rows)
        {
            //this.draw = draw;
            fieldMatrix = new FieldMatrix(cols, rows);
        }

        public FieldMatrix FieldMatrix
        {
            get { return fieldMatrix; }
        }

        public bool InField(Brick brick, Shape shape, ref Coordinate coordinate,
            List<Coordinate> oldCoordinates)
        {
            return InField(brick, shape, ref coordinate, oldCoordinates, true);
        }

        public bool InField(Brick brick, Shape shape, ref Coordinate coordinate,
            List<Coordinate> oldCoordinates, bool update)
        {
            List<Coordinate> coordinates = shape.GetCoordinates(coordinate);
            bool ok = true;
            int xMovement = 0;
            for (int i=0; i<coordinates.Count; i++)
            {
                if (coordinates[i].X < 0)
                {
                    //inField = false;
                    xMovement = System.Math.Min(coordinates[i].X, xMovement);
                    ok = false;
                }
                else if (coordinates[i].X >= fieldMatrix.Cols)
                {
                    //inField = false;
                    xMovement = System.Math.Max(coordinates[i].X - fieldMatrix.Cols + 1, xMovement);
                    ok = false;
                }
                if (coordinates[i].Y >= fieldMatrix.Rows || coordinates[i].Y < 0)
                {
                    ok = false;
                }
                if (ok)
                {
                    if (fieldMatrix[coordinates[i]] != null && fieldMatrix[coordinates[i]] != brick)
                    {
                        ok = false;
                    }
                }
            }
            if (ok && update)
            {
                foreach (Coordinate oldCoordinate in oldCoordinates)
                {
                    if (fieldMatrix[oldCoordinate] == brick)
                    {
                        fieldMatrix[oldCoordinate] = null;
                    }
                }
                foreach (Coordinate newCoordinate in coordinates)
                {
                    if (fieldMatrix[newCoordinate] == null)
                    {
                        fieldMatrix[newCoordinate] = brick;
                    }
                }
            }

            if (xMovement != 0)
            {
                Coordinate newCoordinate = coordinate + new Coordinate(-xMovement, 0);
                if (ok = InField(brick, shape, ref newCoordinate, oldCoordinates, update))
                {
                    coordinate = newCoordinate;
                }
                coordinates = shape.GetCoordinates(coordinate);
            }

            return ok;
        }

        public int RemoveRows()
        {
            return fieldMatrix.RemoveRows();
        }

        //public bool MoveBrick(Brick brick)
        //{
        //}

    }

}
