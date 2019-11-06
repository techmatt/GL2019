namespace WebRunner
{
    partial class GameWindow
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
            this.components = new System.ComponentModel.Container();
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.timerRendering = new System.Windows.Forms.Timer(this.components);
            this.buttonFullScreen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMissionName = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonComplete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTeam = new System.Windows.Forms.TextBox();
            this.buttonAdvanceLevel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.Location = new System.Drawing.Point(13, 86);
            this.pictureBoxMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(1280, 720);
            this.pictureBoxMain.TabIndex = 0;
            this.pictureBoxMain.TabStop = false;
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartGame.Location = new System.Drawing.Point(435, 7);
            this.buttonStartGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(164, 43);
            this.buttonStartGame.TabIndex = 1;
            this.buttonStartGame.Text = "Start Mission";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // timerRendering
            // 
            this.timerRendering.Enabled = true;
            this.timerRendering.Interval = 1;
            this.timerRendering.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonFullScreen
            // 
            this.buttonFullScreen.Location = new System.Drawing.Point(607, 7);
            this.buttonFullScreen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFullScreen.Name = "buttonFullScreen";
            this.buttonFullScreen.Size = new System.Drawing.Size(198, 43);
            this.buttonFullScreen.TabIndex = 2;
            this.buttonFullScreen.Text = "enter full screen";
            this.buttonFullScreen.UseVisualStyleBackColor = true;
            this.buttonFullScreen.Click += new System.EventHandler(this.buttonFullScreen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(104, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mission Name:";
            // 
            // textBoxMissionName
            // 
            this.textBoxMissionName.Location = new System.Drawing.Point(228, 7);
            this.textBoxMissionName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxMissionName.Name = "textBoxMissionName";
            this.textBoxMissionName.Size = new System.Drawing.Size(197, 26);
            this.textBoxMissionName.TabIndex = 4;
            this.textBoxMissionName.Text = "tutorialMission";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(12, 12);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(86, 38);
            this.buttonBrowse.TabIndex = 5;
            this.buttonBrowse.Text = "Load";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonComplete
            // 
            this.buttonComplete.Location = new System.Drawing.Point(812, 9);
            this.buttonComplete.Name = "buttonComplete";
            this.buttonComplete.Size = new System.Drawing.Size(135, 41);
            this.buttonComplete.TabIndex = 6;
            this.buttonComplete.Text = "Complete Level";
            this.buttonComplete.UseVisualStyleBackColor = true;
            this.buttonComplete.Click += new System.EventHandler(this.buttonComplete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(120, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Team Name:";
            // 
            // textBoxTeam
            // 
            this.textBoxTeam.Location = new System.Drawing.Point(228, 38);
            this.textBoxTeam.Name = "textBoxTeam";
            this.textBoxTeam.Size = new System.Drawing.Size(197, 26);
            this.textBoxTeam.TabIndex = 7;
            this.textBoxTeam.Text = "NoTeamName";
            // 
            // buttonAdvanceLevel
            // 
            this.buttonAdvanceLevel.Location = new System.Drawing.Point(952, 10);
            this.buttonAdvanceLevel.Name = "buttonAdvanceLevel";
            this.buttonAdvanceLevel.Size = new System.Drawing.Size(154, 39);
            this.buttonAdvanceLevel.TabIndex = 8;
            this.buttonAdvanceLevel.Text = "Advance Level";
            this.buttonAdvanceLevel.UseVisualStyleBackColor = true;
            this.buttonAdvanceLevel.Click += new System.EventHandler(this.buttonAdvanceLevel_Click);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.buttonAdvanceLevel);
            this.Controls.Add(this.textBoxTeam);
            this.Controls.Add(this.buttonComplete);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxMissionName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonFullScreen);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.pictureBoxMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GameWindow";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Timer timerRendering;
        private System.Windows.Forms.Button buttonFullScreen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMissionName;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonComplete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTeam;
        private System.Windows.Forms.Button buttonAdvanceLevel;
    }
}