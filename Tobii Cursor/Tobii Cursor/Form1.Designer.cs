namespace Tobii_Cursor
{
    partial class Form1
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
                timer.Stop();
                pipeWriter.Close();
                pipeServer.Close();
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
            this.chkOverlay = new System.Windows.Forms.CheckBox();
            this.tmrHandleDetectedInput = new System.Windows.Forms.Timer(this.components);
            this.lblModifier = new System.Windows.Forms.Label();
            this.lblKeybind = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkSmoothing = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkOverlay
            // 
            this.chkOverlay.AutoSize = true;
            this.chkOverlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOverlay.Location = new System.Drawing.Point(24, 35);
            this.chkOverlay.Name = "chkOverlay";
            this.chkOverlay.Size = new System.Drawing.Size(190, 33);
            this.chkOverlay.TabIndex = 4;
            this.chkOverlay.Text = "Overlay On/Off";
            this.chkOverlay.UseVisualStyleBackColor = true;
            this.chkOverlay.CheckedChanged += new System.EventHandler(this.chkOverlay_CheckedChanged);
            // 
            // lblModifier
            // 
            this.lblModifier.AutoSize = true;
            this.lblModifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModifier.Location = new System.Drawing.Point(44, 82);
            this.lblModifier.Name = "lblModifier";
            this.lblModifier.Size = new System.Drawing.Size(64, 16);
            this.lblModifier.TabIndex = 11;
            this.lblModifier.Text = "Modifier";
            // 
            // lblKeybind
            // 
            this.lblKeybind.AutoSize = true;
            this.lblKeybind.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeybind.Location = new System.Drawing.Point(187, 82);
            this.lblKeybind.Name = "lblKeybind";
            this.lblKeybind.Size = new System.Drawing.Size(64, 16);
            this.lblKeybind.TabIndex = 12;
            this.lblKeybind.Text = "Keybind";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(327, 75);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 27);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "+";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // chkSmoothing
            // 
            this.chkSmoothing.AutoSize = true;
            this.chkSmoothing.Checked = true;
            this.chkSmoothing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSmoothing.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.chkSmoothing.Location = new System.Drawing.Point(238, 36);
            this.chkSmoothing.Name = "chkSmoothing";
            this.chkSmoothing.Size = new System.Drawing.Size(224, 33);
            this.chkSmoothing.TabIndex = 14;
            this.chkSmoothing.Text = "Smoothing On/Off";
            this.chkSmoothing.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 394);
            this.Controls.Add(this.chkSmoothing);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.lblKeybind);
            this.Controls.Add(this.lblModifier);
            this.Controls.Add(this.chkOverlay);
            this.Name = "Form1";
            this.Text = "Tobii Eye";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkOverlay;
        private System.Windows.Forms.Timer tmrHandleDetectedInput;
        private System.Windows.Forms.Label lblModifier;
        private System.Windows.Forms.Label lblKeybind;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkSmoothing;
    }
}

