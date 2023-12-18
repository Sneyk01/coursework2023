namespace myPongGame
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            gameToolStripMenuItem = new ToolStripMenuItem();
            pauseToolStripMenuItem = new ToolStripMenuItem();
            stopToolStripMenuItem = new ToolStripMenuItem();
            сохранитьToolStripMenuItem = new ToolStripMenuItem();
            загрузитьToolStripMenuItem = new ToolStripMenuItem();
            settingToolStripMenuItem = new ToolStripMenuItem();
            рейтингToolStripMenuItem = new ToolStripMenuItem();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            сохранитьБазуДанныхToolStripMenuItem = new ToolStripMenuItem();
            startGame = new Button();
            optionsButton = new Button();
            pongLabel = new Label();
            bestScore = new Label();
            helpButton = new Button();
            scoreButton = new Button();
            saveFileDialog1 = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { gameToolStripMenuItem, settingToolStripMenuItem, рейтингToolStripMenuItem, справкаToolStripMenuItem, сохранитьБазуДанныхToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 5;
            menuStrip1.TabStop = true;
            menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            gameToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pauseToolStripMenuItem, stopToolStripMenuItem, сохранитьToolStripMenuItem, загрузитьToolStripMenuItem });
            gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            gameToolStripMenuItem.Size = new Size(57, 24);
            gameToolStripMenuItem.Text = "Игра";
            // 
            // pauseToolStripMenuItem
            // 
            pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            pauseToolStripMenuItem.Size = new Size(224, 26);
            pauseToolStripMenuItem.Text = "Пауза";
            // 
            // stopToolStripMenuItem
            // 
            stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            stopToolStripMenuItem.Size = new Size(224, 26);
            stopToolStripMenuItem.Text = "В главное меню";
            // 
            // сохранитьToolStripMenuItem
            // 
            сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            сохранитьToolStripMenuItem.Size = new Size(224, 26);
            сохранитьToolStripMenuItem.Text = "Сохранить";
            сохранитьToolStripMenuItem.Click += сохранитьToolStripMenuItem_Click;
            // 
            // загрузитьToolStripMenuItem
            // 
            загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            загрузитьToolStripMenuItem.Size = new Size(224, 26);
            загрузитьToolStripMenuItem.Text = "Загрузить";
            загрузитьToolStripMenuItem.Click += загрузитьToolStripMenuItem_Click;
            // 
            // settingToolStripMenuItem
            // 
            settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            settingToolStripMenuItem.Size = new Size(98, 24);
            settingToolStripMenuItem.Text = "Настройки";
            settingToolStripMenuItem.Click += settingToolStripMenuItem_Click;
            // 
            // рейтингToolStripMenuItem
            // 
            рейтингToolStripMenuItem.Name = "рейтингToolStripMenuItem";
            рейтингToolStripMenuItem.Size = new Size(78, 24);
            рейтингToolStripMenuItem.Text = "Рейтинг";
            рейтингToolStripMenuItem.Click += рейтингToolStripMenuItem_Click;
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(81, 24);
            справкаToolStripMenuItem.Text = "Справка";
            справкаToolStripMenuItem.Click += справкаToolStripMenuItem_Click;
            // 
            // сохранитьБазуДанныхToolStripMenuItem
            // 
            сохранитьБазуДанныхToolStripMenuItem.Name = "сохранитьБазуДанныхToolStripMenuItem";
            сохранитьБазуДанныхToolStripMenuItem.Size = new Size(157, 24);
            сохранитьБазуДанныхToolStripMenuItem.Text = "Сохранить рейтинг";
            сохранитьБазуДанныхToolStripMenuItem.Click += сохранитьБазуДанныхToolStripMenuItem_Click;
            // 
            // startGame
            // 
            startGame.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            startGame.Location = new Point(300, 109);
            startGame.Name = "startGame";
            startGame.Size = new Size(201, 61);
            startGame.TabIndex = 1;
            startGame.Text = "Начать";
            startGame.UseVisualStyleBackColor = true;
            startGame.Click += startGame_Click;
            // 
            // optionsButton
            // 
            optionsButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            optionsButton.Location = new Point(300, 184);
            optionsButton.Name = "optionsButton";
            optionsButton.Size = new Size(201, 61);
            optionsButton.TabIndex = 2;
            optionsButton.Text = "Настройки";
            optionsButton.UseVisualStyleBackColor = true;
            optionsButton.Click += optionsButton_Click;
            // 
            // pongLabel
            // 
            pongLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pongLabel.Font = new Font("Forte", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            pongLabel.Location = new Point(300, 40);
            pongLabel.Name = "pongLabel";
            pongLabel.Size = new Size(201, 66);
            pongLabel.TabIndex = 6;
            pongLabel.Text = "ПОНГ";
            pongLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // bestScore
            // 
            bestScore.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bestScore.Font = new Font("Segoe UI Black", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            bestScore.Location = new Point(0, 409);
            bestScore.Name = "bestScore";
            bestScore.Size = new Size(800, 32);
            bestScore.TabIndex = 7;
            bestScore.Text = "Лучший счет: подключение к базе данных";
            bestScore.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // helpButton
            // 
            helpButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            helpButton.Location = new Point(300, 334);
            helpButton.Name = "helpButton";
            helpButton.Size = new Size(201, 61);
            helpButton.TabIndex = 4;
            helpButton.Text = "Справка";
            helpButton.UseVisualStyleBackColor = true;
            helpButton.Click += helpButton_Click;
            // 
            // scoreButton
            // 
            scoreButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            scoreButton.Location = new Point(300, 259);
            scoreButton.Name = "scoreButton";
            scoreButton.Size = new Size(201, 61);
            scoreButton.TabIndex = 3;
            scoreButton.Text = "Рейтинг";
            scoreButton.UseVisualStyleBackColor = true;
            scoreButton.Click += scoreButton_Click;
            // 
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(scoreButton);
            Controls.Add(helpButton);
            Controls.Add(bestScore);
            Controls.Add(pongLabel);
            Controls.Add(optionsButton);
            Controls.Add(startGame);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(818, 497);
            Name = "mainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Понг";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public Button startGame;
        public Button optionsButton;
        private ToolStripMenuItem settingToolStripMenuItem;
        public MenuStrip menuStrip1;
        public ToolStripMenuItem gameToolStripMenuItem;
        public ToolStripMenuItem pauseToolStripMenuItem;
        public ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem;
        public Button helpButton;
        public Label pongLabel;
        public Label bestScore;
        public Button scoreButton;
        private ToolStripMenuItem рейтингToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem сохранитьБазуДанныхToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem загрузитьToolStripMenuItem;
    }
}