using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace myPongGame
{
    public partial class Settings : Form
    {
        public string login = "root";
        public string password = "";
        public string host = "localhost";
        public string dbName = "courseWorkPong";
        public Settings()
        {
            InitializeComponent();
        }

        public Settings(int bot, int map, int mapSize)
        {
            InitializeComponent();
            botSetting.SelectedIndex = bot;
            mapSetting.SelectedIndex = map;
            mapSizeSetting.SelectedIndex = mapSize;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dbButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                string fileText = System.IO.File.ReadAllText(filename);

                try
                {
                    using JsonDocument doc = JsonDocument.Parse(fileText);
                    JsonElement root = doc.RootElement;
                    var dbInfo = root[0];
                    this.login = dbInfo.GetProperty("login").ToString();
                    this.password = dbInfo.GetProperty("password").ToString();
                    this.host = dbInfo.GetProperty("host").ToString();
                    this.dbName = dbInfo.GetProperty("dbName").ToString();

                    if (login.Length > 20 || password.Length > 20 || host.Length > 30 || dbName.Length > 20)
                        throw new Exception();
                }
                catch
                {
                    MessageBox.Show("Ошибка формата");
                    login = "root";
                    password = "";
                    host = "localhost";
                    dbName = "courseWorkPong";
                }

            }
        }
    }
}
