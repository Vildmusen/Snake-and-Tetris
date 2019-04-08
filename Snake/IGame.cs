using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    interface IGame
    {
        void StartGame();
        void GameLoop(Object myObject, EventArgs gameEvent);
        void Input(int dir);
        string GetScore();
    }
}
