namespace Pulse
{
    partial class DecoderWindow
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
            this.pictureBoxDecoder = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecoder)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxDecoder
            // 
            this.pictureBoxDecoder.Location = new System.Drawing.Point(26, 38);
            this.pictureBoxDecoder.Name = "pictureBoxDecoder";
            this.pictureBoxDecoder.Size = new System.Drawing.Size(191, 129);
            this.pictureBoxDecoder.TabIndex = 0;
            this.pictureBoxDecoder.TabStop = false;
            // 
            // DecoderWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBoxDecoder);
            this.Name = "DecoderWindow";
            this.Text = "DecoderWindow";
            this.Load += new System.EventHandler(this.DecoderWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecoder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxDecoder;
    }
}