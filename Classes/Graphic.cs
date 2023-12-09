using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace myPongGame.Classes
{
    internal class Graphic
    {
        protected PictureBox _canvas;
        private Pen          _my_pen;
        private Brush        _my_brush;
        // Bitmap bitmap = new Bitmap(1890, 960);
        public Graphic() {
            this._my_pen = new Pen(Color.Black);
            this._my_brush = new SolidBrush(Color.Black);
        }
        public Graphic(PictureBox canvas) {
            this._my_pen = new Pen(Color.Black);
            this._my_brush = new SolidBrush(Color.Black);
            this._canvas = canvas; 
        }

        public void createFrame(Field field, Ball ball, Board player1, Board player2, Wall[] walls, TimeSpan timeDif)
        {
            // Placeholder for future (player vs computer)
            var mode = "PvsC";
            var bitmap = this._canvas.Image;
            Graphics g = Graphics.FromImage(bitmap);

            // for text centering
            StringFormat midScreen = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // get some params for coords and params of objects
            int[] fieldCoord    = field.getSize();
            Point ballCoord     = ball.getCoord();
            Point player1Coord  = player1.getCoord();
            int[] player1Params = player1.getParams();
            Point player2Coord  = player2.getCoord();
            int[] player2Params = player2.getParams();

            // Calc center of field
            Point fieldCenter = new Point(fieldCoord[0] / 2, fieldCoord[1] / 2);

            // Calc platform1-edges platform coord
            Point player1BallUpCoord = player1Coord;
            player1BallUpCoord.Y += player1.getRadius();
            
            Point player1BallDownCoord = player1Coord;
            player1BallDownCoord.Y -= player1.getRadius() - player1Params[1];

            // Calc ball center coord
            Point player1CenterCoord = player1Coord;
            player1CenterCoord.Y += player1Params[1] / 2;

            // Calc platform2-edges platform coord
            Point player2BallUpCoord = player2Coord;
            player2BallUpCoord.Y += player2.getRadius();

            Point player2BallDownCoord = player2Coord;
            player2BallDownCoord.Y -= player2.getRadius() - player2Params[1];

            // Clear workspace
            g.Clear(Color.White);

            // Draw debug imgs
            // this._my_pen.Color = Color.Red;
            // g.DrawRectangle(this._my_pen, player1Coord.X - player1Params[0] / 2, player1Coord.Y + Convert.ToInt32(player1.getSpeed()), player1Params[0], player1Params[1]);            // shadow
            // g.DrawLine(this._my_pen, player1BallUpCoord.X, Convert.ToInt32(player1BallUpCoord.Y + player1.getSpeed()), ballCoord.X, ballCoord.Y);                                      // lines    
            // g.DrawLine(this._my_pen, player1BallDownCoord.X, Convert.ToInt32(player1BallDownCoord.Y + player1.getSpeed()), ballCoord.X, ballCoord.Y);
            // g.DrawLine(this._my_pen, player1CenterCoord, ballCoord);
            // g.DrawLine(this._my_pen, ball.getCoord().X - 100, ball.getCoord().Y, ball.getCoord().X + 100, ball.getCoord().Y);
            g.DrawLine(this._my_pen, fieldCenter.X - 1, fieldCenter.Y, fieldCenter.X + 1, fieldCenter.Y);
            g.DrawLine(this._my_pen, fieldCenter.X, fieldCenter.Y - 1, fieldCenter.X, fieldCenter.Y + 1);
            // this._my_pen.Color = Color.Black;

            // Draw ball
            // this._my_brush = new SolidBrush(Color.FromArgb(255, 228, 236, 252));
            this._my_brush = new SolidBrush(Color.White);
            g.DrawEllipse(this._my_pen, getBallRectangle(ball.getCoord(), ball.getRadius()));
            g.FillEllipse(Brushes.Orange, getBallRectangle(ball.getCoord(), ball.getRadius()));

            // Draw p1 board
            this._my_brush = new SolidBrush(Color.FromArgb(255, 59, 77, 58));
            g.DrawRectangle(this._my_pen, player1Coord.X - player1Params[0] / 2, player1Coord.Y + player1.getRadius(), player1Params[0], player1Params[1] - 2 * player1.getRadius());  // draw first board
            g.DrawEllipse(this._my_pen, getBallRectangle(player1BallUpCoord, player1.getRadius()));                                                                                    // draw edges
            g.DrawEllipse(this._my_pen, getBallRectangle(player1BallDownCoord, player1.getRadius()));
            g.FillRectangle(Brushes.Blue, player1Coord.X - player1Params[0] / 2, player1Coord.Y + player1.getRadius(), player1Params[0], player1Params[1] - 2 * player1.getRadius());     // filled rect little less, because i want use circles)

            // Draw p2 board
            // this._my_brush = new SolidBrush(Color.FromArgb(255, 180, 148, 99));
            g.DrawRectangle(this._my_pen, player2Coord.X - player2Params[0] / 2, player2Coord.Y + player2.getRadius(), player2Params[0], player2Params[1] - 2 * player2.getRadius());  // draw second board
            g.DrawEllipse(this._my_pen, getBallRectangle(player2BallUpCoord, player2.getRadius()));                                                                                    // draw edges
            g.DrawEllipse(this._my_pen, getBallRectangle(player2BallDownCoord, player2.getRadius()));
            g.FillRectangle(Brushes.Red, player2Coord.X - player2Params[0] / 2, player2Coord.Y + player2.getRadius(), player2Params[0], player2Params[1] - 2 * player2.getRadius());     // filled rect little less, because i want use circles)


            // Draw walls
            if (walls != null)
            {
                this._my_pen.Color = Color.Gray;

                foreach (Wall wall in walls)
                {
                    int wallH = wall.getParams()[0];
                    int wallType = wall.getWallType();
                    int wallRadius = wall.getRadius();
                    Point wallCoord = wall.getCoord();
                    Point wallEndCoord = wallCoord;

                    wallEndCoord.Y += wall.getParams()[0];

                    // Draw circle if possible
                    if (wallType == 1 || wallType == 3)
                    {
                        wallH -= wallRadius;        // wall main side - circle 
                        wallEndCoord.Y -= wall.getRadius();
                        g.DrawEllipse(this._my_pen, getBallRectangle(wallEndCoord, wallRadius));
                    }

                    // Draw circle if possible
                    if (wallType == 2 || wallType == 3)
                    {
                        wallCoord.Y += wallRadius;  // wall main side - circle 
                        g.DrawEllipse(this._my_pen, getBallRectangle(wallCoord, wallRadius));

                        // We move start poin bottom and lost - wallRadius
                        if (wallType == 3)
                        {
                            wallH -= wallRadius;
                        }
                    }

                    this._my_brush = new SolidBrush(Color.White);
                    g.DrawRectangle(this._my_pen, wallCoord.X - wallRadius, wallCoord.Y, wallRadius * 2, wallH);
                    g.FillRectangle(Brushes.LightGray, wallCoord.X - wallRadius + 1, wallCoord.Y - 1, wallRadius * 2 - 1, wallH + 2);
                }
                this._my_pen.Color = Color.Black;
                this._my_pen.Width = 1;
            }

            // Draw score
            if (mode == "PvsC")
            {
                int[] player1Status = player1.getBoardScoreStatus();

                this._my_brush = new SolidBrush(Color.FromArgb(180, 100, 255, 100));
                g.DrawString("Мячи: "  + player1Status[0].ToString(), new Font("Arial", 18), Brushes.Green, fieldCenter.X / 4, 30, midScreen);
                g.DrawString("Жизни: " + player1Status[1].ToString(), new Font("Arial", 18), Brushes.Green, fieldCenter.X + (fieldCenter.X* 3 / 4), 30, midScreen);

                g.DrawString("Время:", new Font("Arial", 9), Brushes.Green, fieldCenter.X, 7, midScreen);
                g.DrawString($"{timeDif:mm\\:ss}", new Font("Arial", 18), Brushes.Green, fieldCenter.X, 30, midScreen);
            }

            // Draw pause
            if (field.getType() == -1)
            {
                g.DrawString("ПАУЗА", new Font("Arial", 36), Brushes.Green, fieldCenter, midScreen);
                g.DrawString("Нажмите ecs чтобы продолжить", new Font("Arial", 9), Brushes.Green, fieldCenter.X, fieldCenter.Y + 40, midScreen);
                g.DrawString("Нажмите q для выхода в меню", new Font("Arial", 9), Brushes.Green, fieldCenter.X, fieldCenter.Y + 40 + 20, midScreen);

            }
            
            // Draw gameover
            if (field.getType() == -2)
            {
                g.DrawString("ИГРА ОКОНЧЕНА", new Font("Arial", 40), Brushes.Red, fieldCenter, midScreen);
                g.DrawString("Нажмите любую клавишу чтобы выйти", new Font("Arial", 9), Brushes.Red, fieldCenter.X, fieldCenter.Y + 50, midScreen);
            }

            g.Dispose();
            this._canvas.Image = bitmap;

            //return bitmap;
        }

        private Rectangle getBallRectangle(Point ballCoord, int radius)
        {
            int x1 = ballCoord.X - radius;
            int y1 = ballCoord.Y - radius;
            int sizeX = 2 * radius;
            int sizeY = 2 * radius;

            return new Rectangle(x1, y1, sizeX, sizeY);
        }

        private Rectangle getBoardRectangle(Point boardCenterCoord, int[] boardParams)
        {
            int x1 = boardCenterCoord.X - boardParams[0] / 2;
            int y1 = boardCenterCoord.Y;
            int sizeX = boardParams[0];
            int sizeY = boardParams[1];

            return new Rectangle(x1, y1, sizeX, sizeY);
        }
    }
}
