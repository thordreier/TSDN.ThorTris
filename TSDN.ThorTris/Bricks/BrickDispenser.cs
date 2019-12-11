using System;
using System.Collections.Generic;
using System.Text;

using TSDN.ThorTris.Bricks.Types;
using TSDN.ThorTris.Fields;


namespace TSDN.ThorTris.Bricks
{

    
    public class BrickDispenser
    {

        private Field field;
        private GameDraw draw;

        private static Random rnd = new Random();

        public BrickDispenser(Field field, GameDraw draw)
        {
            this.field = field;
            this.draw = draw;
        }

        public Brick NewBrick()
        {
            Brick brick = null;
            int random = rnd.Next(7);
            switch (random)
            {
                case 0:
                    brick = new BrickI(field, draw);
                    break;
                case 1:
                    brick = new BrickJ(field, draw);
                    break;
                case 2:
                    brick = new BrickL(field, draw);
                    break;
                case 3:
                    brick = new BrickO(field, draw);
                    break;
                case 4:
                    brick = new BrickS(field, draw);
                    break;
                case 5:
                    brick = new BrickT(field, draw);
                    break;
                case 6:
                    brick = new BrickZ(field, draw);
                    break;
            }
            //brick = new BrickT(field, draw); //fixme

            if (!brick.MoveIntoField())
            {
                return null;
            }
            return brick;
        }

    }


}
