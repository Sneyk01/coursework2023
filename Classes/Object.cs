using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPongGame.Classes
{
    internal class Object
    {
        protected Point _coord;
        protected Point _defaultCoord;
        protected int _radius;

        public Object() {
            this._coord = new Point();
            this._defaultCoord = new Point(50, 50);
            this._radius = 5;
        }
        public Object(Point defCoord) {
            this._coord = defCoord;
            this._defaultCoord = defCoord;
        }        
        public Object(Point defCoord, int radius) {
            this._coord = defCoord;
            this._defaultCoord = defCoord;
            this._radius = radius;
        }

        public Point getCoord() { return _coord; }
        public void setCoord(Point point) { this._coord =  point;}
        public void setDefCoord(Point defCoord)
        {
            this._defaultCoord = defCoord;
        }
        public int getRadius() { return this._radius; }

        public void setRadius(int radius) {  this._radius = radius;}

        public virtual void reset()
        {
            this._coord = this._defaultCoord;
        }
    }
}
