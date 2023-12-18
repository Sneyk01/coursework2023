using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPongGame.Classes
{
    internal class Ball : Object
    {
        private double[] _speeds;

        public Ball()
        {
            this._coord     = new Point(50, 50);
            this._radius    = 5;
            this._speeds    = new double[2];
            this._speeds[0] = 0;
            this._speeds[1] = 0;
        }

        public Ball(Point coord, int radius, double[] speeds) : base(coord, radius)
        {
            this._speeds = speeds;
        }

        public void revers(int axis)  // 0 - X axis, 1 - Y axis
        {
            this._speeds[axis] *= -1;
        }

        public Point getFutureCoord()
        {
            Point futureCoord = new Point(Convert.ToInt32(this._coord.X + this._speeds[0]), Convert.ToInt32(this._coord.Y + this._speeds[1]));
            return futureCoord;
        }
        public double[] getSpeed() { return this._speeds; }

        public void setSpeed(double vx, double vy)
        {
            this._speeds[0] = vx;
            this._speeds[1] = vy;
        }

        public void update()
        {
            double maxSpeed = 10 * (this._radius / 5);
            double minSpeed = -10 * (this._radius / 5);

            if (this._speeds[0] > maxSpeed) { this._speeds[0] = maxSpeed;}
            if (this._speeds[1] > maxSpeed) { this._speeds[1] = maxSpeed;}
            if (this._speeds[0] < minSpeed) { this._speeds[0] = minSpeed;}
            if (this._speeds[1] < minSpeed) { this._speeds[1] = minSpeed;}

            this._coord.X = Convert.ToInt32(this._coord.X + this._speeds[0]);
            this._coord.Y = Convert.ToInt32(this._coord.Y + this._speeds[1]);
        }

        public void setRandomSpeed()
        {
            int maxSpeed = 4 * (this._radius / 5);
            int minSpeed = 1 * (this._radius / 5);

            Random rnd = new Random();

            this._speeds[0] = rnd.Next(minSpeed + 1, maxSpeed); // vx little more than vy
            this._speeds[1] = rnd.Next(minSpeed, maxSpeed);

            if (rnd.Next(0, 2) == 0)
                this._speeds[0] *= -1;

            if (rnd.Next(0, 2) == 0)
                this._speeds[1] *= -1;
        }

        public Ball getBall()
        {
            Ball tempBall = new Ball(_defaultCoord, _radius, _speeds);
            tempBall.setCoord(_coord);
            return tempBall;
        } 
    }
}
