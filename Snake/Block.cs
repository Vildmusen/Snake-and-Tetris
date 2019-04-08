using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class Block
    {
        enum BlockType { I, O, T, S, Z, J, L }

        public int XPos;
        public int YPos;
        SolidBrush BlockBrush;

        public Block(int XPos, int YPos, int Type)
        {
            Color BlockClr = new Color();
            if (Type == 0) BlockClr = Color.LightBlue;
            if (Type == 1) BlockClr = Color.Yellow;
            if (Type == 2) BlockClr = Color.Purple;
            if (Type == 3) BlockClr = Color.Green;
            if (Type == 4) BlockClr = Color.Red;
            if (Type == 5) BlockClr = Color.Blue;
            if (Type == 6) BlockClr = Color.Orange;
            BlockBrush = new SolidBrush(BlockClr);

            this.XPos = XPos;
            this.YPos = YPos;
        }

        public void DrawBlock(Graphics Drawing)
        {
            Drawing.FillRectangle(BlockBrush, new Rectangle(XPos, YPos, 20, 20));
        }

        public int CurrentYPos()
        {
            return YPos;
        }

        public int CurrentXPos()
        {
            return XPos;
        }
    }
}
