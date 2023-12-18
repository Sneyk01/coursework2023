using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using myPongGame.Classes;

namespace myPongGame
{
    public partial class loadForm : Form
    {
        public int output;
        public loadForm()
        {
            InitializeComponent();

            string json;

            using (StreamReader r = new StreamReader(@"C:\Users\Public\Documents\pong\save.json"))
            {
                json = r.ReadToEnd();
            }

            List<gameState> saveData = JsonConvert.DeserializeObject<List<gameState>>(json);

            for (int i = 0; i < saveData.Count; ++i)
                loadBox.Items.Add($"Сохранение {i}");

            loadBox.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            output = loadBox.SelectedIndex;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
