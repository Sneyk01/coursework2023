namespace myPongGame
{
    partial class Settings
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
            botSetting = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            mapSetting = new ComboBox();
            mapSizeSetting = new ComboBox();
            saveButton = new Button();
            label4 = new Label();
            cancelButton = new Button();
            dbButton = new Button();
            openFileDialog1 = new OpenFileDialog();
            SuspendLayout();
            // 
            // botSetting
            // 
            botSetting.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            botSetting.DropDownStyle = ComboBoxStyle.DropDownList;
            botSetting.FormattingEnabled = true;
            botSetting.Items.AddRange(new object[] { "Обычный", "Сложный" });
            botSetting.Location = new Point(281, 56);
            botSetting.Name = "botSetting";
            botSetting.Size = new Size(151, 28);
            botSetting.TabIndex = 1;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 56);
            label1.Name = "label1";
            label1.Size = new Size(246, 28);
            label1.TabIndex = 1;
            label1.Text = "Уровень сложности бота:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 106);
            label2.Name = "label2";
            label2.Size = new Size(258, 28);
            label2.TabIndex = 2;
            label2.Text = "Уровень сложности карты:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 156);
            label3.Name = "label3";
            label3.Size = new Size(272, 28);
            label3.TabIndex = 3;
            label3.Text = "Разрешение игровой карты:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mapSetting
            // 
            mapSetting.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mapSetting.DropDownStyle = ComboBoxStyle.DropDownList;
            mapSetting.FormattingEnabled = true;
            mapSetting.Items.AddRange(new object[] { "Без стен", "Средний", "Сложный" });
            mapSetting.Location = new Point(281, 110);
            mapSetting.Name = "mapSetting";
            mapSetting.Size = new Size(151, 28);
            mapSetting.TabIndex = 2;
            // 
            // mapSizeSetting
            // 
            mapSizeSetting.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mapSizeSetting.DropDownStyle = ComboBoxStyle.DropDownList;
            mapSizeSetting.FormattingEnabled = true;
            mapSizeSetting.Items.AddRange(new object[] { "770х400", "1240x640", "1890x960" });
            mapSizeSetting.Location = new Point(281, 160);
            mapSizeSetting.Name = "mapSizeSetting";
            mapSizeSetting.Size = new Size(151, 28);
            mapSizeSetting.TabIndex = 3;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            saveButton.Location = new Point(100, 246);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(94, 29);
            saveButton.TabIndex = 4;
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(-1, 9);
            label4.Name = "label4";
            label4.Size = new Size(442, 32);
            label4.TabIndex = 7;
            label4.Text = "Настройки";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(229, 246);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(94, 29);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Отменить";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // dbButton
            // 
            dbButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dbButton.Location = new Point(12, 205);
            dbButton.Name = "dbButton";
            dbButton.Size = new Size(420, 29);
            dbButton.TabIndex = 8;
            dbButton.Text = "Обновить конфигурацию базы данных";
            dbButton.UseVisualStyleBackColor = true;
            dbButton.Click += dbButton_Click;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(444, 287);
            Controls.Add(dbButton);
            Controls.Add(cancelButton);
            Controls.Add(label4);
            Controls.Add(saveButton);
            Controls.Add(mapSizeSetting);
            Controls.Add(mapSetting);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(botSetting);
            MinimumSize = new Size(462, 334);
            Name = "Settings";
            Text = "Настройки";
            ResumeLayout(false);
        }

        #endregion

        public ComboBox botSetting;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button saveButton;
        private Label label4;
        private Button cancelButton;
        public ComboBox mapSetting;
        public ComboBox mapSizeSetting;
        public Button dbButton;
        private OpenFileDialog openFileDialog1;
    }
}