using System;
using System.Collections.Generic;
using System.Text;

using TSDN.ThorTris.Bricks;
using TSDN.ThorTris.Fields;

namespace TSDN.ThorTris
{


    public class Player
    {

        //fixme
        private static int[] pointsRemovedRows = { 0, 200, 600, 1200, 2000 };
        private static int pointDroppedRow = 1;
        private static int pointBrick = 5;

        private int id;
        private BrickDispenser brickDispenser;
        protected Field field;
        protected GameDraw draw;
        
        Brick brick;
        private int points = 0;
        private bool alive = true;

        public Player(int id, BrickDispenser brickDispenser, Field field, GameDraw draw)
        {
            this.id = id;
            this.brickDispenser = brickDispenser;
            this.field = field;
            this.draw = draw;

            brick = brickDispenser.NewBrick(); //fixme
            DrawPoints();
        }

        public Brick Brick
        {
            get { return brick; }
        }

        public Field Field
        {
            get { return field; }
        }

        public virtual void TimerTick()
        {
            if (Brick == null)
            {
                return;
            }
            //if (alive && !brick.MoveDown())
            if (!MoveDown())
            {
                points += pointsRemovedRows[field.RemoveRows()] + pointBrick + pointDroppedRow * brick.DroppedCount;
                DrawPoints();
                if ((brick = brickDispenser.NewBrick()) == null)
                {
                    draw.DrawString(0, 0, "DEAD: " + points.ToString()); //fixme
                    alive = false;
                }
            }
        }

        private void DrawPoints()
        {
            draw.DrawString(0, 0, "Points: " + points.ToString());
        }

        public virtual bool MoveLeft()
        {
            if (alive)
            {
                return brick.MoveLeft();
            }
            return false;
        }

        public virtual bool MoveRight()
        {
            if (alive)
            {
                return brick.MoveRight();
            }
            return false;
        }

        public virtual bool TurnClockwise()
        {
            if (alive)
            {
                return brick.TurnClockwise();
            }
            return false;
        }

        public virtual bool TurnCounterclockwise()
        {
            if (alive)
            {
                return brick.TurnCounterclockwise();
            }
            return false;
        }

        public virtual bool MoveDown()
        {
            if (alive)
            {
                return brick.MoveDown();
            }
            return false;
        }

        public virtual void MoveToBottom()
        {
            if (alive)
            {
                brick.MoveToBottom();
            }
        }

    }


}
