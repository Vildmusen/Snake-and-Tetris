using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class BlockStruct
    {
        enum BlockType { I, O, T, S, Z, J, L }

        BlockType Type;
        List<Block> Structure;
        Random rnd;
        public int XPos;
        public int YPos;

        public BlockStruct(int Type)
        {
            this.Type = (BlockType)Type;
            rnd = new Random();

            YPos = -40;
            XPos = 60;

            Structure = new List<Block>();

            CreateStruct(Type);
        }

        public BlockStruct(int Type, int x, int y)
        {
            this.Type = (BlockType)Type;
            rnd = new Random();

            YPos = y;
            XPos = x;

            Structure = new List<Block>();

            CreateStruct(Type);
        }

        public void CreateStruct(int Type)
        {
            if (this.Type == BlockType.I)
            {
                YPos = -20;
                Structure.Add(new Block(XPos, YPos, Type));
                Structure.Add(new Block(XPos + 20, YPos, Type));
                Structure.Add(new Block(XPos + 40, YPos, Type));
                Structure.Add(new Block(XPos + 60, YPos, Type));
            }
            if (this.Type == BlockType.O)
            {
                Structure.Add(new Block(XPos, YPos, Type));
                Structure.Add(new Block(XPos + 20, YPos, Type));
                Structure.Add(new Block(XPos, YPos - 20, Type));
                Structure.Add(new Block(XPos + 20, YPos - 20, Type));
            }
            if (this.Type == BlockType.T)
            {
                Structure.Add(new Block(XPos, YPos, Type));
                Structure.Add(new Block(XPos + 20, YPos, Type));
                Structure.Add(new Block(XPos + 40, YPos, Type));
                Structure.Add(new Block(XPos + 20, YPos + 20, Type));
            }
            if (this.Type == BlockType.S)
            {
                Structure.Add(new Block(XPos, YPos + 20, Type));
                Structure.Add(new Block(XPos + 20, YPos, Type));
                Structure.Add(new Block(XPos + 20, YPos + 20, Type));
                Structure.Add(new Block(XPos + 40, YPos, Type));
            }
            if (this.Type == BlockType.Z)
            {
                Structure.Add(new Block(XPos, YPos, Type));
                Structure.Add(new Block(XPos + 20, YPos, Type));
                Structure.Add(new Block(XPos + 20, YPos + 20, Type));
                Structure.Add(new Block(XPos + 40, YPos + 20, Type));
            }
            if (this.Type == BlockType.J)
            {
                Structure.Add(new Block(XPos, YPos, Type));
                Structure.Add(new Block(XPos + 20, YPos + 20, Type));
                Structure.Add(new Block(XPos, YPos + 20, Type));
                Structure.Add(new Block(XPos + 40, YPos + 20, Type));
            }
            if (this.Type == BlockType.L)
            {
                Structure.Add(new Block(XPos, YPos + 20, Type));
                Structure.Add(new Block(XPos + 20, YPos + 20, Type));
                Structure.Add(new Block(XPos + 40, YPos + 20, Type));
                Structure.Add(new Block(XPos + 40, YPos, Type));
            }
        }

        public void Rotate()
        {
            if (Type == BlockType.S || Type == BlockType.Z || Type == BlockType.L || Type == BlockType.J || Type == BlockType.T)
            {
                int XPivot = Structure[1].XPos;
                int YPivot = Structure[1].YPos;

                foreach(Block part in Structure)
                {
                    if(Math.Sin(part.XPos - XPivot) == 0)
                    {
                        if(Math.Sin(part.YPos -YPivot) < 0)
                        {
                            part.XPos += 20;
                            part.YPos += 20;
                        } else if(Math.Sin(part.YPos - YPivot) > 0)
                        {
                            part.XPos -= 20;
                            part.YPos -= 20;
                        }
                    }
                    else if (Math.Sin(part.XPos - XPivot) < 0)
                    {
                        if (Math.Sin(part.YPos - YPivot) < 0)
                        {
                            part.XPos += 40;
                        }
                        else if (Math.Sin(part.YPos - YPivot) == 0)
                        {
                            part.XPos += 20;
                            part.YPos -= 20;
                        } else
                        {
                            part.YPos -= 40;
                        }
                    }
                    else
                    {
                        if (Math.Sin(part.YPos - YPivot) < 0)
                        {
                            part.YPos += 40;
                        }
                        else if (Math.Sin(part.YPos - YPivot) == 0)
                        {
                            part.XPos -= 20;
                            part.YPos += 20;
                        }
                        else
                        {
                            part.XPos -= 40;
                        }
                    }
                }
                
            }
            else if (Type == BlockType.I)
            {
                int XPivot = Structure[1].XPos;
                int YPivot = Structure[1].YPos;

                foreach (Block part in Structure)
                {
                    int oldY = part.YPos;
                    part.YPos = part.XPos + (YPivot - XPivot);
                    part.XPos = oldY - (YPivot - XPivot);
                }
            }
            else if (Type == BlockType.O)
            {

            }
            else
            {

            }
            
        }

        public bool FallingUpdate()
        {
            if (!ReachedBottom())
            {
                foreach(Block part in Structure)
                {
                    part.YPos += 20;
                }
                return false;
            }
            else return true;
        }

        public bool ReachedBottom()
        {
            foreach(Block part in Structure)
            {
                if(part.YPos >= 400)
                {
                    return true;
                }
            }
            return false;
        }

        public void InputUpdate(int dir)
        {
            if (dir == 1)
            {
                if (NoBlockOutside(20))
                {
                    foreach (Block part in Structure)
                    {
                        part.XPos += 20;  
                    }
                }
            }

            if (dir == 3)
            {
                if (NoBlockOutside(-20))
                {
                    foreach (Block part in Structure)
                    {
                        part.XPos -= 20;
                    }
                }
            }
        }

        public bool NoBlockOutside(int difference)
        {
            foreach(Block part in Structure)
            {
                if(part.XPos + difference < 0 || part.XPos + difference >= 200)
                {
                    return false;
                }
            }
            return true;
        }

        public BlockStruct SetHold()
        {
            return new BlockStruct((int)Type, 220, 40);
        }

        public void DrawStructure(Graphics Drawing)
        {
            foreach(Block part in Structure)
            {
                part.DrawBlock(Drawing);
            }
        }

        public List<Block> GetParts()
        {
            return Structure;
        }

        public int GetTypeNum()
        {
            return (int)Type;
        }

    }
}