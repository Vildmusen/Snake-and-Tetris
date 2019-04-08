using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        IGame game;
        Graphics Drawing;

        public Form1()
        {
            InitializeComponent();
            Drawing = this.CreateGraphics();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Lysnnar efter knapptryck. Enter för att starta spelet, esc för att stänga av och upp-, höger-, ner- eller vänsterpil för att ändra ormens riktning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                label1.Hide();
                label2.Hide();
                game = new SnakeGame(Drawing);
                game.StartGame();
                label1.Show();
                label3.Show();
            }
            if(e.KeyData == Keys.T)
            {
                label1.Hide();
                label2.Hide();
                label4.Show();
                Form1.ActiveForm.Width = 315;
                game = new TetrisGame(Drawing);
                game.StartGame();
                label1.Show();
                label3.Show();
            }
            if(e.KeyData == Keys.Escape)
            {
                Application.Exit();
            }

            if (e.KeyData == Keys.Up)
            {
                game.Input(0);
            }
            if (e.KeyData == Keys.Right)
            {
                game.Input(1);
            }
            if (e.KeyData == Keys.Down)
            {
                game.Input(2);
            }
            if (e.KeyData == Keys.Left)
            {
                game.Input(3);
            }
            if (e.KeyData == Keys.Space)
            {
                game.Input(4);
            }
            if (e.KeyData == Keys.Z)
            {
                game.Input(5);
            }
            UpdateScore();
        }

        public void UpdateScore()
        {
            label4.Text = "Score: " + game.GetScore();
        }
    }
}
