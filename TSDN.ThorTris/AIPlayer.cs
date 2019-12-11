using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using TSDN.ThorTris.Bricks;
using TSDN.ThorTris.Fields;


namespace TSDN.ThorTris
{


    public class AIPlayer : Player
    {
        private static int startScore = -99999999; //something low
        private static int pointsFilledRow = 100;
        private static int pointsFilledBelow = 25;
        private static int pointsHight = 2;
        private static int pointsNeighbours = 5;
        private static int pointsHoles = 10;
        private static int pointsUndergroundHoles = 10;

        private int cols, rows;
        //private System.Windows.Forms.Timer timer;
        int bestX = 0;
        int bestTurns = 0;

        public AIPlayer(int id, BrickDispenser brickDispenser, Field field, GameDraw draw)
            : base(id, brickDispenser, field, draw)
        {
            cols = Field.FieldMatrix.Cols;
            rows = Field.FieldMatrix.Rows;

            //timer = new System.Windows.Forms.Timer();
            //timer.Interval = 150;
            //timer.Enabled = true;
            //this.timer.Tick += new System.EventHandler(AIMove);

        }

        //private bool BetterHight(Coordinate oldCoordinate, Coordinate newCoordinate)
        //{
        //}

        public void AIMove()
        {
            if (Brick == null)
            {
                return;
            }
            //System.Windows.Forms.MessageBox.Show("xxx");
            //Turn and move the brick to the best place
            Coordinate origCoordinate = Brick.Coordinate;
            if (bestTurns > 0)
            {
                if (TurnClockwise())
                {
                    --bestTurns;
                }
            }
            else if (bestX < origCoordinate.X)
            {
                base.Brick.MoveLeft();
            }
            else if (bestX > origCoordinate.X)
            {
                base.Brick.MoveRight();
            }
        }

        private int ScoreFilledRows(List<Coordinate> coordinates)
        {
            //Find number of rows to be filled (removed)
            int rowsFilled = 0;
            for (int row = 0; row < rows; row++)
            {
                bool filled = true;
                for (int col = 0; col < cols; col++)
                {
                    Coordinate testCoordinate = new Coordinate(col, row);
                    if (field.FieldMatrix[testCoordinate] == null)
                    {
                        if (!testCoordinate.InList(coordinates))
                        {
                            filled = false;
                            break;
                        }
                    }
                }
                if (filled)
                {
                    ++rowsFilled;
                }
            }
            return rowsFilled * pointsFilledRow;
        }

        private int ScoreFilledBelow(List<Coordinate> coordinates)
        {
            //Find number of empty pixels directly under the brick
            int fit = 0;
            for (int i = 0; i < coordinates.Count; i++)
            {
                Coordinate c = coordinates[i] + new Coordinate(0, 1);
                //if ((!field.FieldMatrix.ValidCoordinate(c) || field.FieldMatrix[c] != null)
                //    && !c.InList(coordinates))
                if (!FilledCoordinate(coordinates, c))
                {
                    --fit;
                }
            }
            return fit * pointsFilledBelow;
        }

        private int ScoreHight(List<Coordinate> coordinates)
        {
            //Find the highest pixel
            Coordinate highest = Coordinate.Highest(coordinates);
            return highest.Y * pointsHight;
        }

        private int ScoreNeighbours(List<Coordinate> coordinates)
        {
            //Find number of empty pixels directly under the brick
            int fit = 0;
            for (int i = 0; i < coordinates.Count; i++)
            {
                //int x = NeighboursFilled(coordinates, coordinates[i]) - 2;
                //Substract every empty neighbour
                fit += NeighboursFilled(coordinates, coordinates[i]) - 2;

                //int y = NeighboursFilledNotSelf(coordinates, coordinates[i]);
                //Add every filled neighbour
                fit += NeighboursFilledNotSelf(coordinates, coordinates[i]);

                //for (int x = -1; x < 2; x += 2)
                //{
                //    Coordinate c = coordinates[i] + new Coordinate(x, 0);
                //    //if ((!field.FieldMatrix.ValidCoordinate(c) || field.FieldMatrix[c] != null)
                //    //    && !c.InList(coordinates))
                //    if (!FilledCoordinate(coordinates, c))
                //    {
                //        --fit;
                //    }
                //}
            }
            return fit * pointsNeighbours;
        }

        private int ScoreHoles(List<Coordinate> coordinates)
        {
            int hole = 0;
            for (int x = 0; x < cols; x++)
            {
                bool inHole = false;
                for (int y = 0; y < rows; y++)
                {
                    Coordinate coordinate = new Coordinate(x, y);
                    if (FilledCoordinate(coordinates, coordinate))
                    {
                        break;
                    }
                    if (inHole)
                    {
                        --hole;
                    }
                    else if (NeighboursFilled(coordinates, coordinate) == 2)
                    {
                        inHole = true;
                    }
                }
            }
            return hole * pointsHoles;
        }

        private int ScoreUndergroundHoles(List<Coordinate> coordinates)
        {
            //Find number of empty pixels directly under the brick
            FieldMatrix matrix = field.FieldMatrix;
            int holes = 0;
            for (int i = 0; i < coordinates.Count; i++)
            {
                Coordinate c = coordinates[i] + new Coordinate(0, 1);
                if (c.InList(coordinates))
                {
                    continue;
                }
                while (matrix.ValidCoordinate(c))
                {
                    if (!FilledCoordinate(coordinates, c))
                    {
                        --holes;
                    }
                    c += new Coordinate(0, 1);
                }
            }
            return holes * pointsUndergroundHoles;
        }

        private int NeighboursFilled(List<Coordinate> coordinates, Coordinate coordinate)
        {
            int filled = 0;
            for (int x = -1; x < 2; x += 2)
            {
                Coordinate c = coordinate + new Coordinate(x, 0);
                //if ((!field.FieldMatrix.ValidCoordinate(c) || field.FieldMatrix[c] != null)
                //    && !c.InList(coordinates))
                if (FilledCoordinate(coordinates, c))
                {
                    ++filled;
                }
            }
            return filled;
        }

        private int NeighboursFilledNotSelf(List<Coordinate> coordinates, Coordinate coordinate)
        {
            int filled = 0;
            for (int x = -1; x < 2; x += 2)
            {
                Coordinate c = coordinate + new Coordinate(x, 0);
                //if ((!field.FieldMatrix.ValidCoordinate(c) || field.FieldMatrix[c] != null)
                //    && !c.InList(coordinates))
                if (FilledCoordinateNotSelf(coordinates, c))
                {
                    ++filled;
                }
            }
            return filled;
        }

        private bool FilledCoordinate(List<Coordinate> coordinates, Coordinate coordinate)
        {
            FieldMatrix matrix = field.FieldMatrix;
            return !(matrix.ValidCoordinate(coordinate)
                && (matrix[coordinate] == null || matrix[coordinate] == Brick)
                && !coordinate.InList(coordinates));
        }

        private bool FilledCoordinateNotSelf(List<Coordinate> coordinates, Coordinate coordinate)
        {
            FieldMatrix matrix = field.FieldMatrix;
            return !(matrix.ValidCoordinate(coordinate)
                && (matrix[coordinate] == null || matrix[coordinate] == Brick));
        }

        //private int GetScore(List<Coordinate> coordinates)
        //{
        //    ////Find the highest pixel
        //    //Coordinate highest = Coordinate.Highest(coordinates);
        //    //if (rowsFilled > bestRemoves
        //    //    || (rowsFilled == bestRemoves && fit < bestFit)
        //    //    || (rowsFilled == bestRemoves && fit == bestFit && hight > bestHight)
        //    //    )
        //    //{
        //    //    bestX = x;
        //    //    bestHight = hight;
        //    //    bestTurns = turns;
        //    //    bestFit = fit;
        //    //    bestRemoves = rowsFilled;
        //    //}
        //}

        public override void TimerTick()
        {
            Brick oldBrick = Brick;

            base.TimerTick();

            //fixme
            if (oldBrick == Brick)
            {
                return;
            }
            Calculate();
        }

        private void Calculate()
        {
            if (Brick == null)
            {
                return;
            }

            Brick brick = Brick;
            Coordinate origCoordinate = brick.Coordinate;

            //int[,] bestDrop = new int[cols, 4];
            int bestX = 0;
            int bestTurns = 0;
            int bestScore = startScore;
            //Coordinate bestHight = new Coordinate(0, 0); //something high
            //int bestFit = 9999; //something high
            //int bestRemoves = 0;

            //Run through every way a brick can be turned
            for (int turns = 0; turns < 4; turns++)
            {
                //Run thrugh every place a brick can land
                for (int x = -3; x < cols; x++)
                {
                    Coordinate xCoordinate = new Coordinate(x, 0);
                    Coordinate changedCoordinate = xCoordinate;
                    Shape shape = Brick.GetShape(turns);

                    //Test if brick is inside playfield
                    if (Field.InField(brick, shape, ref changedCoordinate, null, false)
                        && changedCoordinate == xCoordinate)
                    {
                        Coordinate droppedCoordinate = DroppedCoordinate(brick, shape, xCoordinate);
                        List<Coordinate> coordinates = shape.GetCoordinates(droppedCoordinate);


                        //Find best score
                        int score = BestScore(bestScore, coordinates);
                        if (score > bestScore)
                        {
                            bestX = x;
                            bestTurns = turns;
                            bestScore = score;
                        }
                    }
                }
            }
            //System.Windows.Forms.MessageBox.Show(bestX.ToString() + "  " + bestTurns.ToString());
            //DropBrick(bestX, bestTurns);
            this.bestX = bestX;
            this.bestTurns = bestTurns;

            //System.Windows.Forms.MessageBox.Show(
            //    "origCoordinate: " + origCoordinate.X.ToString()
            //    + "  bestX:" + bestX.ToString()
            //    + "  bestDrop:" + bestDrop.ToString()
            //    + "  bestFit:" + bestFit.ToString()
            //    + "  bestTurns:" + bestTurns.ToString());

        }

        private Coordinate DroppedCoordinate(Brick brick, Shape shape, Coordinate xCoordinate)
        {
            //Find where the brick will be when dropped
            Coordinate yCoordinate = xCoordinate;
            Coordinate changedCoordinate;
            do
            {
                yCoordinate = yCoordinate + new Coordinate(0, 1);
                changedCoordinate = yCoordinate;
            }
            while (Field.InField(brick, shape, ref changedCoordinate, null, false)
                && changedCoordinate == yCoordinate);
            return yCoordinate + new Coordinate(0, -1);
        }

        private void WriteScore()
        {
            if (Brick == null)
            {
                return;
            }
            Coordinate droppedCoordinate = DroppedCoordinate(Brick, Brick.Shape, Brick.Coordinate);
            List<Coordinate> coordinates = Brick.Shape.GetCoordinates(droppedCoordinate);
            BestScore(startScore, coordinates);
        }

        private int BestScore(int bestScore, List<Coordinate> coordinates)
        {
            int scoreFilledRows = ScoreFilledRows(coordinates);
            int scoreFilledBelow = ScoreFilledBelow(coordinates);
            int scoreHight = ScoreHight(coordinates);
            int scoreNeighbours = ScoreNeighbours(coordinates);
            int scoreHoles = ScoreHoles(coordinates);
            int scoreUndergroundHoles = ScoreUndergroundHoles(coordinates);
            int score = scoreFilledRows + scoreFilledBelow + scoreHight + scoreNeighbours
                + scoreHoles + scoreUndergroundHoles;

            //Find best score
            if (score > bestScore)
            {
                bestScore = score;
                int line = 1;
                draw.DrawString(0, line++, "AI Score: " + score);
                draw.DrawString(0, line++, "AI FilledRows: " + scoreFilledRows);
                draw.DrawString(0, line++, "AI FilledBelow: " + scoreFilledBelow);
                draw.DrawString(0, line++, "AI Hight: " + scoreHight);
                draw.DrawString(0, line++, "AI Neighbours: " + scoreNeighbours);
                draw.DrawString(0, line++, "AI Holes: " + scoreHoles);
                draw.DrawString(0, line++, "AI Holes: " + scoreHoles);
                draw.DrawString(0, line++, "AI UgroundHoles: " + scoreUndergroundHoles);
                draw.DrawString(0, line++, "AI bestTurns: " + bestTurns);
                draw.DrawString(0, line++, "AI bestX: " + bestX);
            }
            return bestScore;
        }

        //private void DropBrick(int bestX, int bestTurns)
        //{
        //    //Turn and move the brick to the best place
        //    Coordinate origCoordinate = Brick.Coordinate;
        //    for (int i = 0; i < bestTurns; i++)
        //    {
        //        base.Brick.TurnClockwise();
        //    }
        //    if (bestX < origCoordinate.X)
        //    {
        //        int runs = origCoordinate.X - bestX;
        //        for (int i = 0; i < runs; i++)
        //        {
        //            base.Brick.MoveLeft();
        //        }
        //    }
        //    else if (bestX > origCoordinate.X)
        //    {
        //        int runs = bestX - origCoordinate.X;
        //        for (int i = 0; i < runs; i++)
        //        {
        //            base.Brick.MoveRight();
        //        }
        //    }
        //}

        public override bool MoveLeft()
        {
            bool value = base.MoveLeft();
            WriteScore();
            return value;
        }

        public override bool MoveRight()
        {
            bool value = base.MoveRight();
            WriteScore();
            return value;
        }

        public override bool TurnClockwise()
        {
            bool value = base.TurnClockwise();
            WriteScore();
            return value;
        }

        public override bool TurnCounterclockwise()
        {
            bool value = base.TurnCounterclockwise();
            WriteScore();
            return value;
        }

        public override bool MoveDown()
        {
            AIMove();
            WriteScore();
            return base.MoveDown();
        }

    }


}
