namespace AuditorA.UI
{
    partial class PluginInfoWnd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginInfoWnd));
            this.lblPlugInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPlugInfo
            // 
            this.lblPlugInfo.AutoSize = true;
            this.lblPlugInfo.Location = new System.Drawing.Point(12, 9);
            this.lblPlugInfo.Name = "lblPlugInfo";
            this.lblPlugInfo.Size = new System.Drawing.Size(39, 13);
            this.lblPlugInfo.TabIndex = 0;
            this.lblPlugInfo.Text = "no info";
            // 
            // PluginInfoWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblPlugInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PluginInfoWnd";
            this.ShowInTaskbar = false;
            this.Text = "Plugin Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPlugInfo;
    }
}