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
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.Location = new System.Drawing.Point(13, 18);
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
            this.radioButtonCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCamera.Location = new System.Drawing.Point(13, 763);
            this.radioButtonCamera.Name = "radioButtonCamera";
            this.radioButtonCamera.Size = new System.Drawing.Size(74, 20);
            this.radioButtonCamera.TabIndex = 1;
            this.radioButtonCamera.TabStop = true;
            this.radioButtonCamera.Text = "Camera";
            this.radioButtonCamera.UseVisualStyleBackColor = true;
            this.radioButtonCamera.CheckedChanged += new System.EventHandler(this.radioButtonCamera_CheckedChanged);
            // 
            // radioButtonShielding
            // 
            this.radioButtonShielding.AutoSize = true;
            this.radioButtonShielding.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonShielding.Location = new System.Drawing.Point(12, 789);
            this.radioButtonShielding.Name = "radioButtonShielding";
            this.radioButtonShielding.Size = new System.Drawing.Size(82, 20);
            this.radioButtonShielding.TabIndex = 1;
            this.radioButtonShielding.TabStop = true;
            this.radioButtonShielding.Text = "Shielding";
            this.radioButtonShielding.UseVisualStyleBackColor = true;
            this.radioButtonShielding.CheckedChanged += new System.EventHandler(this.radioButtonShielding_CheckedChanged);
            // 
            // radioButtonFirewall
            // 
            this.radioButtonFirewall.AutoSize = true;
            this.radioButtonFirewall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFirewall.Location = new System.Drawing.Point(12, 815);
            this.radioButtonFirewall.Name = "radioButtonFirewall";
            this.radioButtonFirewall.Size = new System.Drawing.Size(72, 20);
            this.radioButtonFirewall.TabIndex = 1;
            this.radioButtonFirewall.TabStop = true;
            this.radioButtonFirewall.Text = "Firewall";
            this.radioButtonFirewall.UseVisualStyleBackColor = true;
            this.radioButtonFirewall.CheckedChanged += new System.EventHandler(this.radioButtonFirewall_CheckedChanged);
            // 
            // radioButtonWall
            // 
            this.radioButtonWall.AutoSize = true;
            this.radioButtonWall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonWall.Location = new System.Drawing.Point(12, 841);
            this.radioButtonWall.Name = "radioButtonWall";
            this.radioButtonWall.Size = new System.Drawing.Size(53, 20);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 918);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1446, 1079);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonWall);
            this.Controls.Add(this.radioButtonFirewall);
            this.Controls.Add(this.radioButtonShielding);
            this.Controls.Add(this.radioButtonCamera);
            this.Controls.Add(this.pictureBoxMain);
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
        private System.Windows.Forms.Label label1;
    }
}