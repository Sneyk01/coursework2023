using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace myPongGame.Classes
{
    internal class Field
    {
        protected int[] _size;
        protected int   _type; // 0 - easy, 1 - medium, 2 - hard, -1 - pause, -2 - stop
        public Wall[]   _walls;
        public Ball     _ball;
        public Board[]  _boards;
        public Graphic  _graphic;

        public double dx  = 0;
        public double de1 = 0;
        public double de2 = 0;

        public Field()
        {
            this._size    = new int[2];
            this._size[0] = 770;
            this._size[1] = 400;

            this._type = 0;
            this._ball = new Ball();

            this._boards    = new Board[2];
            this._boards[0] = new Player();
            this._boards[1] = new Computer();

            int[] board1Size = this._boards[0].getParams();
            int[] board2Size = this._boards[1].getParams();

            // set default coords
            this._ball.setDefCoord(new Point(this._size[0] / 2, this._size[1] / 2));
            this._boards[0].setDefCoord(new Point(20, (this._size[1] / 2) - board1Size[1] / 2));
            this._boards[1].setDefCoord(new Point(this._size[0] - 20 - 1, (this._size[1] / 2) - board2Size[1] / 2 ));

            this._graphic = new Graphic();
        }

        public Field(Size mapSize, int botType)
        {
            this._size = new int[2];
            this._size[0] = mapSize.Width;
            this._size[1] = mapSize.Height;

            this._type = 0;
            this._ball = new Ball(new Point(this._size[0] / 2, this._size[1] / 2), this._size[1] / 80, new double[2] { 0.0, 0.0 });

            int[] boardSize = new int[2] { Convert.ToInt32(this._size[0] / 38), this._size[1] / 10 };

            this._boards    = new Board[2];
            this._boards[0] = new Player(new Point(boardSize[0], (this._size[1] / 2) - boardSize[1] / 2), boardSize, boardSize[0] / 2, 0, 0.3);
            this._boards[1] = new Computer(new Point(this._size[0] - boardSize[0] - 1, (this._size[1] / 2) - boardSize[1] / 2), boardSize, boardSize[0] / 2, 0, 0.3, botType);

            this._graphic = new Graphic();
        }
        // load game
        public Field(Size mapSize, int botType, gameState gs)
        {
            this._size = new int[2];
            this._size[0] = mapSize.Width;
            this._size[1] = mapSize.Height;

            this._type = 0;
            this._ball = new Ball(new Point(this._size[0] / 2, this._size[1] / 2), gs.ballRadius, new double[2] { gs.ballSpeedX, gs.ballSpeedY });

            int[] boardSize = new int[2] { Convert.ToInt32(this._size[0] / 38), this._size[1] / 10 };

            this._boards    = new Board[2];
            this._boards[0] = new Player(new Point(boardSize[0], (this._size[1] / 2) - boardSize[1] / 2), boardSize, boardSize[0] / 2, 0, gs.playerBoardAcc);
            this._boards[1] = new Computer(new Point(this._size[0] - boardSize[0] - 1, (this._size[1] / 2) - boardSize[1] / 2), boardSize, boardSize[0] / 2, 0, gs.computerBoardAcc, botType);

            this._graphic = new Graphic();

            this._ball.setCoord(new Point(gs.ballCoordX, gs.ballCoordY));
            this._boards[0].setCoord(new Point(gs.playerBoardX, gs.playerBoardY));
            this._boards[1].setCoord(new Point(gs.computerBoardX, gs.computerBoardY));

            for (int i = 0; i < gs.computerGoals; i++)
            {
                this._boards[1].win();
                this._boards[0].lose();

            }
            for (int i = 0; i < gs.playerGoals; i++)
            {
                this._boards[0].win();

            }
        }
        public Field(PictureBox gameCanvas)
        {
            this._size    = new int[2];
            this._size[0] = 770;
            this._size[1] = 400;

            this._type = 0;
            this._ball = new Ball();

            this._boards    = new Board[2];
            this._boards[0] = new Player();
            this._boards[1] = new Computer();

            this._graphic = new Graphic(gameCanvas);
        }

        public Field(int[] size, int type, Wall[] walls, Ball ball, Board[] boards, Graphic graphic)
        {
            this._size    = size;
            this._type    = type;
            this._walls   = walls;
            this._ball    = ball;
            this._boards  = boards;
            this._graphic = graphic;
        }

        public void checkBallPlatformCollison(Board board, Ball ball)
        {

            double speedImp = 0.4 * (ball.getRadius() / 5);  // part of platform energy
            double hitSpeed = 0.3 * (ball.getRadius() / 5);  // + vx

            Point ballCoord    = ball.getFutureCoord();
            double[] ballSpeed = ball.getSpeed();
            Point boardCoord   = board.getCoord();
            int[] boardParams  = board.getParams();

            Point boardCoordBallUpCoord = boardCoord;
            boardCoordBallUpCoord.Y += board.getRadius();
            boardCoordBallUpCoord.Y = Convert.ToInt32(boardCoordBallUpCoord.Y + board.getSpeed());      // shadow moment

            Point boardCoordBallDownCoord = boardCoord;
            boardCoordBallDownCoord.Y += boardParams[1] - board.getRadius();
            boardCoordBallDownCoord.Y = Convert.ToInt32(boardCoordBallDownCoord.Y + board.getSpeed());  // shadow moment

            Point boardCoordCenterCoord = boardCoord;
            boardCoordCenterCoord.Y += boardParams[1] / 2;

            int dx     = Math.Abs(ballCoord.X - boardCoord.X);
            int dyUp   = ballCoord.Y - boardCoordBallUpCoord.Y;
            int dyDown = ballCoord.Y - boardCoordBallDownCoord.Y;

            double dE1 = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dyUp, 2));
            double dE2 = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dyDown, 2));
                                                                            // we use dE1 and dE2 for checking ball-edge distantion and hit 180 if it required

            int radSum = ball.getRadius() + board.getRadius();

            if (dx < radSum)
            {
                if (board.isView())  // main hit
                {
                    ball.revers(0);
                    if (boardCoord.X < 100)  // left board
                    {
                        ball.setSpeed(ballSpeed[0] + hitSpeed, ballSpeed[1] + board.getSpeed() * speedImp);
                        // ball.setCoord(new Point(boardCoord.X + radSum, ballCoord.Y));  // because i use futureCoord maybe this string useless
                    }
                    else                   // right board
                    {
                        ball.setSpeed(ballSpeed[0] - hitSpeed, ballSpeed[1] + board.getSpeed() * speedImp);
                        ball.setCoord(new Point(boardCoord.X - radSum, ballCoord.Y));
                    }
                    
                }
                else                 // edge hit
                {
                    if (dE1 < radSum)
                    {
                        ball.revers(0);
                        ball.revers(1);
                        // ball.setCoord(new Point(ballCoord.X, boardCoordBallUpCoord.Y - radSum)); // because i use futureCoord maybe this string useless
                        ball.setSpeed(ballSpeed[0], ballSpeed[1] + board.getSpeed() * speedImp);

                        if (ball.getCoord().Y - ball.getRadius() == 0 && ballSpeed[0] == 0) // Platform eat ball :)
                        {
                            if (boardCoord.X < 100)   // left board
                            {
                                ball.setCoord(new Point(ballCoord.X + radSum + 1, boardCoordBallUpCoord.Y - radSum));
                                ball.setSpeed(1, ballSpeed[1]);
                            }
                            else
                            {                  // right board
                                ball.setCoord(new Point(ballCoord.X - radSum - 1, boardCoordBallUpCoord.Y - radSum));
                                ball.setSpeed(-1, ballSpeed[1]);
                            }
                        }
                    }
                    else if (dE2 < radSum)
                    {
                        ball.revers(0);
                        ball.revers(1);
                        ball.setCoord(new Point(ballCoord.X, boardCoordBallDownCoord.Y + radSum));

                        ball.setSpeed(ballSpeed[0], ballSpeed[1] + board.getSpeed() * speedImp);

                        if (ball.getCoord().Y + ball.getRadius() >= this._size[1] - 1 && ballSpeed[0] == 0) // Platform eat ball :)
                        {
                            if (boardCoord.X < 100)  // left board
                            {
                                ball.setCoord(new Point(ballCoord.X + radSum + 1, boardCoordBallUpCoord.Y + radSum));
                                ball.setSpeed(1, ballSpeed[1]);
                            }
                            else
                            {                 // right board
                                ball.setCoord(new Point(ballCoord.X - radSum - 1, boardCoordBallUpCoord.Y + radSum));
                                ball.setSpeed(-1, ballSpeed[1]);
                            }
                        }
                    }
                }
            }

            this.dx = dx;
            this.de1 = dE1;
            this.de2 = dE2;
        }

        public void checkBallWallCollision(Ball ball, Wall[] walls)
        {
            Point ballCoord = ball.getFutureCoord();
            int ballRadius  = ball.getRadius();

            // Check field collision
            if (ballCoord.Y - ballRadius < 0)
            {
                Point newBallCoord = new Point(ballCoord.X, 0 + ballRadius);
                ball.setCoord(newBallCoord);
                ball.revers(1);
            }

            if (ballCoord.Y + ballRadius > this._size[1] - 1)
            {
                Point newBallCoord = new Point(ballCoord.X, this._size[1] - 1 - ballRadius);
                ball.setCoord(newBallCoord);
                ball.revers(1);
            }

            // Check walls collision
            if (walls != null)
            {
                foreach (Wall wall in walls)
                {
                    Point wallCoord = wall.getCoord();

                    int dx = Math.Abs(ballCoord.X - wallCoord.X);
                    int radSum = ballRadius + wall.getRadius();
                    int wallRadius = wall.getRadius();
                    int wallH = wall.getParams()[0];
                    int wallType = wall.getWallType();

                    if (wallCoord.Y < (ballCoord.Y + ballRadius) && wallCoord.Y + wallH > (ballCoord.Y - ballRadius))
                    {
                        int dy1 = ballCoord.Y - wallCoord.Y;
                        int dy2 = ballCoord.Y - (wallCoord.Y + wallH);

                        double dE1 = Math.Sqrt(Math.Pow(dy1, 2) + Math.Pow(dx, 2));
                        double dE2 = Math.Sqrt(Math.Pow(dy2, 2) + Math.Pow(dx, 2));

                        // Temp params for calculate edge-hits cases
                        int[] wallTempParams = new int[4];
                        wallTempParams[0] = wallCoord.X;
                        wallTempParams[1] = wallCoord.Y;
                        wallTempParams[2] = wallRadius;
                        wallTempParams[3] = wallH;

                        if (wallType == 1 || wallType == 3)
                        {
                            wallTempParams[3] -= wallRadius;
                            // MessageBox.Show($"{wallTempParams[3]} {wallTempParams[1] + wallTempParams[3]} {wallH} ");
                        }
                        if (wallType == 2 || wallType == 3)
                        {
                            wallTempParams[1] += wallRadius;
                            //  MessageBox.Show($"{wallTempParams[1]} {ballCoord.Y} {wallCoord.Y} ");
                        }

                        // If ball hit on main wall side (not rounded edges)
                        if (dx <= radSum && ballCoord.Y >= wallTempParams[1] && ballCoord.Y <= (wallTempParams[1] + wallTempParams[3]))
                        {
                            ball.revers(0);
                            Point newBallCoord;
                            if (ball.getCoord().X - wallCoord.X > 0)
                                newBallCoord = new Point(wallCoord.X + wallRadius + 1 + ballRadius, ballCoord.Y);  // magnet ball to wall 
                            else
                                newBallCoord = new Point(wallCoord.X - wallRadius - 1 - ballRadius, ballCoord.Y);  // 1 - pixel line of ball

                            ball.setCoord(newBallCoord);
                            antiStop(ball);
                            // MessageBox.Show("main");

                            continue;
                        }

                        if (dE1 <= radSum)
                        {
                            ball.revers(0);
                            ball.revers(1);
                            antiStop(ball);
                            // MessageBox.Show("dE1");

                            continue;
                        }

                        if (dE2 <= radSum)
                        {
                            ball.revers(0);
                            ball.revers(1);
                            antiStop(ball);
                            // MessageBox.Show("dE2");

                            continue;
                        }

                        // Ball can be blocked between two walls
                        void antiStop(Ball ball)
                        {
                            double[] tempSpeed = ball.getSpeed();
                            if (tempSpeed[0] > 0)
                                tempSpeed[0] += 0.2 * (ball.getRadius() / 5);
                            else
                                tempSpeed[0] -= 0.2 * (ball.getRadius() / 5);

                            if (tempSpeed[1] > 0)
                                tempSpeed[1] += 0.1 * (ball.getRadius() / 5);
                            else
                                tempSpeed[1] -= 0.1 * (ball.getRadius() / 5);
                            ball.setSpeed(tempSpeed[0], tempSpeed[1]);
                        }
                    }
                }
            }
        }

        public void checkPlatformEndCollision(Board board)
        {
            Point boardCoord  = board.getCoord();
            int[] boardParams = board.getParams();

            if (boardCoord.Y < 0)
            {
                Point newBoardCoord = new Point(boardCoord.X, 0);
                board.setCoord(newBoardCoord);
                board.jelly();
            } else if (boardCoord.Y + boardParams[1] > this._size[1] - 1)        // 0...n-1
            {
                Point newBoardCoord = new Point(boardCoord.X, this._size[1] - 1 - boardParams[1]);
                board.setCoord(newBoardCoord);
                board.jelly();
            }
        }

        public void resetAll()
        {
            this._ball.reset();
            this._ball.setRandomSpeed();
            this._boards[0].reset();
            this._boards[1].reset();
        }

        public PictureBox createGameCanvas()
        {
            PictureBox gameCanvas;

            Bitmap tempBmp = new Bitmap(this._size[0], this._size[1]);
            Graphics tempGraphic = Graphics.FromImage(tempBmp);
            tempGraphic.Clear(Color.White);

            gameCanvas = new PictureBox
            {
                Name     = "gameCanvas",
                Size     = new Size(this._size[0], this._size[1]),
                Location = new Point(12, 31), // rework?
                Image    = tempBmp,
            };

            this._graphic = new Graphic(gameCanvas);

            tempGraphic.Dispose();

            return gameCanvas;
        }

        public int[] getSize() { return this._size; }
        public int getType() { return this._type; }
        public void setType(int type) { 
            this._type = type; 

            if (this._type == 0)  // simple mode (move in normal)
            {
                // With out walls
                this._walls = null;
            }

            if (this._type == 1)  // normal mode
            {
                int[] wallCoord = new int[4];
                wallCoord[0] = this._size[0] / 2 - this._size[0] / 5;
                wallCoord[1] = 0;
                wallCoord[2] = this._size[0] / 2 + this._size[0] / 5;
                wallCoord[3] = this._size[1] / 2 + this._size[1] / 4;

                this._walls = new Wall[2];
                this._walls[0] = new Wall(new Point(wallCoord[0], wallCoord[1]), this._ball.getRadius(), this._size[1] / 4, 1);
                this._walls[1] = new Wall(new Point(wallCoord[2], wallCoord[3]), this._ball.getRadius(), this._size[1] / 4 - 1, 2);

            }

            if (this._type == 2)  // hard mode
            {
                int[] wallCoord = new int[8];
                wallCoord[0] = this._size[0] / 2;
                wallCoord[1] = 0;
                wallCoord[2] = this._size[0] / 2;
                wallCoord[3] = (this._size[1] / 2) - (this._size[1] / 6);
                wallCoord[4] = this._size[0] / 2;
                wallCoord[5] = (this._size[1] / 2) + (this._size[1] / 6) - (this._size[1] / 12);
                wallCoord[6] = this._size[0] / 2;
                wallCoord[7] = this._size[1] - this._size[1] / 6;

                this._walls = new Wall[4];
                this._walls[0] = new Wall(new Point(wallCoord[0], wallCoord[1]), this._ball.getRadius(), this._size[1] / 6, 1);
                this._walls[1] = new Wall(new Point(wallCoord[2], wallCoord[3]), this._ball.getRadius(), this._size[1] / 12, 3);
                this._walls[2] = new Wall(new Point(wallCoord[4], wallCoord[5]), this._ball.getRadius(), this._size[1] / 12, 3);
                this._walls[3] = new Wall(new Point(wallCoord[6], wallCoord[7]), this._ball.getRadius(), this._size[1] / 6 - 1, 2);

            }
        }

    }
}
