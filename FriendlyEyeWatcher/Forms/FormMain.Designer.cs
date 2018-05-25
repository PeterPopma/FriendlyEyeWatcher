namespace FriendlyEyeWatcher.Forms
{
    partial class FormMain
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
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelCurrentFrameNumber = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDangerous = new System.Windows.Forms.Button();
            this.outlineLabelStatus = new FriendlyEyeWatcher.CustomControls.OutlineLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.panelNavigation.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.BackColor = System.Drawing.Color.Black;
            this.pictureBoxImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(1904, 1041);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage.TabIndex = 1;
            this.pictureBoxImage.TabStop = false;
            // 
            // panelNavigation
            // 
            this.panelNavigation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelNavigation.Controls.Add(this.panel2);
            this.panelNavigation.Controls.Add(this.buttonNext);
            this.panelNavigation.Controls.Add(this.buttonPrevious);
            this.panelNavigation.Location = new System.Drawing.Point(662, 837);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(437, 154);
            this.panelNavigation.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelCurrentFrameNumber);
            this.panel2.Location = new System.Drawing.Point(152, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(130, 94);
            this.panel2.TabIndex = 5;
            // 
            // labelCurrentFrameNumber
            // 
            this.labelCurrentFrameNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCurrentFrameNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 52F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentFrameNumber.ForeColor = System.Drawing.Color.Yellow;
            this.labelCurrentFrameNumber.Location = new System.Drawing.Point(0, 0);
            this.labelCurrentFrameNumber.Name = "labelCurrentFrameNumber";
            this.labelCurrentFrameNumber.Size = new System.Drawing.Size(130, 94);
            this.labelCurrentFrameNumber.TabIndex = 3;
            this.labelCurrentFrameNumber.Text = "XX";
            this.labelCurrentFrameNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonNext
            // 
            this.buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.ForeColor = System.Drawing.Color.Yellow;
            this.buttonNext.Location = new System.Drawing.Point(285, 18);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(127, 113);
            this.buttonNext.TabIndex = 4;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrevious.ForeColor = System.Drawing.Color.Yellow;
            this.buttonPrevious.Location = new System.Drawing.Point(21, 18);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(127, 111);
            this.buttonPrevious.TabIndex = 3;
            this.buttonPrevious.Text = "<";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.LawnGreen;
            this.buttonSave.Location = new System.Drawing.Point(81, 853);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(514, 146);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonDangerous
            // 
            this.buttonDangerous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDangerous.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDangerous.ForeColor = System.Drawing.Color.Red;
            this.buttonDangerous.Location = new System.Drawing.Point(1268, 845);
            this.buttonDangerous.Name = "buttonDangerous";
            this.buttonDangerous.Size = new System.Drawing.Size(587, 146);
            this.buttonDangerous.TabIndex = 5;
            this.buttonDangerous.Text = "Dangerous";
            this.buttonDangerous.UseVisualStyleBackColor = true;
            // 
            // outlineLabelStatus
            // 
            this.outlineLabelStatus.AutoSize = true;
            this.outlineLabelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outlineLabelStatus.ForeColor = System.Drawing.Color.LawnGreen;
            this.outlineLabelStatus.Location = new System.Drawing.Point(32, 27);
            this.outlineLabelStatus.Name = "outlineLabelStatus";
            this.outlineLabelStatus.OutlineForeColor = System.Drawing.Color.Black;
            this.outlineLabelStatus.OutlineWidth = 2F;
            this.outlineLabelStatus.Size = new System.Drawing.Size(1230, 120);
            this.outlineLabelStatus.TabIndex = 6;
            this.outlineLabelStatus.Text = "Everything safe for now..";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.outlineLabelStatus);
            this.Controls.Add(this.buttonDangerous);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.panelNavigation);
            this.Controls.Add(this.pictureBoxImage);
            this.Name = "FormMain";
            this.Text = "FormMain";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.panelNavigation.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDangerous;
        private CustomControls.OutlineLabel outlineLabelStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelCurrentFrameNumber;
    }
}