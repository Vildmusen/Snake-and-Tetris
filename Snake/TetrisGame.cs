using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Snake
{
    class TetrisGame : IGame
    {
        Graphics Drawing;
        SolidBrush Background;
        Timer Timer;
        BlockStruct CurrentBlock;
        List<BlockStruct> Holder;
        BlockStruct HoldBlock;
        List<Block> Solids;
        Random rnd = new Random();
        bool Running;
        int Combo;
        int Score;
        int Level;
        int TetrisCount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public TetrisGame(Graphics g)
        {
            Drawing = g;
            Background = new SolidBrush(Color.Gray);
            CurrentBlock = new BlockStruct(rnd.Next(7));
            Solids = new List<Block>();
            Holder = new List<BlockStruct>();
            Running = true;
            Score = 0;
            Level = 0;
            TetrisCount = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartGame()
        {
            Timer = new Timer();

            // Denna rad lägger till vad som ska ske varje loop. Alltså ska funktionen "GameLoop" köras varje gång den tickar en gång. (Tror jag... haha har inte stenkoll :'D)
            Timer.Tick += new EventHandler(GameLoop);
            Timer.Interval = 300;
            Timer.Start();

            while (Running)
            {
                // Får Timern att köra sin "tick", aka GameLoop() (Antar jag haha)
                Application.DoEvents();
            }
            Timer.Dispose();
        }

        /// <summary>
        /// Funktionen som körs varje tick: 
        /// Stoppar timern -> Målar bakgrunden vit -> Uppdaterar ormens position -> Kollar kollisioner -> Målar ut Mat och orm -> Startar timern igen
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="gameEvent"></param>
        public void GameLoop(Object myObject, EventArgs gameEvent)
        {
            Timer.Stop();

            if (Collisions() || CurrentBlock.FallingUpdate())
            {
                foreach(Block part in CurrentBlock.GetParts())
                {
                    Solids.Add(part);
                }
                CurrentBlock = new BlockStruct(rnd.Next(7));
            }

            CheckTetris();
            DrawAll();
            CheckDead();

            Timer.Enabled = true;
        }

        public void Input(int input)
        {
            if (input == 5)
            {
                Hold();
            }
            else if (input == 4)
            {
                FallToBottom();
            }
            else if (input == 2)
            {
                CurrentBlock.FallingUpdate();
            }
            else if (input == 0)
            {
                bool allowed = true;
                CurrentBlock.Rotate();

                foreach(Block part in CurrentBlock.GetParts())
                {
                    foreach(Block solid in Solids)
                    {
                        if(part.XPos == solid.XPos && part.YPos == solid.YPos)
                        {
                            allowed = false;
                        }
                    }
                }
                allowed = CurrentBlock.NoBlockOutside(0);
                if (!allowed)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        CurrentBlock.Rotate();
                    }
                }
                else
                {
                    
                }
            }
            else
            {
                bool allowed = true;
                int range = 0;
                if (input == 1) range = 20;
                if (input == 3) range = -20;

                foreach (Block part in CurrentBlock.GetParts())
                {
                    foreach (Block solid in Solids)
                    {
                        if(part.XPos + range == solid.XPos && 
                            part.YPos == solid.YPos)
                        {
                            allowed = false;
                        }
                    }
                }
                if (allowed)
                {
                    CurrentBlock.InputUpdate(input);
                }
            }
            DrawAll();
        }

        public void Hold()
        {
            if(Holder.Count > 0)
            {
                Holder.Add(CurrentBlock.SetHold());
                CurrentBlock = new BlockStruct(Holder[0].GetTypeNum());
                Holder.RemoveAt(0);
            }
            else
            {
                Holder.Add(CurrentBlock.SetHold());
                CurrentBlock = new BlockStruct(rnd.Next(7));
            }
        }

        public void DrawAll()
        {
            Drawing.Clear(Color.Black);
            Drawing.FillRectangle(Background, new Rectangle(0,0,200,420));
            CurrentBlock.DrawStructure(Drawing);
            if(Holder.Count > 0) Holder[0].DrawStructure(Drawing);

            foreach (Block block in Solids)
            {
                block.DrawBlock(Drawing);
            }
        }

        public bool Collisions()
        {  
            foreach (Block CurrentPart in CurrentBlock.GetParts())
            {
                foreach (Block part in Solids)
                {
                    if (CurrentPart.YPos + 20 == part.YPos &&
                        CurrentPart.XPos == part.XPos)
                    {
                        return true;
                    }
                }   
            }
            
            return false;
        }

        public void FallToBottom()
        {
            while (!Collisions())
            {
                if (CurrentBlock.FallingUpdate())
                {
                    break;
                }
            }
        }

        public void CheckTetris()
        {
            Combo = 0;

            for (int i = 0; i < 22; i++)
            {
                CheckRow(i * 20);
            }

            switch (Combo)
            {
                case 1:
                    Score += Level * 40;
                    break;
                case 2:
                    Score += Level * 100;
                    break;
                case 3:
                    Score += Level * 300;
                    break;
                case 4:
                    Score += Level * 1200;
                    break;
                default:
                    break;
            }

            TetrisCount += Combo;

            if(TetrisCount > 10)
            {
                TetrisCount -= 10;
                Level++;
                Timer.Interval = 300 - (Level * 40);
            }

            Combo = 0;
            
        }

        public void CheckRow(int Height)
        {
            int count = 0;
            List<Block> ToBeRemoved = new List<Block>();

            for (int i = 0; i < Solids.Count; i++)
            {
                if (Solids[i].YPos == Height)
                {
                    count++;
                    ToBeRemoved.Add(Solids[i]);
                }
            }

            if(count >= 10)
            {
                Combo++;
                foreach(Block i in ToBeRemoved)
                {
                    Solids.Remove(i);
                }
                foreach(Block part in Solids)
                {
                    if(part.YPos < Height)
                    {
                        part.YPos += 20;
                    }
                }
            }
        }

        public void CheckDead()
        {
            foreach(Block block in Solids)
            {
                if(block.YPos < 0)
                {
                    Running = false;
                }
            }
        }

        public string GetScore()
        {
            return Score.ToString();
        }
    }
}
