using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPongGame.Classes
{
    internal class Board : Object
    {
        protected int[]  _params;
        protected double _speed;
        protected Point  _futureCoord;
        protected double _acceleration;
        protected bool   _isView;
        protected int    _goals;

        protected int[] moveCode = new int[2];

        public Board() {
            this._radius       = 10;
            this._speed        = 0;
            this._params       = new int[2];
            this._params[0]    = 20;
            this._params[1]    = 40;
            this._acceleration = 0.3;
            this._isView       = true;
            this._futureCoord  = this._coord;
            this._goals        = 0;
        }

        public Board(Point coord, int[] parameters, int radius, int speed, double acceleration) : base(coord, radius) {
            this._speed        = speed;
            this._params       = parameters;
            this._acceleration = acceleration * (this._params[1] / 40); // Special to scaling
            this._futureCoord  = this._coord;
            this._isView       = true;
            this._goals        = 0;
        }

        public void changeSpeed(Keys key, int state)
        {
            switch (key) {
                case Keys.W:
                    this.moveCode[0] = state;
                    break;

                case Keys.S:
                    this.moveCode[1] = state;
                    break;
            }
        }

        public virtual void update()
        {
            double Ft    = 0.05 * (this._acceleration / 0.3);
            int maxSpeed = 8 * (this._params[1] / 40);


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

        public virtual void win()
        {
            this._goals += 1;
        }

        public virtual void lose()
        {

        }

        public int[] getParams() { return this._params; }

        public void setParams(int[] parameters) { this._params = parameters; }
        
        public double getSpeed() { return this._speed; }

        public void setSpeed(double speed) {  this._speed = speed; }

        public double getAcceleration() { return this._acceleration; }

        public void setAcceleration(int acceleration) { this._acceleration = acceleration; }

        public virtual int[] getBoardScoreStatus() { return new int[] { this._goals }; }

        public bool isView() { return this._isView; }

        public virtual void findBall(Point ballCoord)
        {
            Point upShadow = this._coord;
            upShadow.Y += Convert.ToInt32(this._speed);

            if (ballCoord.Y >= upShadow.Y && ballCoord.Y <= upShadow.Y + this._params[1])
            {
                this._isView = true;
            }
            else
            {
                this._isView = false;
            }

        }

        public void jelly()
        {
            double hitPersent = 0.4;
            this._speed = this._speed * hitPersent * (-1);
        }

        public override void reset()
        {
            base.reset();
            this._speed = 0;
        }
    }
}
