using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace myPongGame.Classes
{
    internal class Computer : Board
    {
        private int _difficultyLevel;
        private Point _oldBallCoord;

        private double P;
        private double I;
        private double D;
        private double oldErr;

        private double kp;
        private double ki;
        private double kd;

        public Computer()
        {
            this._difficultyLevel = 0;
            this._oldBallCoord = new Point();
        }
        public Computer(Point coord, int[] parameters, int radius, int speed, double acceleration, int level) : base(coord, parameters, radius, speed, acceleration)
        {
            this._difficultyLevel = level;
            this._oldBallCoord = new Point();

            P = 0;
            I = 0;
            D = 0;
            oldErr = 0;
            kp = 0.007;
            ki = 0.0009;
            kd = 0.000007;
        }

        public override void update()
        {
            double Ft = 0.05;
            int maxSpeed = 8;

            // Easy level
            if (this._difficultyLevel == 0)
            {
                if (this.moveCode[0] == 1 && this.moveCode[1] == 0)
                {
                    if (this._speed > -maxSpeed)
                        this._speed -= this._acceleration;
                }
                else if (this.moveCode[0] == 0 && this.moveCode[1] == 1)
                {
                    if (this._speed < maxSpeed)
                        this._speed += this._acceleration;
                }
                else if (this.moveCode[0] == 1 && this.moveCode[1] == 1)
                {
                    this._speed = 0;
                }

                this._coord.Y = Convert.ToInt32(this._coord.Y + this._speed);
                if (this._speed > Ft)
                    this._speed -= Ft;
                else if (this._speed < -Ft)
                    this._speed += Ft;
            }

            // Hard level
            if (this._difficultyLevel == 1)
            {
                double pidReg = kp * P + ki * I + kd * D;

                if (pidReg < -0.3)
                    pidReg = -0.3;
                if (pidReg > 0.3)
                    pidReg = 0.3;

                this._acceleration = pidReg;
                this._speed += pidReg;

                this._coord.Y = Convert.ToInt32(this._coord.Y + this._speed);
                if (this._speed > Ft)
                    this._speed -= Ft;
                else if (this._speed < -Ft)
                    this._speed += Ft;
            }

        }

        public override void findBall(Point ballCoord)
        {
            base.findBall(ballCoord);
            int dx = Math.Abs(this._coord.X - ballCoord.X);
            int oldDx = Math.Abs(this._coord.X - this._oldBallCoord.X);

            // If ball -> platform
            if (oldDx > dx)
            {
                // Easy level
                if (this._difficultyLevel == 0)
                {
                    Point platformCenter = this._coord;
                    platformCenter.Y += this._params[1] / 2;

                    if (ballCoord.Y > platformCenter.Y)
                    {
                        this.moveCode[0] = 0;
                        this.moveCode[1] = 1;
                    }
                    if (ballCoord.Y < platformCenter.Y)
                    {
                        this.moveCode[0] = 1;
                        this.moveCode[1] = 0;
                    }

                    if (this._isView)
                    {
                        this.moveCode[0] = 0;
                        this.moveCode[1] = 0;
                    }
                }

                // Hard level
                if (this._difficultyLevel == 1)
                {
                    double dt = 1 / 60;
                    Point platformCenter = this._coord;
                    platformCenter.Y += this._params[1] / 2;

                    P = ballCoord.Y - platformCenter.Y;
                    I = I + P * dt;
                    if (P == oldErr || (P + oldErr > -0.001 && P + oldErr < 0.001))
                        D = 0;
                    else
                        D = (P + oldErr) / dt;

                    oldErr = P;
                }
            }
            else
            {
                // Easy level
                if (this._difficultyLevel == 0)
                {
                    this.moveCode[0] = 0;
                    this.moveCode[1] = 0;
                }

                // Hard level
                if (this._difficultyLevel == 1)
                {
                    double dt = 1 / 60;
                    Point platformCenter = this._coord;
                    platformCenter.Y += this._params[1] / 2;

                    P = ballCoord.Y - platformCenter.Y;
                    I = I + P * dt;
                    if (P == oldErr || (P + oldErr > -0.001 && P + oldErr < 0.001))
                        D = 0;
                    else
                        D = (P + oldErr) / dt;

                    oldErr = P;
                }
            }

            this._oldBallCoord = ballCoord;
        }
    }
}
