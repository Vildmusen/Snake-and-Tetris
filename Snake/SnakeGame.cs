using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Snake
{
    class SnakeGame : IGame
    {
        Graphics Drawing;
        Timer Timer;
        Snake Player;
        List<Mat> Mat;
        bool Running;

        /// <summary>
        /// Gör en lista som används för att hålla i "Mat". Sätter running = true, vilket för game-loopen att köra.
        /// </summary>
        /// <param name="g"></param>
        public SnakeGame(Graphics g)
        {
            Drawing = g;
            Mat = new List<Mat>();
            Running = true;
        }

        /// <summary>
        /// Gör en ny orm, timer och lägger in en initial "Mat" i listan för maten. Kör igång gameloopen.
        /// </summary>
        public void StartGame()
        {
            Player = new Snake();
            Timer = new Timer();
            Mat.Add(new Mat());

            // Denna rad lägger till vad som ska ske varje loop. Alltså ska funktionen "GameLoop" köras varje gång den tickar en gång. (Tror jag... haha har inte stenkoll :'D)
            Timer.Tick += new EventHandler(GameLoop);
            Timer.Interval = 100;
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

            Drawing.Clear(Color.White);
            Player.UpdateSnakePosition();
            CheckCollisions();
            Mat[0].DrawMat(Drawing);
            Player.DrawSnake(Drawing);

            Timer.Enabled = true;
        }

        /// <summary>
        /// Kollar om ormen är på samma position som en matbit och tar isf bort den och lägger till en ny matbit i listan. Ökar då ormens längd med 1. 
        /// Kollar också om ormen är död med funktionen "IsDead", och sätter running = false för att avbryta spelet isf.
        /// </summary>
        public void CheckCollisions()
        {
            //Kollar om ormen äter någon mat
            if(Math.Abs(Player.GetHead()[0] - Mat[0].GetPos()[0]) < 20 && 
                Math.Abs(Player.GetHead()[1] - Mat[0].GetPos()[1]) < 20)
            {
                Mat.RemoveAt(0);
                Mat.Add(new Mat());
                Player.GrowTail();
            }

            //Kollar om ormen har åkt av skärmen eller in i sig själv
            if (Player.IsDead())
            {
                Running = false;
            }
        }

        /// <summary>
        /// Uppdaterar ormens riktning genom att kalla på ormens uppdateringsfunktion.
        /// </summary>
        /// <param name="dir"></param>
        public void Input(int dir)
        {
            Player.UpdateSnakeDirection(dir);
        }

        public string GetScore()
        {
            return "";
        }
    }
}
