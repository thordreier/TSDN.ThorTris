using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using TSDN.ThorTris.Fields;


namespace TSDN.ThorTris.Bricks.Types
{

    
    public class BrickL : Brick
    {

        #region static shape
        private static bool[,] s1 ={
            { false, false, false, false },
            { true , false, false, false },
            { true , false, false, false },
            { true , true , false, false } };
        private static bool[,] s2 ={
            { false, false, false, false },
            { true , true , true , false },
            { true , false, false, false },
            { false, false, false, false } };
        private static bool[,] s3 ={
            { false, false, false, false },
            { false, true , true , false },
            { false, false, true , false },
            { false, false, true , false } };
        private static bool[,] s4 ={
            { false, false, false, false },
            { false, false, false, false },
            { false, false, true , false },
            { true , true , true , false } };
        private static Shape[] shapesBrick = { new Shape(s1), new Shape(s2), new Shape(s3), new Shape(s4) };
        #endregion

        public BrickL(Field field, GameDraw draw)
            : base(field, draw)
        {
            shapes = shapesBrick;
            color = Color.Magenta;
        }

    }


}
