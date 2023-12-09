using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static myPongGame.Classes.LeaderBoard;

namespace myPongGame.Classes
{
    internal class Match
    {
        private int             _lastLeaderScore;
        private Field           _field;
        private System.DateTime _startTime;
        private int             _gameStatus;
        private Thread          _gameThread; // good?
        private int             _mapType;// TODO: use settings 
        private Score           _score;
        private System.Threading.Timer timer;

        public Match() { 
            this._lastLeaderScore = 100; // TODO
            this._field           = new Field();
            this._startTime       = DateTime.Now;
            this._gameStatus      = 0;
            this._score           = new Score();
        }
        public Match(int[] gameParams, int lastLeaderScore) {
            Size gameSize = new Size(770, 400);
            switch (gameParams[2])
            {
                case 1:
                    gameSize = new Size(1240, 640);
                    break;
                case 2:
                    gameSize = new Size(1890, 960);
                    break;
            }

            this._lastLeaderScore = lastLeaderScore;
            this._field           = new Field(gameSize, gameParams[0]);
            this._startTime       = DateTime.Now;
            this._gameStatus      = 0;
            this._score           = new Score();
            this._mapType = gameParams[1];
            this._score.setModifiers(this._mapType, gameParams[0]);
            this._field.setType(this._mapType);
        }

        public void startMatch(Form1 formLink, int[] gameParams, LeaderBoard lBoard)
        {

            // Enable debug mode
            bool debugMode = false;

            // Create debug form
            debugForm debugForm = new debugForm();

            // Create game canvas
            PictureBox gameCanvas = this._field.createGameCanvas();
            formLink.Controls.Add(gameCanvas);

            async void calculateFrame(object obj)
            {
                // Check ball-platform position
                this._field._boards[0].findBall(this._field._ball.getCoord());
                this._field._boards[1].findBall(this._field._ball.getCoord());

                // Update platform position
                this._field._boards[0].update();
                this._field._boards[1].update();

                // Update ball position
                this._field._ball.update();

                // Check ball-platform collision
                this._field.checkBallPlatformCollison(this._field._boards[0], this._field._ball);
                this._field.checkBallPlatformCollison(this._field._boards[1], this._field._ball);

                // Check ball-wall collision
                this._field.checkBallWallCollision(this._field._ball, this._field._walls);

                // Check platform-field collision
                this._field.checkPlatformEndCollision(this._field._boards[0]);
                this._field.checkPlatformEndCollision(this._field._boards[1]);

                // Show new frame
                gameCanvas.Invoke(new Action(() =>
                {
                    _field._graphic.createFrame(this._field, this._field._ball, this._field._boards[0], this._field._boards[1], this._field._walls, DateTime.Now - this._startTime);
                }));

                if (debugMode)
                {
                    debugForm.Invoke(new Action(() =>
                    {
                        debugForm.p1Coord.Text = this._field._boards[1].getAcceleration().ToString();
                        debugForm.p1Speed.Text = this._field._boards[1].getAcceleration().ToString();
                        debugForm.p1IsView.Text = this._field._boards[1].getAcceleration().ToString();

                        debugForm.p2Coord.Text = this._field._boards[1].getCoord().ToString();
                        debugForm.p2Speed.Text = this._field._boards[1].getSpeed().ToString();
                        debugForm.p2IsView.Text = this._field._boards[1].isView().ToString();
                        // debugForm.timeDif.Text = time.Milliseconds.ToString();

                        debugForm.bCoord.Text = this._field._ball.getCoord().ToString();
                        debugForm.bSpeed.Text = this._field._ball.getSpeed()[0].ToString() + " ";
                        debugForm.bSpeed.Text += this._field._ball.getSpeed()[1].ToString();

                        debugForm.dx.Text = this._field.dx.ToString();
                        debugForm.de1.Text = this._field.de1.ToString();
                        debugForm.de2.Text = this._field.de2.ToString();
                    }));         // debug window
                }
            }

            // Copy old buttons
            Button startGame     = formLink.startGame;
            Button optionsButton = formLink.optionsButton;
            Button scoreButton = formLink.scoreButton;
            Button helpButton    = formLink.helpButton;
            Label pongLabel      = formLink.pongLabel;
            Label bestScore      = formLink.bestScore;

            // Delete buttons on screen
            formLink.Controls.Remove(formLink.startGame);
            formLink.Controls.Remove(formLink.optionsButton);
            formLink.Controls.Remove(formLink.scoreButton);
            formLink.Controls.Remove(formLink.helpButton);
            formLink.Controls.Remove(formLink.pongLabel);
            formLink.Controls.Remove(formLink.bestScore);
            
            // Create event handlers
            formLink.KeyDown += keyDown;
            formLink.KeyDown += pauseGame;
            formLink.KeyDown += stopGame;
            formLink.KeyUp   += keyUp;

            // Setup buttons handlers
            formLink.pauseToolStripMenuItem.Click += pauseGame;
            formLink.stopToolStripMenuItem.Click  += stopGame;

            if (debugMode)
            {
                debugForm.Show();
            }

            // Init game loop
            void gameLoop()
            {
                TimeSpan timeDif = TimeSpan.Zero;

                this._field.resetAll();

                TimerCallback tm = new TimerCallback(calculateFrame);
                timer = new System.Threading.Timer(tm, 0, 0, 16);

                while (this._gameStatus >= 0)
                {
                    // Check game status
                    timeDif = this.checkGame();
                    Thread.Sleep(16);

                    // GamePause loop
                    while (this._gameStatus == 1)
                    {
                        Thread.Sleep(10);

                        // Stop game timer
                        TimeSpan tempTimeDif = DateTime.Now - this._startTime;
                        this._startTime += tempTimeDif - timeDif;

                        formLink.Invoke(new Action(() =>
                        {
                            _field._graphic.createFrame(this._field, this._field._ball, this._field._boards[0], this._field._boards[1], this._field._walls, timeDif);
                        }));
                    }

                    // GameOver loop
                    while (this._gameStatus == 2)
                    {
                        Thread.Sleep(10);

                        // Stop game timer
                        TimeSpan tempTimeDif = DateTime.Now - this._startTime;
                        this._startTime += tempTimeDif - timeDif;

                        formLink.Invoke(new Action(() =>
                        {
                            _field._graphic.createFrame(this._field, this._field._ball, this._field._boards[0], this._field._boards[1], this._field._walls, timeDif);
                        }));
                    }
                }

                /*
                while (this._gameStatus >= 0)
                {
                    Thread.Sleep(1);                               // 60 FPS    6 ms TODO: revork timer

                    // Check ball-platform position
                    this._field._boards[0].findBall(this._field._ball.getCoord());
                    this._field._boards[1].findBall(this._field._ball.getCoord());

                    // Update platform position
                    this._field._boards[0].update();
                    this._field._boards[1].update();

                    // Update ball position
                    this._field._ball.update();

                    // Check ball-platform collision
                    this._field.checkBallPlatformCollison(this._field._boards[0], this._field._ball);
                    this._field.checkBallPlatformCollison(this._field._boards[1], this._field._ball);

                    // Check ball-wall collision
                    this._field.checkBallWallCollision(this._field._ball, this._field._walls);

                    // Check platform-field collision
                    this._field.checkPlatformEndCollision(this._field._boards[0]);
                    this._field.checkPlatformEndCollision(this._field._boards[1]);

                    // Check game status
                    timeDif = this.checkGame();

                    counter += 1;
                    
                    // Show new frame
                    formLink.Invoke(new Action(() =>
                    {
                        _field._graphic.createFrame(this._field, this._field._ball, this._field._boards[0], this._field._boards[1], this._field._walls, timeDif);
                    }));

                    
                    if (debugMode) 
                    { 
                        debugForm.Invoke(new Action(() =>
                        {
                            debugForm.p1Coord.Text = this._field._boards[0].getCoord().ToString();
                            debugForm.p1Speed.Text = this._field._boards[0].getSpeed().ToString();
                            debugForm.p1IsView.Text = this._field._boards[0].isView().ToString();

                            debugForm.p2Coord.Text = this._field._boards[1].getCoord().ToString();
                            debugForm.p2Speed.Text = this._field._boards[1].getSpeed().ToString();
                            debugForm.p2IsView.Text = this._field._boards[1].isView().ToString();
                            // debugForm.timeDif.Text = time.Milliseconds.ToString();

                            debugForm.bCoord.Text = this._field._ball.getCoord().ToString();
                            debugForm.bSpeed.Text = this._field._ball.getSpeed()[0].ToString() + " ";
                            debugForm.bSpeed.Text += this._field._ball.getSpeed()[1].ToString();

                            debugForm.dx.Text = this._field.dx.ToString();
                            debugForm.de1.Text = this._field.de1.ToString();
                            debugForm.de2.Text = this._field.de2.ToString();

                            TimeSpan fps = DateTime.Now - fpsTime;

                            if (fps.Seconds >= 1) // timeDif don't change
                            {
                                fpsTime = DateTime.Now;
                                debugForm.p1Button.Text = counter.ToString();
                                counter = 0;
                            }
                        }));         // debug window
                    }

                    // GamePause loop
                    while (this._gameStatus == 1)
                    {
                        Thread.Sleep(10);

                        TimeSpan tempTimeDif = DateTime.Now - this._startTime;
                        this._startTime += tempTimeDif - timeDif;

                        formLink.Invoke(new Action(() =>
                        {
                            _field._graphic.createFrame(this._field, this._field._ball, this._field._boards[0], this._field._boards[1], this._field._walls, timeDif);
                        }));

                    }
                    

                    // GameOver loop
                    while (this._gameStatus == 2)
                    {
                        Thread.Sleep(10);

                        TimeSpan tempTimeDif = DateTime.Now - this._startTime;
                        this._startTime += tempTimeDif - timeDif;

                        formLink.Invoke(new Action(() =>
                        {
                            _field._graphic.createFrame(this._field, this._field._ball, this._field._boards[0], this._field._boards[1], this._field._walls, timeDif);
                        }));
                    }
                }
                */

                // Actions before match loop die
                formLink.Invoke(new Action(() =>
                {
                    // Delete game canvas
                    formLink.Controls.Remove(gameCanvas);
                    gameCanvas.Dispose();

                    // Return and activate buttons
                    formLink.Controls.Add(startGame);
                    formLink.Controls.Add(optionsButton);
                    formLink.Controls.Add(scoreButton);
                    formLink.Controls.Add(helpButton);
                    formLink.Controls.Add(pongLabel);
                    formLink.Controls.Add(bestScore);

                    // Update bestScore label
                    if (formLink.dbState == 0)
                    {
                        formLink.bestScore.Text = "Лучший счет: Ошибка подключения";
                    }
                    else
                    {
                        try
                        {
                            PlayerData bestPlayer;
                            bestPlayer = lBoard.doAction("SELECT * FROM `playerdata` ORDER BY Score DESC LIMIT 1");
                            formLink.bestScore.Text = $"Лучший счет: {bestPlayer.Score.ToString()}";
                        }
                        catch (Exception e)
                        {
                            formLink.dbState = 0;
                            MessageBox.Show("Ошибка подключения к базе данных");
                        }
                    }

                    formLink.mode = 0;
                }));

                // Delete event handlers
                formLink.KeyDown -= keyDown;
                formLink.KeyDown -= pauseGame;
                formLink.KeyDown -= stopGame;
                formLink.KeyUp   -= keyUp;

                // Delete buttons handlers
                formLink.pauseToolStripMenuItem.Click -= pauseGame;
                formLink.stopToolStripMenuItem.Click  -= stopGame;

                int playerLives = this._field._boards[0].getBoardScoreStatus()[1];
                this._score.calcScore(this._field._boards[0].getBoardScoreStatus()[0], playerLives, timeDif);

                if (formLink.dbState != 0)
                    MessageBox.Show($"Ваш счет: {this._score.getScore()}");
                else
                    MessageBox.Show($"Ваш счет: {this._score.getScore()}\r\nНет подключения к базе данных!");
                String playerName;
                int playerScore = this._score.getScore();
                int dbStatus = lBoard.isFreeLBoardSpace(); // 0 - no free space; 1 - free space; -1 - not connected
                if (dbStatus != -1)
                {
                    formLink.dbState = 1;

                    if (playerScore > this._lastLeaderScore || dbStatus == 1)
                    {
                        Form2 nameForm = new Form2();

                        if (nameForm.ShowDialog() == DialogResult.OK)
                        {
                            playerName = nameForm._userName;
                        }
                        else
                        {
                            playerName = "UNKNOWN";
                        }

                        try
                        {
                            lBoard.addNewPlayer(playerName, playerScore);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Ошибка подключение к базе данных");
                        }

                        nameForm.Dispose();
                    }
                }
            }

            _gameThread = new Thread(gameLoop);

            // Start game thread
            _gameThread.Start();
        }

        public TimeSpan checkGame() 
        {
            Point ballCoord = this._field._ball.getCoord();
            int ballRad     = this._field._ball.getRadius();

            // Calc game time
            TimeSpan timeDif = DateTime.Now - this._startTime;

            if (ballCoord.X - ballRad  <= 0)
            {
                this._field._boards[0].lose();
                this._field._boards[1].win();
                this._field.resetAll();
            }

            if (ballCoord.X + ballRad  >= this._field.getSize()[0] - 1)
            {
                this._field._boards[0].win();
                this._field._boards[1].lose();
                this._field.resetAll();
            }

            // placeholder for future
            if ("PvsC" == "PvsC")
            {
                int playerLives = this._field._boards[0].getBoardScoreStatus()[1];

                if (playerLives <= 0) // TODO
                {
                    this._gameStatus = 2;     // gameOver wait key
                    this._field.setType(-2);  // in game, gameover (bad practice, but use in graphic)
                    timer.Dispose();          // Delete timer
                }
            }

            return timeDif;
        }
        public void pauseGame(object sender, KeyEventArgs e)
        {
            if (this._field.getType() == this._mapType || this._field.getType() == -1) // if pong or pause mode bad idea
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (this._gameStatus == 1)
                    {
                        timer.Change(0, 16);                 // start timer
                        this._gameStatus = 0;                // in game
                        this._field.setType(this._mapType);  // in game mode 1 // REWORK!!!
                    }
                    else
                    {
                        timer.Change(System.Threading.Timeout.Infinite, 0);  // stop timer
                        this._gameStatus = 1;    // pause game
                        this._field.setType(-1); // in game pause
                    }
                }
            }
        }        
        public void pauseGame(object sender, EventArgs e)
        {
            if (this._gameStatus == 1)
            {
                timer.Change(0, 16);                 // start timer
                this._gameStatus = 0;                // in game
                this._field.setType(this._mapType);  // in game mode 1 // REWORK!!!
            }
            else
            {
                timer.Change(System.Threading.Timeout.Infinite, 0);  // stop timer
                this._gameStatus = 1;    // pause game
                this._field.setType(-1); // in game pause
            }
        }

        public void stopGame(object sender, KeyEventArgs e) {
            if (this._field.getType() == -1)
            {
                if (e.KeyCode == Keys.Q)
                {
                    this._gameStatus = -1;
                }
            }

            // If any key is pressed
            if (this._gameStatus == 2)
            {
                this._gameStatus = -1;
            }
        }

        public void stopGame(object sender, EventArgs e)
        {
            this._gameStatus = -1;
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.W || e.KeyCode == Keys.A)
            {
                this._field._boards[0].changeSpeed(e.KeyCode, 1);
            }
        }
        private void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.W || e.KeyCode == Keys.A)
            {
                this._field._boards[0].changeSpeed(e.KeyCode, 0);
            }
        }

    }
}
