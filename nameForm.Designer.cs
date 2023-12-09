namespace myPongGame
{
    partial class nameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            enterButton = new Button();
            nameBox = new TextBox();
            label1 = new Label();
            errorLabel = new Label();
            SuspendLayout();
            // 
            // enterButton
            // 
            enterButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            enterButton.Location = new Point(73, 97);
            enterButton.Name = "enterButton";
            enterButton.Size = new Size(201, 61);
            enterButton.TabIndex = 4;
            enterButton.Text = "Ввод";
            enterButton.UseVisualStyleBackColor = true;
            enterButton.Click += enterButton_Click;
            // 
            // nameBox
            // 
            nameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nameBox.Location = new Point(73, 32);
            nameBox.MaxLength = 20;
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(201, 27);
            nameBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Location = new Point(-1, 9);
            label1.Name = "label1";
            label1.Size = new Size(350, 20);
            label1.TabIndex = 6;
            label1.Text = "Введите имя:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // errorLabel
            // 
            errorLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            errorLabel.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            errorLabel.ForeColor = Color.Red;
            errorLabel.Location = new Point(-1, 68);
            errorLabel.Name = "errorLabel";
            errorLabel.Size = new Size(350, 19);
            errorLabel.TabIndex = 7;
            errorLabel.Text = "Длина имени должна быть не меньше 3х символов";
            errorLabel.TextAlign = ContentAlignment.MiddleCenter;
            errorLabel.Visible = false;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(349, 170);
            Controls.Add(errorLabel);
            Controls.Add(label1);
            Controls.Add(nameBox);
            Controls.Add(enterButton);
            MinimumSize = new Size(367, 217);
            Name = "Form2";
            Text = "Новый рекорд";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Button enterButton;
        private Label label1;
        private Label errorLabel;
        private TextBox nameBox;
    }
}