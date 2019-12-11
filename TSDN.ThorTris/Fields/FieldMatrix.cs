using System;
using System.Collections.Generic;
using System.Text;

using TSDN.ThorTris.Bricks;


namespace TSDN.ThorTris.Fields
{

    
    public class FieldMatrix
    {

        private int cols, rows;
        private Brick[,] matrix;

        public FieldMatrix(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
            matrix = new Brick[cols, rows];
        }

        public int Cols
        {
            get { return cols; }
        }

        public int Rows
        {
            get { return rows; }
        }

        public Brick this[Coordinate coordinate]
        {
            get
            {
                if (ValidCoordinate(coordinate))
                {
                    return matrix[coordinate.X, coordinate.Y];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (ValidCoordinate(coordinate))
                {
                    matrix[coordinate.X, coordinate.Y] = value;
                }
            }
        }

        public bool ValidCoordinate(Coordinate coordinate)
        {
            if (coordinate.X >= 0 && coordinate.X < cols && coordinate.Y >= 0 && coordinate.Y < rows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RowFilled(int row)
        {
            bool filled = true;
            for (int col = 0; col < cols; col++)
            {
                if (matrix[col, row] == null)
                {
                    filled = false;
                    break;
                }
            }
            return filled;
        }

        public int RowsFilled()
        {
            int rowsFilled = 0;
            for (int row = 0; row < rows; row++)
            {
                if (RowFilled(row))
                {
                    ++rowsFilled;
                }
            }
            return rowsFilled;
        }

        public void RemoveRow(int row)
        {
            for (int r = row; r >= 0; r--)
            {
                CopyRow(r - 1, r);
            }
        }

        public int RemoveRows()
        {
            int rowsRemoved = 0;
            for (int row = 0; row < rows; row++)
            {
                if (RowFilled(row))
                {
                    ++rowsRemoved;
                    RemoveRow(row);
                }
            }
            return rowsRemoved;
        }

        public void CopyRow(int fromRow, int toRown)
        {
            if (fromRow < 0)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[col, toRown] = null;
                }
            }
            else
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[col, toRown] = matrix[col, fromRow];
                }
            }
        }

    }

}
