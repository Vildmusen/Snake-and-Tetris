using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    /// <summary>
    /// Class som fungerar som "maten" ormen äter.
    /// </summary>
    class Mat
    {
        private int Xpos;
        private int Ypos;

        SolidBrush MatBrush;

        /// <summary>
        /// Skapar en random position på spelbrädet och den pensel som ska användas för att "måla" maten
        /// </summary>
        public Mat()
        {
            Random rand = new Random();
            Xpos = rand.Next(20) * 20;
            Ypos = rand.Next(20) * 20;
            MatBrush = new SolidBrush(Color.Red);
        }

        /// <summary>
        /// Använder penseln för att rita ut maten.
        /// </summary>
        /// <param name="Drawing"></param>
        public void DrawMat(Graphics Drawing)
        {
            Drawing.FillRectangle(MatBrush, new Rectangle(Xpos, Ypos, 20, 20));
        }

        /// <summary>
        /// Skickar matens position.
        /// </summary>
        /// <returns></returns>
        public List<int> GetPos()
        {
            return new List<int> { Xpos, Ypos };
        }
    }
}
