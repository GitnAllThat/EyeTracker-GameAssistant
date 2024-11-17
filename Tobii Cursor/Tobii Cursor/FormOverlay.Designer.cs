namespace Tobii_Cursor
{
    partial class FormOverlay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOverlay));
            this.imgCursor = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgCursor)).BeginInit();
            this.SuspendLayout();
            // 
            // imgCursor
            // 
            this.imgCursor.Image = ((System.Drawing.Image)(resources.GetObject("imgCursor.Image")));
            this.imgCursor.InitialImage = null;
            this.imgCursor.Location = new System.Drawing.Point(106, 110);
            this.imgCursor.Name = "imgCursor";
            this.imgCursor.Size = new System.Drawing.Size(33, 33);
            this.imgCursor.TabIndex = 0;
            this.imgCursor.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 450);
            this.Controls.Add(this.imgCursor);
            this.Name = "FormOverlay";
            this.Text = "Tobii Overlay";
            this.Load += new System.EventHandler(this.FormOverlay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCursor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgCursor;
        private System.Windows.Forms.Timer timer1;
    }
}