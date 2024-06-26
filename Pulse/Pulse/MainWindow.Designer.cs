﻿namespace Pulse
{
    partial class MainWindow
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.timerRender = new System.Windows.Forms.Timer(this.components);
            this.buttonNextLevel = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.pictureBoxGlyph = new System.Windows.Forms.PictureBox();
            this.labelGlyphText = new System.Windows.Forms.Label();
            this.buttonFullscreen = new System.Windows.Forms.Button();
            this.checkBoxFlipscreens = new System.Windows.Forms.CheckBox();
            this.labelFPS = new System.Windows.Forms.Label();
            this.labelTeam = new System.Windows.Forms.Label();
            this.textBoxTeamName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGlyph)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(371, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(122, 52);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start Game";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // timerRender
            // 
            this.timerRender.Interval = 1;
            this.timerRender.Tick += new System.EventHandler(this.timerRender_Tick);
            // 
            // buttonNextLevel
            // 
            this.buttonNextLevel.Location = new System.Drawing.Point(12, 293);
            this.buttonNextLevel.Name = "buttonNextLevel";
            this.buttonNextLevel.Size = new System.Drawing.Size(111, 35);
            this.buttonNextLevel.TabIndex = 1;
            this.buttonNextLevel.Text = "Next level";
            this.buttonNextLevel.UseVisualStyleBackColor = true;
            this.buttonNextLevel.Click += new System.EventHandler(this.buttonNextLevel_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(19, 19);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(103, 44);
            this.buttonRegister.TabIndex = 2;
            this.buttonRegister.Text = "Register Glyphs";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // pictureBoxGlyph
            // 
            this.pictureBoxGlyph.Location = new System.Drawing.Point(19, 105);
            this.pictureBoxGlyph.Name = "pictureBoxGlyph";
            this.pictureBoxGlyph.Size = new System.Drawing.Size(124, 114);
            this.pictureBoxGlyph.TabIndex = 3;
            this.pictureBoxGlyph.TabStop = false;
            // 
            // labelGlyphText
            // 
            this.labelGlyphText.AutoSize = true;
            this.labelGlyphText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGlyphText.Location = new System.Drawing.Point(16, 232);
            this.labelGlyphText.Name = "labelGlyphText";
            this.labelGlyphText.Size = new System.Drawing.Size(101, 20);
            this.labelGlyphText.TabIndex = 4;
            this.labelGlyphText.Text = "Scan Glyph";
            // 
            // buttonFullscreen
            // 
            this.buttonFullscreen.Location = new System.Drawing.Point(380, 293);
            this.buttonFullscreen.Name = "buttonFullscreen";
            this.buttonFullscreen.Size = new System.Drawing.Size(100, 36);
            this.buttonFullscreen.TabIndex = 5;
            this.buttonFullscreen.Text = "Fullscreen";
            this.buttonFullscreen.UseVisualStyleBackColor = true;
            this.buttonFullscreen.Click += new System.EventHandler(this.buttonFullscreen_Click);
            // 
            // checkBoxFlipscreens
            // 
            this.checkBoxFlipscreens.AutoSize = true;
            this.checkBoxFlipscreens.Location = new System.Drawing.Point(380, 342);
            this.checkBoxFlipscreens.Name = "checkBoxFlipscreens";
            this.checkBoxFlipscreens.Size = new System.Drawing.Size(82, 17);
            this.checkBoxFlipscreens.TabIndex = 6;
            this.checkBoxFlipscreens.Text = "Flip screens";
            this.checkBoxFlipscreens.UseVisualStyleBackColor = true;
            // 
            // labelFPS
            // 
            this.labelFPS.AutoSize = true;
            this.labelFPS.Location = new System.Drawing.Point(374, 77);
            this.labelFPS.Name = "labelFPS";
            this.labelFPS.Size = new System.Drawing.Size(54, 13);
            this.labelFPS.TabIndex = 7;
            this.labelFPS.Text = "FPS: 30.0";
            // 
            // labelTeam
            // 
            this.labelTeam.AutoSize = true;
            this.labelTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTeam.Location = new System.Drawing.Point(217, 119);
            this.labelTeam.Name = "labelTeam";
            this.labelTeam.Size = new System.Drawing.Size(110, 20);
            this.labelTeam.TabIndex = 8;
            this.labelTeam.Text = "Runner Team:";
            // 
            // textBoxTeamName
            // 
            this.textBoxTeamName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTeamName.Location = new System.Drawing.Point(333, 119);
            this.textBoxTeamName.Name = "textBoxTeamName";
            this.textBoxTeamName.Size = new System.Drawing.Size(160, 26);
            this.textBoxTeamName.TabIndex = 9;
            this.textBoxTeamName.Text = "TeamName";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 371);
            this.Controls.Add(this.textBoxTeamName);
            this.Controls.Add(this.labelTeam);
            this.Controls.Add(this.labelFPS);
            this.Controls.Add(this.checkBoxFlipscreens);
            this.Controls.Add(this.buttonFullscreen);
            this.Controls.Add(this.labelGlyphText);
            this.Controls.Add(this.pictureBoxGlyph);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonNextLevel);
            this.Controls.Add(this.buttonStart);
            this.Name = "MainWindow";
            this.Text = "Pulse";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGlyph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Timer timerRender;
        private System.Windows.Forms.Button buttonNextLevel;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.PictureBox pictureBoxGlyph;
        private System.Windows.Forms.Label labelGlyphText;
        private System.Windows.Forms.Button buttonFullscreen;
        private System.Windows.Forms.CheckBox checkBoxFlipscreens;
        private System.Windows.Forms.Label labelFPS;
        private System.Windows.Forms.Label labelTeam;
        private System.Windows.Forms.TextBox textBoxTeamName;
    }
}

