﻿namespace WebRunner
{
    partial class LevelEditor
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
            this.radioButtonCamera = new System.Windows.Forms.RadioButton();
            this.radioButtonShielding = new System.Windows.Forms.RadioButton();
            this.radioButtonFirewall = new System.Windows.Forms.RadioButton();
            this.radioButtonWall = new System.Windows.Forms.RadioButton();
            this.timerRendering = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLevelName = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMissionName = new System.Windows.Forms.TextBox();
            this.radioButtonSelect = new System.Windows.Forms.RadioButton();
            this.scrollAngleA = new System.Windows.Forms.HScrollBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.scrollAngleB = new System.Windows.Forms.HScrollBar();
            this.label5 = new System.Windows.Forms.Label();
            this.scrollSpeed = new System.Windows.Forms.HScrollBar();
            this.labelAngleA = new System.Windows.Forms.Label();
            this.labelAngleB = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButtonSpawnPoint = new System.Windows.Forms.RadioButton();
            this.radioButtonDoor = new System.Windows.Forms.RadioButton();
            this.radioButtonObjective = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.Location = new System.Drawing.Point(20, 28);
            this.pictureBoxMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(1280, 720);
            this.pictureBoxMain.TabIndex = 0;
            this.pictureBoxMain.TabStop = false;
            this.pictureBoxMain.Click += new System.EventHandler(this.pictureBoxMain_Click);
            this.pictureBoxMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseDown);
            this.pictureBoxMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseMove);
            // 
            // radioButtonCamera
            // 
            this.radioButtonCamera.AutoSize = true;
            this.radioButtonCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCamera.Location = new System.Drawing.Point(12, 798);
            this.radioButtonCamera.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonCamera.Name = "radioButtonCamera";
            this.radioButtonCamera.Size = new System.Drawing.Size(83, 24);
            this.radioButtonCamera.TabIndex = 1;
            this.radioButtonCamera.TabStop = true;
            this.radioButtonCamera.Text = "Camera";
            this.radioButtonCamera.UseVisualStyleBackColor = true;
            this.radioButtonCamera.CheckedChanged += new System.EventHandler(this.radioButtonCamera_CheckedChanged);
            // 
            // radioButtonShielding
            // 
            this.radioButtonShielding.AutoSize = true;
            this.radioButtonShielding.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonShielding.Location = new System.Drawing.Point(12, 828);
            this.radioButtonShielding.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonShielding.Name = "radioButtonShielding";
            this.radioButtonShielding.Size = new System.Drawing.Size(92, 24);
            this.radioButtonShielding.TabIndex = 1;
            this.radioButtonShielding.TabStop = true;
            this.radioButtonShielding.Text = "Shielding";
            this.radioButtonShielding.UseVisualStyleBackColor = true;
            this.radioButtonShielding.CheckedChanged += new System.EventHandler(this.radioButtonShielding_CheckedChanged);
            // 
            // radioButtonFirewall
            // 
            this.radioButtonFirewall.AutoSize = true;
            this.radioButtonFirewall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFirewall.Location = new System.Drawing.Point(12, 858);
            this.radioButtonFirewall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonFirewall.Name = "radioButtonFirewall";
            this.radioButtonFirewall.Size = new System.Drawing.Size(80, 24);
            this.radioButtonFirewall.TabIndex = 1;
            this.radioButtonFirewall.TabStop = true;
            this.radioButtonFirewall.Text = "Firewall";
            this.radioButtonFirewall.UseVisualStyleBackColor = true;
            this.radioButtonFirewall.CheckedChanged += new System.EventHandler(this.radioButtonFirewall_CheckedChanged);
            // 
            // radioButtonWall
            // 
            this.radioButtonWall.AutoSize = true;
            this.radioButtonWall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonWall.Location = new System.Drawing.Point(12, 888);
            this.radioButtonWall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonWall.Name = "radioButtonWall";
            this.radioButtonWall.Size = new System.Drawing.Size(57, 24);
            this.radioButtonWall.TabIndex = 1;
            this.radioButtonWall.TabStop = true;
            this.radioButtonWall.Text = "Wall";
            this.radioButtonWall.UseVisualStyleBackColor = true;
            this.radioButtonWall.CheckedChanged += new System.EventHandler(this.radioButtonWall_CheckedChanged);
            // 
            // timerRendering
            // 
            this.timerRendering.Enabled = true;
            this.timerRendering.Interval = 1;
            this.timerRendering.Tick += new System.EventHandler(this.timerRendering_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(1976, 735);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(331, 26);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(819, 795);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Level Name:";
            // 
            // textBoxLevelName
            // 
            this.textBoxLevelName.Location = new System.Drawing.Point(921, 795);
            this.textBoxLevelName.Name = "textBoxLevelName";
            this.textBoxLevelName.Size = new System.Drawing.Size(168, 26);
            this.textBoxLevelName.TabIndex = 5;
            this.textBoxLevelName.Text = "defaultLevel";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(1111, 773);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(89, 28);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(1212, 774);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(88, 28);
            this.buttonLoad.TabIndex = 7;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(803, 762);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Mission Name:";
            // 
            // textBoxMissionName
            // 
            this.textBoxMissionName.Location = new System.Drawing.Point(921, 759);
            this.textBoxMissionName.Name = "textBoxMissionName";
            this.textBoxMissionName.Size = new System.Drawing.Size(168, 26);
            this.textBoxMissionName.TabIndex = 9;
            this.textBoxMissionName.Text = "defaultMission";
            // 
            // radioButtonSelect
            // 
            this.radioButtonSelect.AutoSize = true;
            this.radioButtonSelect.Location = new System.Drawing.Point(12, 762);
            this.radioButtonSelect.Name = "radioButtonSelect";
            this.radioButtonSelect.Size = new System.Drawing.Size(142, 24);
            this.radioButtonSelect.TabIndex = 10;
            this.radioButtonSelect.TabStop = true;
            this.radioButtonSelect.Text = "Select Structure";
            this.radioButtonSelect.UseVisualStyleBackColor = true;
            this.radioButtonSelect.CheckedChanged += new System.EventHandler(this.radioButtonSelect_CheckedChanged);
            // 
            // scrollAngleA
            // 
            this.scrollAngleA.Location = new System.Drawing.Point(370, 798);
            this.scrollAngleA.Maximum = 360;
            this.scrollAngleA.Name = "scrollAngleA";
            this.scrollAngleA.Size = new System.Drawing.Size(328, 20);
            this.scrollAngleA.TabIndex = 11;
            this.scrollAngleA.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollAngleA_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 798);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Camera angle start:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 826);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Camera angle size:";
            // 
            // scrollAngleB
            // 
            this.scrollAngleB.Location = new System.Drawing.Point(370, 828);
            this.scrollAngleB.Maximum = 360;
            this.scrollAngleB.Minimum = 20;
            this.scrollAngleB.Name = "scrollAngleB";
            this.scrollAngleB.Size = new System.Drawing.Size(328, 20);
            this.scrollAngleB.TabIndex = 11;
            this.scrollAngleB.Value = 20;
            this.scrollAngleB.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollAngleB_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(250, 858);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Camera speed:";
            // 
            // scrollSpeed
            // 
            this.scrollSpeed.Location = new System.Drawing.Point(370, 858);
            this.scrollSpeed.Name = "scrollSpeed";
            this.scrollSpeed.Size = new System.Drawing.Size(328, 20);
            this.scrollSpeed.TabIndex = 11;
            this.scrollSpeed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollSpeed_Scroll);
            // 
            // labelAngleA
            // 
            this.labelAngleA.AutoSize = true;
            this.labelAngleA.Location = new System.Drawing.Point(701, 798);
            this.labelAngleA.Name = "labelAngleA";
            this.labelAngleA.Size = new System.Drawing.Size(31, 20);
            this.labelAngleA.TabIndex = 13;
            this.labelAngleA.Text = "0.0";
            // 
            // labelAngleB
            // 
            this.labelAngleB.AutoSize = true;
            this.labelAngleB.Location = new System.Drawing.Point(701, 828);
            this.labelAngleB.Name = "labelAngleB";
            this.labelAngleB.Size = new System.Drawing.Size(31, 20);
            this.labelAngleB.TabIndex = 13;
            this.labelAngleB.Text = "0.0";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(701, 858);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(31, 20);
            this.labelSpeed.TabIndex = 13;
            this.labelSpeed.Text = "0.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(381, 765);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Structure Properties";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(998, 839);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Level Properties";
            // 
            // radioButtonSpawnPoint
            // 
            this.radioButtonSpawnPoint.AutoSize = true;
            this.radioButtonSpawnPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSpawnPoint.Location = new System.Drawing.Point(12, 922);
            this.radioButtonSpawnPoint.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonSpawnPoint.Name = "radioButtonSpawnPoint";
            this.radioButtonSpawnPoint.Size = new System.Drawing.Size(116, 24);
            this.radioButtonSpawnPoint.TabIndex = 1;
            this.radioButtonSpawnPoint.TabStop = true;
            this.radioButtonSpawnPoint.Text = "Spawn Point";
            this.radioButtonSpawnPoint.UseVisualStyleBackColor = true;
            this.radioButtonSpawnPoint.CheckedChanged += new System.EventHandler(this.radioButtonSpawnPoint_CheckedChanged);
            // 
            // radioButtonDoor
            // 
            this.radioButtonDoor.AutoSize = true;
            this.radioButtonDoor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDoor.Location = new System.Drawing.Point(12, 956);
            this.radioButtonDoor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonDoor.Name = "radioButtonDoor";
            this.radioButtonDoor.Size = new System.Drawing.Size(62, 24);
            this.radioButtonDoor.TabIndex = 1;
            this.radioButtonDoor.TabStop = true;
            this.radioButtonDoor.Text = "Door";
            this.radioButtonDoor.UseVisualStyleBackColor = true;
            this.radioButtonDoor.CheckedChanged += new System.EventHandler(this.radioButtonDoor_CheckedChanged);
            // 
            // radioButtonObjective
            // 
            this.radioButtonObjective.AutoSize = true;
            this.radioButtonObjective.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonObjective.Location = new System.Drawing.Point(12, 990);
            this.radioButtonObjective.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonObjective.Name = "radioButtonObjective";
            this.radioButtonObjective.Size = new System.Drawing.Size(92, 24);
            this.radioButtonObjective.TabIndex = 1;
            this.radioButtonObjective.TabStop = true;
            this.radioButtonObjective.Text = "Objective";
            this.radioButtonObjective.UseVisualStyleBackColor = true;
            this.radioButtonObjective.CheckedChanged += new System.EventHandler(this.radioButtonObjective_CheckedChanged);
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1434, 1116);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.labelAngleB);
            this.Controls.Add(this.labelAngleA);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.scrollSpeed);
            this.Controls.Add(this.scrollAngleB);
            this.Controls.Add(this.scrollAngleA);
            this.Controls.Add(this.radioButtonSelect);
            this.Controls.Add(this.textBoxMissionName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxLevelName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.radioButtonObjective);
            this.Controls.Add(this.radioButtonDoor);
            this.Controls.Add(this.radioButtonSpawnPoint);
            this.Controls.Add(this.radioButtonWall);
            this.Controls.Add(this.radioButtonFirewall);
            this.Controls.Add(this.radioButtonShielding);
            this.Controls.Add(this.radioButtonCamera);
            this.Controls.Add(this.pictureBoxMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LevelEditor";
            this.Text = "LevelEditor";
            this.Load += new System.EventHandler(this.LevelEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.RadioButton radioButtonCamera;
        private System.Windows.Forms.RadioButton radioButtonShielding;
        private System.Windows.Forms.RadioButton radioButtonFirewall;
        private System.Windows.Forms.RadioButton radioButtonWall;
        private System.Windows.Forms.Timer timerRendering;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLevelName;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMissionName;
        private System.Windows.Forms.RadioButton radioButtonSelect;
        private System.Windows.Forms.HScrollBar scrollAngleA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.HScrollBar scrollAngleB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.HScrollBar scrollSpeed;
        private System.Windows.Forms.Label labelAngleA;
        private System.Windows.Forms.Label labelAngleB;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButtonSpawnPoint;
        private System.Windows.Forms.RadioButton radioButtonDoor;
        private System.Windows.Forms.RadioButton radioButtonObjective;
    }
}