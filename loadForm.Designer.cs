namespace myPongGame
{
    partial class loadForm
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
            loadBox = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // loadBox
            // 
            loadBox.FormattingEnabled = true;
            loadBox.Location = new Point(34, 60);
            loadBox.Name = "loadBox";
            loadBox.Size = new Size(287, 28);
            loadBox.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(121, 116);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "Загрузить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(93, 22);
            label1.Name = "label1";
            label1.Size = new Size(168, 20);
            label1.TabIndex = 2;
            label1.Text = "Выберите сохранение:";
            // 
            // loadForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(335, 157);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(loadBox);
            MinimumSize = new Size(353, 204);
            Name = "loadForm";
            Text = "loadForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox loadBox;
        private Button button1;
        private Label label1;
    }
}