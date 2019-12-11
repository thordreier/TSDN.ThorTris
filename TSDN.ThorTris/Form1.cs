using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TSDN.ThorTris
{
    public partial class Form1 : Form
    {

        private Game game;

        public Form1()
        {
            InitializeComponent();

            game = new Game(this);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.KeyPressed(e.KeyData);
        }

    }
}