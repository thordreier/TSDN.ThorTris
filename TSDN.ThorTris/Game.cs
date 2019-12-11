using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using TSDN.ThorTris.Bricks;
using TSDN.ThorTris.Fields;


namespace TSDN.ThorTris
{


    public class Game
    {

        //fixme
        private static int cols = 10;
        //private static int rows = 20;
        private static int rows = 20;


        private Form form;
        private Field field;
        private GameDraw draw;
        private BrickDispenser brickDispenser;
        private System.Windows.Forms.Timer timer;
        private Player player;

        private BufferedGraphicsContext bufferedGraphicsContext;
        private BufferedGraphics bufferedGraphics;

        public Game(Form form)
        {
            this.form = form;

            field = new Field(cols, rows);
            
            Graphics canvas = form.CreateGraphics();
            draw = new GameDraw(canvas, field);

            brickDispenser = new BrickDispenser(field, draw);

            //player = new Player(0, brickDispenser, field, draw);
            player = new AIPlayer(0, brickDispenser, field, draw);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 10;
            timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(MoveStuff);

            draw.Draw(); //fixme
        }

        public void KeyPressed(Keys key)
        {
            switch (key)
            {
                case Keys.Left:
                    player.MoveLeft();
                    break;
                case Keys.Right:
                    player.MoveRight();
                    break;
                case Keys.Up:
                    player.TurnClockwise();
                    break;
                case Keys.Down:
                    player.MoveDown();
                    break;
                case Keys.Z:
                    player.TurnCounterclockwise();
                    break;
                case Keys.Space:
                    player.MoveToBottom();
                    break;
                case Keys.P:
                    timer.Enabled = !timer.Enabled;
                    break;
                //case Keys.Q:
                //    brick = brickDispenser.NewBrick();
                //    break;
            }
            draw.Draw(); //fixme
        }

        public void MoveStuff(object sender, EventArgs e)
        {
            player.TimerTick();
            draw.Draw(); //fixme
        }

    }


}
