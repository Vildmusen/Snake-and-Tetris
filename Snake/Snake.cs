using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    /// <summary>
    /// Klassen för spelaren, aka ormen.
    /// </summary>
    class Snake
    {
        public enum Direction { Up, Right, Down, Left };

        private List<List<int>> Tail = new List<List<int>>();
        public int TailLength;
        private List<int> Recent;
        
        private Direction CurrentDirection;
        private int Speed;
        private int Xpos;
        private int Ypos;

        SolidBrush SnakeBrush;

        /// <summary>
        /// Sätter en hastighet, startposition, och svanslängd och gör en pensel som används för att måla ut ormen
        /// </summary>
        public Snake()
        {
            Speed = 20;
            Xpos = 200;
            Ypos = 200;
            TailLength = 1;
            SnakeBrush = new SolidBrush(Color.Blue);
        }

        /// <summary>
        /// Tar en siffra som input (0-3) och ändrar ormens riktning baserat på den. if-satserna kollar så att ormen in svänger 180 grader in i sig själv.
        /// </summary>
        /// <param name="dir"></param>
        public void UpdateSnakeDirection(int dir)
        {
            Direction UpdatedDir = (Direction)dir;
            if (dir == 0 && CurrentDirection != Direction.Down) CurrentDirection = UpdatedDir;
            if (dir == 1 && CurrentDirection != Direction.Left) CurrentDirection = UpdatedDir;
            if (dir == 2 && CurrentDirection != Direction.Up) CurrentDirection = UpdatedDir;
            if (dir == 3 && CurrentDirection != Direction.Right) CurrentDirection = UpdatedDir;
        }

        /// <summary>
        /// Sparar senaste positionen ormen hade som en del av svansen.
        /// </summary>
        private void AddOnTail()
        {
            Recent = new List<int>();
            Recent.Add(Xpos);
            Recent.Add(Ypos);
            Tail.Insert(0, Recent);
        }

        /// <summary>
        /// Ökar längden av ormens svans med 1.
        /// </summary>
        public void GrowTail()
        {
            TailLength++;
        }

        /// <summary>
        /// Uppdaterar koordinaterna för ormens position med hjälp av ormens riktning och hastighet.
        /// </summary>
        public void UpdateSnakePosition()
        {
            if (CurrentDirection == Direction.Up) Ypos -= Speed;
            if (CurrentDirection == Direction.Right) Xpos += Speed;
            if (CurrentDirection == Direction.Down) Ypos += Speed;
            if (CurrentDirection == Direction.Left) Xpos -= Speed;
            AddOnTail();
        }

        /// <summary>
        /// Utför två kollar: först om ormen är kvar på skärmen, sen om den har åkt in i sig själv.
        /// </summary>
        /// <returns> Sant om ormen har åkt utanför skärmen eller in i sig själv, annars false. </returns>
        public bool IsDead()
        {
            //Kollar om ormen är kvar på skärmen
            if (Xpos < 0 || Xpos > 400) return true;
            if (Ypos < 0 || Ypos > 400) return true;

            //Kollar om ormen har samma position som någon annan del av sin svans. (men bara de "TailLength" första delarna av svansen #ghettolösning)
            for (int i = 1; i < TailLength; i++)
            {
                if(Xpos == Tail[i][0] && Ypos == Tail[i][1])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Skickar tillbaka positionen av ormens "huvud" alltså där spelaren är just nu.
        /// </summary>
        /// <returns></returns>
        public List<int> GetHead()
        {
            return new List<int> { Xpos, Ypos };
        }

        /// <summary>
        /// Ritar ut ormen med hjälp av penseln.
        /// </summary>
        /// <param name="Drawing"></param>
        public void DrawSnake(Graphics Drawing)
        {
            for (int i = 0; i < TailLength; i++)
            {
                Drawing.FillRectangle(SnakeBrush, new Rectangle(Tail[i][0], Tail[i][1], 20, 20));
            }
        }
    }
}
