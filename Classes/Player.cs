using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPongGame.Classes
{
    internal class Player : Board
    {
        private int _lives;
        public Player() {
            this._lives = 5;
            this._goals = 0;
        }
        public Player(Point coord, int[] parameters, int radius, int speed, double acceleration) : base(coord, parameters, radius, speed, acceleration) {
            this._lives = 5;
            this._goals = 0;
        }

        public override int[] getBoardScoreStatus() { return new int[] { this._goals, this._lives }; }

        public override void lose()
        {
            this._lives -= 1;
        }
    }
}
