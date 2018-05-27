namespace FriendlyEyeWatcher.Forms
{
    partial class FormOverview
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
            this.outlineLabelStatus = new FriendlyEyeWatcher.CustomControls.OutlineLabel();
            this.outlineLabelVotesYes = new FriendlyEyeWatcher.CustomControls.OutlineLabel();
            this.outlineLabelVotesNo = new FriendlyEyeWatcher.CustomControls.OutlineLabel();
            this.SuspendLayout();
            // 
            // outlineLabelStatus
            // 
            this.outlineLabelStatus.AutoSize = true;
            this.outlineLabelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outlineLabelStatus.ForeColor = System.Drawing.Color.White;
            this.outlineLabelStatus.Location = new System.Drawing.Point(24, 29);
            this.outlineLabelStatus.Name = "outlineLabelStatus";
            this.outlineLabelStatus.OutlineForeColor = System.Drawing.Color.Black;
            this.outlineLabelStatus.OutlineWidth = 2F;
            this.outlineLabelStatus.Size = new System.Drawing.Size(903, 120);
            this.outlineLabelStatus.TabIndex = 7;
            this.outlineLabelStatus.Text = "Thanks for voting!";
            // 
            // outlineLabelVotesYes
            // 
            this.outlineLabelVotesYes.AutoSize = true;
            this.outlineLabelVotesYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outlineLabelVotesYes.ForeColor = System.Drawing.Color.LawnGreen;
            this.outlineLabelVotesYes.Location = new System.Drawing.Point(369, 247);
            this.outlineLabelVotesYes.Name = "outlineLabelVotesYes";
            this.outlineLabelVotesYes.OutlineForeColor = System.Drawing.Color.Black;
            this.outlineLabelVotesYes.OutlineWidth = 2F;
            this.outlineLabelVotesYes.Size = new System.Drawing.Size(937, 120);
            this.outlineLabelVotesYes.TabIndex = 8;
            this.outlineLabelVotesYes.Text = "people voted YES:";
            // 
            // outlineLabelVotesNo
            // 
            this.outlineLabelVotesNo.AutoSize = true;
            this.outlineLabelVotesNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outlineLabelVotesNo.ForeColor = System.Drawing.Color.Red;
            this.outlineLabelVotesNo.Location = new System.Drawing.Point(369, 415);
            this.outlineLabelVotesNo.Name = "outlineLabelVotesNo";
            this.outlineLabelVotesNo.OutlineForeColor = System.Drawing.Color.Black;
            this.outlineLabelVotesNo.OutlineWidth = 2F;
            this.outlineLabelVotesNo.Size = new System.Drawing.Size(884, 120);
            this.outlineLabelVotesNo.TabIndex = 9;
            this.outlineLabelVotesNo.Text = "people voted NO:";
            // 
            // FormOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.outlineLabelVotesNo);
            this.Controls.Add(this.outlineLabelVotesYes);
            this.Controls.Add(this.outlineLabelStatus);
            this.Name = "FormOverview";
            this.Text = "FormOverview";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.OutlineLabel outlineLabelStatus;
        internal CustomControls.OutlineLabel outlineLabelVotesYes;
        internal CustomControls.OutlineLabel outlineLabelVotesNo;
    }
}