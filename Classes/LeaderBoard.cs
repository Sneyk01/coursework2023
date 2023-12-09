using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static myPongGame.Classes.LeaderBoard;

namespace myPongGame.Classes
{
    internal class LeaderBoard
    {
        private String _host;
        private String _login;
        private String _password;
        private String _dbName;
        public LeaderBoard() 
        {
            this._host = "localhost";
            this._login = "root";
            this._password = "";
            this._dbName = "courseWorkPong";
        }

        public LeaderBoard(string host, string login, string password, string dbName) 
        {
            this._host = host;
            this._login = login;
            this._password = password;
            this._dbName = dbName;
        }

        public class PlayerData
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Score { get; set; }
        }

        public class ApplicationContext : DbContext
        {
            private String host;
            private String login;
            private String password;
            private String dbName;
            public DbSet<PlayerData> playerData { get; set; } = null!;

            public ApplicationContext()
            {
                Database.EnsureCreated();
            }
            public ApplicationContext(String host, String login, String password, String dbName)
            {
                this.host = host;
                this.login = login;
                this.password = password;
                this.dbName = dbName;

                Database.EnsureCreated();
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseMySql($"server={host};user={login};password={password};database={dbName};",
                    new MySqlServerVersion(new Version(5, 7, 36)));
            }
        }

        public string getLeaderBoard()
        {
            using (ApplicationContext db = new ApplicationContext(_host, _login, _password, _dbName))
            {
                var players = db.playerData.OrderByDescending(p => p.Score).ToList();
                String leaderBoards = "";

                foreach (PlayerData p in players)
                {
                    leaderBoards += $"{p.Name} - {p.Score} \r\n";
                }

                return leaderBoards;
            }
        }

        public PlayerData doAction(string sqlReq)
        {
            using (ApplicationContext db = new ApplicationContext(_host, _login, _password, _dbName))
            {
                var tempData = db.playerData.FromSqlRaw(sqlReq).ToList();
                return tempData[0];
            }
        }

        public void addNewPlayer(string playerName, int playerScore)
        {
            using (ApplicationContext db = new ApplicationContext(_host, _login, _password, _dbName))
            {
                var tempData = db.playerData.OrderBy(p => p.Score).ToList();
                if (tempData.Count < 10)
                {
                    db.Database.ExecuteSqlRaw($"INSERT INTO `playerdata` (`id`, `Name`, `Score`) VALUES(NULL, '{playerName}', '{playerScore}');");
                } else
                {
                    tempData = db.playerData.FromSqlRaw("SELECT* FROM `playerdata` ORDER BY Score ASC LIMIT 1").ToList();
                    db.Database.ExecuteSqlRaw($"UPDATE `playerdata` SET `Name` = '{playerName}', `Score` = '{playerScore}' WHERE `playerdata`.`id` = {tempData[0].Id};");
                }
            }
        }

        public int isFreeLBoardSpace()
        {
            // Other cases try-catch checking in game and match
            try
            {
                using (ApplicationContext db = new ApplicationContext(_host, _login, _password, _dbName))
                {
                    var tempData = db.playerData.OrderBy(p => p.Score).ToList();
                    if (tempData.Count < 10)
                    {
                        return 1;
                    }

                    return 0;
                }
            } catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
