namespace Cloudbreaker
{
    partial class ControlWindow
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
            this.buttonLaunchRun = new System.Windows.Forms.Button();
            this.frameTimer = new System.Windows.Forms.Timer(this.components);
            this.buttonLevelEditor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLaunchRun
            // 
            this.buttonLaunchRun.Location = new System.Drawing.Point(317, 12);
            this.buttonLaunchRun.Name = "buttonLaunchRun";
            this.buttonLaunchRun.Size = new System.Drawing.Size(164, 50);
            this.buttonLaunchRun.TabIndex = 0;
            this.buttonLaunchRun.Text = "Launch Run";
            this.buttonLaunchRun.UseVisualStyleBackColor = true;
            this.buttonLaunchRun.Click += new System.EventHandler(this.buttonLaunchRun_Click);
            // 
            // frameTimer
            // 
            this.frameTimer.Enabled = true;
            this.frameTimer.Interval = 33;
            this.frameTimer.Tick += new System.EventHandler(this.frameTimer_Tick);
            // 
            // buttonLevelEditor
            // 
            this.buttonLevelEditor.Location = new System.Drawing.Point(317, 72);
            this.buttonLevelEditor.Name = "buttonLevelEditor";
            this.buttonLevelEditor.Size = new System.Drawing.Size(163, 47);
            this.buttonLevelEditor.TabIndex = 1;
            this.buttonLevelEditor.Text = "Launch Editor";
            this.buttonLevelEditor.UseVisualStyleBackColor = true;
            this.buttonLevelEditor.Click += new System.EventHandler(this.buttonLevelEditor_Click);
            // 
            // ControlWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 337);
            this.Controls.Add(this.buttonLevelEditor);
            this.Controls.Add(this.buttonLaunchRun);
            this.Name = "ControlWindow";
            this.Text = "GM Control Window";
            this.Load += new System.EventHandler(this.ControlWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLaunchRun;
        private System.Windows.Forms.Timer frameTimer;
        private System.Windows.Forms.Button buttonLevelEditor;
    }
}

