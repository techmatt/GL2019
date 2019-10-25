namespace Pulse
{
    partial class PulseWindow
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
            this.pictureBoxPulse = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPulse)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPulse
            // 
            this.pictureBoxPulse.Location = new System.Drawing.Point(81, 132);
            this.pictureBoxPulse.Name = "pictureBoxPulse";
            this.pictureBoxPulse.Size = new System.Drawing.Size(198, 155);
            this.pictureBoxPulse.TabIndex = 0;
            this.pictureBoxPulse.TabStop = false;
            // 
            // PulseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 543);
            this.Controls.Add(this.pictureBoxPulse);
            this.Name = "PulseWindow";
            this.Text = "PulseWindow";
            this.Load += new System.EventHandler(this.PulseWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPulse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPulse;
    }
}