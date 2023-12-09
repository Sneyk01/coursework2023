using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPongGame
{
    public partial class nameForm : Form
    {
        public String _userName;
        public nameForm()
        {
            InitializeComponent();
            this._userName = "";
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            String userName = nameBox.Text;
            if (userName.Length < 3)
            {
                errorLabel.Visible = true;
            }
            else
            {
                this._userName = nameBox.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
