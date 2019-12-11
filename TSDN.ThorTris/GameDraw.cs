using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using TSDN.ThorTris.Fields;


namespace TSDN.ThorTris
{

    public class GameDraw
    {

        private static int blockSize = 20; //fixme
        private static Color fieldBackgroundColor = Color.Black;
        private static Color textBackgroundColor = Color.White;

        private Graphics canvas;
        private Field field;
        private int cols, rows;

        public GameDraw(Graphics canvas, Field field)
        {
            this.canvas = canvas;
            this.field = field;
            cols = field.FieldMatrix.Cols;
            rows = field.FieldMatrix.Rows;
        }

        private void DrawToCanvas(Rectangle rectangle, Color color)
        {
            //foreach (Graphics graphics in canvas)
            //{
            //    graphics.FillRectangle(new SolidBrush(color), rectangle);
            //}
            canvas.FillRectangle(new SolidBrush(color), rectangle);
        }

        private void DrawToCanvas(Coordinate coordinate, Color color)
        {
            DrawToCanvas(GetRectangle(coordinate), color);
        }

        //public void Draw(Color color, List<Coordinate> drawCoordinates, List<Coordinate> undrawCoordinates)
        //{
        //    //Fixme - don't undraw and draw the same coordinate
        //    foreach (Coordinate coordinate in undrawCoordinates)
        //    {
        //        DrawToCanvas(coordinate, backgroundColor);
        //    }
        //    foreach (Coordinate coordinate in drawCoordinates)
        //    {
        //        DrawToCanvas(coordinate, color);
        //    }
        //}

        //public void Draw(FieldMatrix fieldMatrix)
        public void Draw()
        {
            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    Coordinate coordinate = new Coordinate(col, row);
                    if (field.FieldMatrix[coordinate] != null)
                    {
                        DrawToCanvas(coordinate, field.FieldMatrix[coordinate].Color);
                    }
                    else
                    {
                        DrawToCanvas(coordinate, fieldBackgroundColor);
                    }
                }
            }
        }

        public void DrawString(int player, int line, string text)
        {
            Font font = new Font(FontFamily.GenericSerif, 14);
            Rectangle rectangle = new Rectangle(player * cols * blockSize, (rows + line) * blockSize, cols * blockSize, blockSize);
            DrawToCanvas(rectangle, textBackgroundColor);
            canvas.DrawString(text, font, new SolidBrush(Color.Coral), rectangle);
        }

        //public void ClearRow(int row)
        //{
        //    DrawToCanvas(GetRectangle(row), backgroundColor);
        //}

        private Rectangle GetRectangle(Coordinate coordinate)
        {
            return new Rectangle(blockSize * coordinate.X, blockSize * coordinate.Y, blockSize, blockSize);
        }

        //private Rectangle GetRectangle(int row)
        //{
        //    return new Rectangle(0, blockSize * row, 10000, blockSize); //fixme
        //}

    }

}
