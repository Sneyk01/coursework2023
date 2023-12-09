using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPongGame.Classes
{
    internal class Wall : Object
    {
        private int[] _params;
        private int   _wallType;  // 0 - common wall, 1 - rounded down, 2 - rounded up, 3 - rounded wall

        public Wall() : base()
        {
            this._radius   = 2;
            this._params   = new int[1] { 4 };
            this._wallType = 0;
        }

        public Wall(Point wallCoord, int wallRadius, int length, int wallType) : base(wallCoord, wallRadius) 
        {
            this._params   = new int[1] { length };
            this._wallType = wallType;
        }

        public int[] getParams() { return this._params; }
        public int getWallType() { return this._wallType; }
        public void setParams(int[] parameters) { this._params = parameters; }
    }
}
