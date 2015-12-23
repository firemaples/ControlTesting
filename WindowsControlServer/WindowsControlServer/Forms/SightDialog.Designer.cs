namespace WindowsControlServer.Forms
{
    partial class SightDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SightDialog));
            this.pic_sight = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_sight)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_sight
            // 
            this.pic_sight.Image = ((System.Drawing.Image)(resources.GetObject("pic_sight.Image")));
            this.pic_sight.Location = new System.Drawing.Point(0, 0);
            this.pic_sight.Name = "pic_sight";
            this.pic_sight.Size = new System.Drawing.Size(50, 50);
            this.pic_sight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_sight.TabIndex = 0;
            this.pic_sight.TabStop = false;
            // 
            // SightDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(50, 50);
            this.Controls.Add(this.pic_sight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SightDialog";
            this.Text = "SightDialog";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnFormLoaded);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnFormKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pic_sight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_sight;
    }
}