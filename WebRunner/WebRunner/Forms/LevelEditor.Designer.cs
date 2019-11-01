namespace WebRunner
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
            this.radioButtonSpawnA = new System.Windows.Forms.RadioButton();
            this.radioButtonDoor = new System.Windows.Forms.RadioButton();
            this.radioButtonObjective = new System.Windows.Forms.RadioButton();
            this.scrollICESpawnRate = new System.Windows.Forms.HScrollBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.scrollGuardSpawnRate = new System.Windows.Forms.HScrollBar();
            this.scrollMaxTime = new System.Windows.Forms.HScrollBar();
            this.hScrollBar4 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar5 = new System.Windows.Forms.HScrollBar();
            this.labelIceSpawnRate = new System.Windows.Forms.Label();
            this.labelGuardSpawnRate = new System.Windows.Forms.Label();
            this.labelMaxTime = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.radioButtonSpawnB = new System.Windows.Forms.RadioButton();
            this.radioButtonShoes = new System.Windows.Forms.RadioButton();
            this.radioButtonLaserGun = new System.Windows.Forms.RadioButton();
            this.radioButtonLaserTurret = new System.Windows.Forms.RadioButton();
            this.radioButtonBulletTurret = new System.Windows.Forms.RadioButton();
            this.radioButtonMirror = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.Location = new System.Drawing.Point(3, 7);
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
            this.radioButtonCamera.Location = new System.Drawing.Point(12, 771);
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
            this.radioButtonShielding.Location = new System.Drawing.Point(12, 801);
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
            this.radioButtonFirewall.Location = new System.Drawing.Point(12, 831);
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
            this.radioButtonWall.Location = new System.Drawing.Point(12, 861);
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
            this.label1.Location = new System.Drawing.Point(819, 768);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Level Name:";
            // 
            // textBoxLevelName
            // 
            this.textBoxLevelName.Location = new System.Drawing.Point(921, 768);
            this.textBoxLevelName.Name = "textBoxLevelName";
            this.textBoxLevelName.Size = new System.Drawing.Size(168, 26);
            this.textBoxLevelName.TabIndex = 5;
            this.textBoxLevelName.Text = "defaultLevel";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(1111, 746);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(89, 28);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(1212, 747);
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
            this.label2.Location = new System.Drawing.Point(803, 735);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Mission Name:";
            // 
            // textBoxMissionName
            // 
            this.textBoxMissionName.Location = new System.Drawing.Point(921, 732);
            this.textBoxMissionName.Name = "textBoxMissionName";
            this.textBoxMissionName.Size = new System.Drawing.Size(168, 26);
            this.textBoxMissionName.TabIndex = 9;
            this.textBoxMissionName.Text = "defaultMission";
            // 
            // radioButtonSelect
            // 
            this.radioButtonSelect.AutoSize = true;
            this.radioButtonSelect.Location = new System.Drawing.Point(12, 735);
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
            this.scrollAngleA.Location = new System.Drawing.Point(370, 771);
            this.scrollAngleA.Maximum = 360;
            this.scrollAngleA.Name = "scrollAngleA";
            this.scrollAngleA.Size = new System.Drawing.Size(328, 20);
            this.scrollAngleA.TabIndex = 11;
            this.scrollAngleA.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollAngleA_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 771);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Camera angle start:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 799);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Camera angle size:";
            // 
            // scrollAngleB
            // 
            this.scrollAngleB.Location = new System.Drawing.Point(370, 801);
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
            this.label5.Location = new System.Drawing.Point(250, 831);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Camera speed:";
            // 
            // scrollSpeed
            // 
            this.scrollSpeed.Location = new System.Drawing.Point(370, 831);
            this.scrollSpeed.Name = "scrollSpeed";
            this.scrollSpeed.Size = new System.Drawing.Size(328, 20);
            this.scrollSpeed.TabIndex = 11;
            this.scrollSpeed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollSpeed_Scroll);
            // 
            // labelAngleA
            // 
            this.labelAngleA.AutoSize = true;
            this.labelAngleA.Location = new System.Drawing.Point(701, 771);
            this.labelAngleA.Name = "labelAngleA";
            this.labelAngleA.Size = new System.Drawing.Size(31, 20);
            this.labelAngleA.TabIndex = 13;
            this.labelAngleA.Text = "0.0";
            // 
            // labelAngleB
            // 
            this.labelAngleB.AutoSize = true;
            this.labelAngleB.Location = new System.Drawing.Point(701, 801);
            this.labelAngleB.Name = "labelAngleB";
            this.labelAngleB.Size = new System.Drawing.Size(31, 20);
            this.labelAngleB.TabIndex = 13;
            this.labelAngleB.Text = "0.0";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(701, 831);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(31, 20);
            this.labelSpeed.TabIndex = 13;
            this.labelSpeed.Text = "0.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(381, 738);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Structure Properties";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(998, 812);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Level Properties";
            // 
            // radioButtonSpawnA
            // 
            this.radioButtonSpawnA.AutoSize = true;
            this.radioButtonSpawnA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSpawnA.Location = new System.Drawing.Point(12, 895);
            this.radioButtonSpawnA.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonSpawnA.Name = "radioButtonSpawnA";
            this.radioButtonSpawnA.Size = new System.Drawing.Size(87, 24);
            this.radioButtonSpawnA.TabIndex = 1;
            this.radioButtonSpawnA.TabStop = true;
            this.radioButtonSpawnA.Text = "SpawnA";
            this.radioButtonSpawnA.UseVisualStyleBackColor = true;
            this.radioButtonSpawnA.CheckedChanged += new System.EventHandler(this.radioButtonSpawnA_CheckedChanged);
            // 
            // radioButtonDoor
            // 
            this.radioButtonDoor.AutoSize = true;
            this.radioButtonDoor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDoor.Location = new System.Drawing.Point(12, 929);
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
            this.radioButtonObjective.Location = new System.Drawing.Point(12, 963);
            this.radioButtonObjective.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonObjective.Name = "radioButtonObjective";
            this.radioButtonObjective.Size = new System.Drawing.Size(92, 24);
            this.radioButtonObjective.TabIndex = 1;
            this.radioButtonObjective.TabStop = true;
            this.radioButtonObjective.Text = "Objective";
            this.radioButtonObjective.UseVisualStyleBackColor = true;
            this.radioButtonObjective.CheckedChanged += new System.EventHandler(this.radioButtonObjective_CheckedChanged);
            // 
            // scrollICESpawnRate
            // 
            this.scrollICESpawnRate.Location = new System.Drawing.Point(954, 911);
            this.scrollICESpawnRate.Name = "scrollICESpawnRate";
            this.scrollICESpawnRate.Size = new System.Drawing.Size(256, 22);
            this.scrollICESpawnRate.TabIndex = 14;
            this.scrollICESpawnRate.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollICESpawnRate_Scroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(829, 909);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "ICE spawn rate:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(811, 874);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "Guard spawn rate:";
            // 
            // scrollGuardSpawnRate
            // 
            this.scrollGuardSpawnRate.Location = new System.Drawing.Point(954, 876);
            this.scrollGuardSpawnRate.Name = "scrollGuardSpawnRate";
            this.scrollGuardSpawnRate.Size = new System.Drawing.Size(256, 22);
            this.scrollGuardSpawnRate.TabIndex = 14;
            this.scrollGuardSpawnRate.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollGuardSpawnRate_Scroll);
            // 
            // scrollMaxTime
            // 
            this.scrollMaxTime.Location = new System.Drawing.Point(954, 844);
            this.scrollMaxTime.Name = "scrollMaxTime";
            this.scrollMaxTime.Size = new System.Drawing.Size(256, 22);
            this.scrollMaxTime.TabIndex = 14;
            this.scrollMaxTime.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollMaxTime_Scroll);
            // 
            // hScrollBar4
            // 
            this.hScrollBar4.Location = new System.Drawing.Point(954, 943);
            this.hScrollBar4.Name = "hScrollBar4";
            this.hScrollBar4.Size = new System.Drawing.Size(256, 22);
            this.hScrollBar4.TabIndex = 14;
            // 
            // hScrollBar5
            // 
            this.hScrollBar5.Location = new System.Drawing.Point(954, 977);
            this.hScrollBar5.Name = "hScrollBar5";
            this.hScrollBar5.Size = new System.Drawing.Size(256, 22);
            this.hScrollBar5.TabIndex = 14;
            // 
            // labelIceSpawnRate
            // 
            this.labelIceSpawnRate.AutoSize = true;
            this.labelIceSpawnRate.Location = new System.Drawing.Point(1213, 911);
            this.labelIceSpawnRate.Name = "labelIceSpawnRate";
            this.labelIceSpawnRate.Size = new System.Drawing.Size(31, 20);
            this.labelIceSpawnRate.TabIndex = 13;
            this.labelIceSpawnRate.Text = "0.0";
            // 
            // labelGuardSpawnRate
            // 
            this.labelGuardSpawnRate.AutoSize = true;
            this.labelGuardSpawnRate.Location = new System.Drawing.Point(1213, 876);
            this.labelGuardSpawnRate.Name = "labelGuardSpawnRate";
            this.labelGuardSpawnRate.Size = new System.Drawing.Size(31, 20);
            this.labelGuardSpawnRate.TabIndex = 13;
            this.labelGuardSpawnRate.Text = "0.0";
            // 
            // labelMaxTime
            // 
            this.labelMaxTime.AutoSize = true;
            this.labelMaxTime.Location = new System.Drawing.Point(1213, 844);
            this.labelMaxTime.Name = "labelMaxTime";
            this.labelMaxTime.Size = new System.Drawing.Size(31, 20);
            this.labelMaxTime.TabIndex = 13;
            this.labelMaxTime.Text = "0.0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1213, 943);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 20);
            this.label13.TabIndex = 13;
            this.label13.Text = "0.0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1213, 975);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 20);
            this.label14.TabIndex = 13;
            this.label14.Text = "0.0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(875, 844);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "Max time:";
            // 
            // radioButtonSpawnB
            // 
            this.radioButtonSpawnB.AutoSize = true;
            this.radioButtonSpawnB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSpawnB.Location = new System.Drawing.Point(107, 895);
            this.radioButtonSpawnB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonSpawnB.Name = "radioButtonSpawnB";
            this.radioButtonSpawnB.Size = new System.Drawing.Size(87, 24);
            this.radioButtonSpawnB.TabIndex = 1;
            this.radioButtonSpawnB.TabStop = true;
            this.radioButtonSpawnB.Text = "SpawnB";
            this.radioButtonSpawnB.UseVisualStyleBackColor = true;
            this.radioButtonSpawnB.CheckedChanged += new System.EventHandler(this.radioButtonSpawnB_CheckedChanged);
            // 
            // radioButtonShoes
            // 
            this.radioButtonShoes.AutoSize = true;
            this.radioButtonShoes.Location = new System.Drawing.Point(111, 929);
            this.radioButtonShoes.Name = "radioButtonShoes";
            this.radioButtonShoes.Size = new System.Drawing.Size(73, 24);
            this.radioButtonShoes.TabIndex = 15;
            this.radioButtonShoes.TabStop = true;
            this.radioButtonShoes.Text = "Shoes";
            this.radioButtonShoes.UseVisualStyleBackColor = true;
            this.radioButtonShoes.CheckedChanged += new System.EventHandler(this.radioButtonShoes_CheckedChanged);
            // 
            // radioButtonLaserGun
            // 
            this.radioButtonLaserGun.AutoSize = true;
            this.radioButtonLaserGun.Location = new System.Drawing.Point(111, 963);
            this.radioButtonLaserGun.Name = "radioButtonLaserGun";
            this.radioButtonLaserGun.Size = new System.Drawing.Size(102, 24);
            this.radioButtonLaserGun.TabIndex = 16;
            this.radioButtonLaserGun.TabStop = true;
            this.radioButtonLaserGun.Text = "Laser Gun";
            this.radioButtonLaserGun.UseVisualStyleBackColor = true;
            this.radioButtonLaserGun.CheckedChanged += new System.EventHandler(this.radioButtonLaserGun_CheckedChanged);
            // 
            // radioButtonLaserTurret
            // 
            this.radioButtonLaserTurret.AutoSize = true;
            this.radioButtonLaserTurret.Location = new System.Drawing.Point(12, 995);
            this.radioButtonLaserTurret.Name = "radioButtonLaserTurret";
            this.radioButtonLaserTurret.Size = new System.Drawing.Size(113, 24);
            this.radioButtonLaserTurret.TabIndex = 16;
            this.radioButtonLaserTurret.TabStop = true;
            this.radioButtonLaserTurret.Text = "Laser Turret";
            this.radioButtonLaserTurret.UseVisualStyleBackColor = true;
            this.radioButtonLaserTurret.CheckedChanged += new System.EventHandler(this.radioButtonLaserTurret_CheckedChanged);
            // 
            // radioButtonBulletTurret
            // 
            this.radioButtonBulletTurret.AutoSize = true;
            this.radioButtonBulletTurret.Location = new System.Drawing.Point(12, 1025);
            this.radioButtonBulletTurret.Name = "radioButtonBulletTurret";
            this.radioButtonBulletTurret.Size = new System.Drawing.Size(113, 24);
            this.radioButtonBulletTurret.TabIndex = 16;
            this.radioButtonBulletTurret.TabStop = true;
            this.radioButtonBulletTurret.Text = "Bullet Turret";
            this.radioButtonBulletTurret.UseVisualStyleBackColor = true;
            this.radioButtonBulletTurret.CheckedChanged += new System.EventHandler(this.radioButtonBulletTurret_CheckedChanged);
            // 
            // radioButtonMirror
            // 
            this.radioButtonMirror.AutoSize = true;
            this.radioButtonMirror.Location = new System.Drawing.Point(131, 995);
            this.radioButtonMirror.Name = "radioButtonMirror";
            this.radioButtonMirror.Size = new System.Drawing.Size(67, 24);
            this.radioButtonMirror.TabIndex = 16;
            this.radioButtonMirror.TabStop = true;
            this.radioButtonMirror.Text = "Mirror";
            this.radioButtonMirror.UseVisualStyleBackColor = true;
            this.radioButtonMirror.CheckedChanged += new System.EventHandler(this.radioButtonMirror_CheckedChanged);
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1434, 1116);
            this.Controls.Add(this.radioButtonMirror);
            this.Controls.Add(this.radioButtonBulletTurret);
            this.Controls.Add(this.radioButtonLaserTurret);
            this.Controls.Add(this.radioButtonLaserGun);
            this.Controls.Add(this.radioButtonShoes);
            this.Controls.Add(this.hScrollBar5);
            this.Controls.Add(this.hScrollBar4);
            this.Controls.Add(this.scrollMaxTime);
            this.Controls.Add(this.scrollGuardSpawnRate);
            this.Controls.Add(this.scrollICESpawnRate);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.labelAngleB);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.labelMaxTime);
            this.Controls.Add(this.labelGuardSpawnRate);
            this.Controls.Add(this.labelIceSpawnRate);
            this.Controls.Add(this.labelAngleA);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
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
            this.Controls.Add(this.radioButtonSpawnB);
            this.Controls.Add(this.radioButtonSpawnA);
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
        private System.Windows.Forms.RadioButton radioButtonSpawnA;
        private System.Windows.Forms.RadioButton radioButtonDoor;
        private System.Windows.Forms.RadioButton radioButtonObjective;
        private System.Windows.Forms.HScrollBar scrollICESpawnRate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.HScrollBar scrollGuardSpawnRate;
        private System.Windows.Forms.HScrollBar scrollMaxTime;
        private System.Windows.Forms.HScrollBar hScrollBar4;
        private System.Windows.Forms.HScrollBar hScrollBar5;
        private System.Windows.Forms.Label labelIceSpawnRate;
        private System.Windows.Forms.Label labelGuardSpawnRate;
        private System.Windows.Forms.Label labelMaxTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton radioButtonSpawnB;
        private System.Windows.Forms.RadioButton radioButtonShoes;
        private System.Windows.Forms.RadioButton radioButtonLaserGun;
        private System.Windows.Forms.RadioButton radioButtonLaserTurret;
        private System.Windows.Forms.RadioButton radioButtonBulletTurret;
        private System.Windows.Forms.RadioButton radioButtonMirror;
    }
}