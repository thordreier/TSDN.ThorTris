using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using TSDN.ThorTris.Fields;


namespace TSDN.ThorTris.Bricks
{


    abstract public class Brick
    {
        protected Shape[] shapes;
        protected Color color;

        private Field field;
        private GameDraw draw;

        private static Random rnd = new Random();
        private int shapeID;
        private Coordinate coordinate;
        //private bool falling = true;
        private int droppedCount = 0;

        public Brick(Field field, GameDraw draw)
        {
            this.field = field;
            this.draw = draw;
            shapeID = rnd.Next(0, 3);
            //shapeID = 0; //fixme
            coordinate = new Coordinate(4, -4);
        }

        public bool MoveIntoField()
        {
            for (int i = 0; i < 5; i++)
            {
                if (field.InField(this, Shape, ref coordinate, null, false))
                {
                    coordinate += new Coordinate(0, -1);
                    return true;
                }
                else
                {
                    coordinate += new Coordinate(0, 1);
                }
            }
            return false;
        }

        public Shape Shape
        {
            get { return shapes[shapeID]; }
        }

        public Color Color
        {
            get { return color; }
        }

        public Coordinate Coordinate
        {
            get { return coordinate; }
        }

        //private void Draw(Coordinate oldCoordinate)
        //{
        //    draw.Draw(Color, Shape.GetCoordinates(coordinate), Shape.GetCoordinates(oldCoordinate));
        //}
        //private void Draw(Shape oldShape, Coordinate oldCoordinate)
        //{
        //    draw.Draw(Color, Shape.GetCoordinates(coordinate), oldShape.GetCoordinates(oldCoordinate));
        //}

        public int DroppedCount
        {
            get { return droppedCount; }
        }

        #region Movement
        public bool TurnClockwise()
        {
            droppedCount = 0;
            return Turn(1);
        }

        public bool TurnCounterclockwise()
        {
            droppedCount = 0;
            return Turn(-1);
        }

        private bool Turn(int turn)
        {
            Shape oldShape = Shape;
            int newID = (shapeID + 4 + turn) % 4;
            Coordinate oldCoordinate = coordinate;
            Coordinate newCoordinate = coordinate;

            if (field.InField(this, shapes[newID], ref newCoordinate, Shape.GetCoordinates(oldCoordinate)))
            {
                coordinate = newCoordinate;
                shapeID = newID;
                //Draw(oldShape, oldCoordinate);
                return true;
            }
            return false;
        }

        public Shape GetShape(int turn)
        {
            return shapes[(shapeID + 4 + turn) % 4];
        }

        public bool MoveLeft()
        {
            droppedCount = 0;
            return Move(new Coordinate(-1, 0));
        }

        public bool MoveRight()
        {
            droppedCount = 0;
            return Move(new Coordinate(1, 0));
        }

        public bool MoveDown()
        {
            if (Move(new Coordinate(0, 1)))
            {
                droppedCount++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MoveToBottom()
        {
            while (MoveDown()) { }
        }

        private bool Move(Coordinate move)
        {
            Coordinate oldCoordinate = coordinate;
            Coordinate newCoordinate = coordinate + move;

            if(field.InField(this, Shape, ref newCoordinate, Shape.GetCoordinates(oldCoordinate)))
            {
                coordinate = newCoordinate;
                //Draw(oldCoordinate);
                return true;
            }
            return false;
        }
        #endregion

    }


}
