using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static myPongGame.Classes.LeaderBoard;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Xml.Linq;
using Newtonsoft.Json;
using Json.Net;

namespace myPongGame.Classes
{
    internal class Game
    {
        private LeaderBoard _lBoard;
        private mainForm _formLink;
        private int _botSetting;
        private int _mapSetting;
        private int _mapSizeSetting;
        private Match _match;

        public Game()
        {
            _lBoard = new LeaderBoard();
            _formLink = null;

            this._botSetting = 0;
            this._mapSetting = 0;
            this._mapSizeSetting = 0;
        }

        public Game(mainForm mainForm)
        {
            this._lBoard = new LeaderBoard();
            this._formLink = mainForm;

            this._botSetting = 0;
            this._mapSetting = 0;
            this._mapSizeSetting = 0;
        }

        public void startGame()
        {
            this._formLink.menuStrip1.TabStop = false;
            int worstScore;
            if (this._formLink.dbState == 1)
            {
                worstScore = this.getWorstScore();
            }
            else
            {
                worstScore = 999999;
            }

            this._match = new Match(this.getSetting(), worstScore);
            this._match.startMatch(this._formLink, this.getSetting(), this._lBoard);
        }

        // load game
        public void startGame(gameState gs)
        {
            this._formLink.menuStrip1.TabStop = false;
            int worstScore;
            if (this._formLink.dbState == 1)
            {
                worstScore = this.getWorstScore();
            }
            else
            {
                worstScore = 999999;
            }
            MessageBox.Show("Загружаем игру");

            var oldS = new int[3] { gs.botSetting, gs.mapSetting, gs.mapSizeSetting };
            this._match = new Match(oldS, worstScore, gs);

            this._match.startMatch(this._formLink, this.getSetting(), this._lBoard);
        }

        public int[] getSetting() { return new int[3] { this._botSetting, this._mapSetting, this._mapSizeSetting }; }
        public void setSetting(int[] setting, string host, string login, string password, string dbName)
        {
            this._botSetting = setting[0];
            this._mapSetting = setting[1];
            this._mapSizeSetting = setting[2];

            this._lBoard = new LeaderBoard(host, login, password, dbName);
        }

        public string showBestPlayers()
        {
            try
            {
                String text = "Таблица рейтинга: \r\n";
                text += this._lBoard.getLeaderBoard();
                this._formLink.dbState = 1;
                return (text);
            }
            catch (Exception e)
            {
                this._formLink.dbState = 0;
                return ("Не удалось получить доступ к базе данных");
            }
        }

        public String getBestScore()
        {
            try
            {
                PlayerData bestPlayer;
                bestPlayer = this._lBoard.doAction("SELECT * FROM `playerdata` ORDER BY Score DESC LIMIT 1");
                this._formLink.dbState = 1;
                return bestPlayer.Score.ToString();
            }
            catch (Exception e)
            {
                this._formLink.dbState = 0;
                return "Ошибка подключения";
            }

        }
        public int getWorstScore()
        {
            try
            {
                PlayerData bestPlayer;
                bestPlayer = this._lBoard.doAction("SELECT * FROM `playerdata` ORDER BY Score ASC LIMIT 1");
                this._formLink.dbState = 1;
                return bestPlayer.Score;
            }
            catch (Exception e)
            {
                this._formLink.dbState = 0;
                return 0;
            }

        }

        public void saveState()
        {
            // List<gameState> saveData = new List<gameState>();
            string json;

            using (StreamReader r = new StreamReader(@"C:\Users\Public\Documents\pong\save.json"))
            {
                 json = r.ReadToEnd();
            }

            List<gameState> saveData = JsonConvert.DeserializeObject<List<gameState>>(json);

            saveData.Add(new gameState()
            {
                id = saveData[saveData.Count - 1].id + 1,
                botSetting = this._botSetting,
                mapSetting = this._mapSetting,
                mapSizeSetting = this._mapSizeSetting,
                ballCoordX = this._match._field._ball.getCoord().X,
                ballCoordY = this._match._field._ball.getCoord().Y,
                ballSpeedX = this._match._field._ball.getSpeed()[0],
                ballSpeedY = this._match._field._ball.getSpeed()[1],
                ballRadius = this._match._field._ball.getRadius(),
                playerBoardX = this._match._field._boards[0].getCoord().X,
                playerBoardY = this._match._field._boards[0].getCoord().Y,
                computerBoardX = this._match._field._boards[1].getCoord().X,
                computerBoardY = this._match._field._boards[1].getCoord().Y,
                playerBoardAcc = this._match._field._boards[0].getAcceleration(),
                computerBoardAcc = this._match._field._boards[1].getAcceleration(),
                playerGoals = this._match._field._boards[0].getBoardScoreStatus()[0],
                computerGoals = this._match._field._boards[1].getBoardScoreStatus()[0],
                gameTime = this._match.timeDif,

            });

            json = JsonConvert.SerializeObject(saveData.ToArray());
            // MessageBox.Show(json);
            File.WriteAllText(@"C:\Users\Public\Documents\pong\save.json", json);

        }

    }
}
