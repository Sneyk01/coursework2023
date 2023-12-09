using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static myPongGame.Classes.LeaderBoard;

namespace myPongGame.Classes
{
    internal class Game
    {
        private LeaderBoard _lBoard;
        private Form1 _formLink;
        private int _botSetting;
        private int _mapSetting;
        private int _mapSizeSetting;
        
        public Game()
        {
            _lBoard = new LeaderBoard();
            _formLink = null;

            this._botSetting = 0;
            this._mapSetting = 0;
            this._mapSizeSetting = 0;
        }

        public Game(Form1 mainForm)
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

            Match newMatch = new Match(this.getSetting(), worstScore);
            newMatch.startMatch(this._formLink, this.getSetting(), this._lBoard);
        }

        public int[] getSetting() { return new int[3] { this._botSetting, this._mapSetting, this._mapSizeSetting }; }
        public void setSetting(int[] setting, string host, string login, string password, string dbName)
        {
            this._botSetting     = setting[0];
            this._mapSetting     = setting[1];
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
                return(text);
            }
            catch (Exception e)
            {
                this._formLink.dbState = 0;
                return("Не удалось получить доступ к базе данных");
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
    }
}
