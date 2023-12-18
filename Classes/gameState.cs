using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPongGame.Classes
{
    internal class gameState
    {
        public int id;
        public int botSetting;
        public int mapSetting;
        public int mapSizeSetting;
        public int ballCoordX;
        public int ballCoordY;
        public double ballSpeedX;
        public double ballSpeedY;
        public int ballRadius;
        public int playerBoardX;
        public int playerBoardY;
        public int computerBoardX;
        public int computerBoardY;
        public double playerBoardAcc;
        public double computerBoardAcc;
        public TimeSpan gameTime;
        public int playerGoals;
        public int computerGoals;
    }
}
