namespace AuditorA
{
    partial class AuditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditorWindow));
            this.AuditMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPluginAMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPluginBMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPluginCMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPluginDMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PluginMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PluginMenuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.infoPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paramPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startEngineHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopEngineHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HostMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.keyboardHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panicHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devicesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waveDevicesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.midiDevicesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AuditToolBar = new System.Windows.Forms.ToolStrip();
            this.plugAToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.plugBToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.plugCToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.plugDToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.startEngineToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stopEngineToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.keyboardToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.panicToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.AuditStatus = new System.Windows.Forms.StatusStrip();
            this.engineStatusItem = new System.Windows.Forms.ToolStripStatusLabel();
            this.loadPluginDialog = new System.Windows.Forms.OpenFileDialog();
            this.AuditMenu.SuspendLayout();
            this.AuditToolBar.SuspendLayout();
            this.AuditStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // AuditMenu
            // 
            this.AuditMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.pluginMenuItem,
            this.hostMenuItem,
            this.devicesMenuItem,
            this.helpMenuItem});
            this.AuditMenu.Location = new System.Drawing.Point(0, 0);
            this.AuditMenu.Name = "AuditMenu";
            this.AuditMenu.Size = new System.Drawing.Size(284, 24);
            this.AuditMenu.TabIndex = 0;
            this.AuditMenu.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitFileMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // exitFileMenuItem
            // 
            this.exitFileMenuItem.Name = "exitFileMenuItem";
            this.exitFileMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitFileMenuItem.Text = "E&xit";
            this.exitFileMenuItem.Click += new System.EventHandler(this.exitFileMenuItem_Click);
            // 
            // pluginMenuItem
            // 
            this.pluginMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPluginAMenuItem,
            this.loadPluginBMenuItem,
            this.loadPluginCMenuItem,
            this.loadPluginDMenuItem,
            this.PluginMenuSeparator1,
            this.aPluginMenuItem,
            this.bPluginMenuItem,
            this.cPluginMenuItem,
            this.dPluginMenuItem,
            this.PluginMenuSeparator2,
            this.infoPluginMenuItem,
            this.paramPluginMenuItem,
            this.editorPluginMenuItem});
            this.pluginMenuItem.Name = "pluginMenuItem";
            this.pluginMenuItem.Size = new System.Drawing.Size(53, 20);
            this.pluginMenuItem.Text = "&Plugin";
            // 
            // loadPluginAMenuItem
            // 
            this.loadPluginAMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadPluginAMenuItem.Name = "loadPluginAMenuItem";
            this.loadPluginAMenuItem.Size = new System.Drawing.Size(217, 22);
            this.loadPluginAMenuItem.Text = "Load Plugin A";
            this.loadPluginAMenuItem.Click += new System.EventHandler(this.loadPluginAMenuItem_Click);
            // 
            // loadPluginBMenuItem
            // 
            this.loadPluginBMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadPluginBMenuItem.Name = "loadPluginBMenuItem";
            this.loadPluginBMenuItem.Size = new System.Drawing.Size(217, 22);
            this.loadPluginBMenuItem.Text = "Load Plugin B";
            this.loadPluginBMenuItem.Click += new System.EventHandler(this.loadPluginBMenuItem_Click);
            // 
            // loadPluginCMenuItem
            // 
            this.loadPluginCMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadPluginCMenuItem.Name = "loadPluginCMenuItem";
            this.loadPluginCMenuItem.Size = new System.Drawing.Size(217, 22);
            this.loadPluginCMenuItem.Text = "Load Plugin C";
            this.loadPluginCMenuItem.Click += new System.EventHandler(this.loadPluginCMenuItem_Click);
            // 
            // loadPluginDMenuItem
            // 
            this.loadPluginDMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadPluginDMenuItem.Name = "loadPluginDMenuItem";
            this.loadPluginDMenuItem.Size = new System.Drawing.Size(217, 22);
            this.loadPluginDMenuItem.Text = "Load Plugin D";
            this.loadPluginDMenuItem.Click += new System.EventHandler(this.loadPluginDMenuItem_Click);
            // 
            // PluginMenuSeparator1
            // 
            this.PluginMenuSeparator1.Name = "PluginMenuSeparator1";
            this.PluginMenuSeparator1.Size = new System.Drawing.Size(214, 6);
            // 
            // aPluginMenuItem
            // 
            this.aPluginMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aPluginMenuItem.Enabled = false;
            this.aPluginMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aPluginMenuItem.Image")));
            this.aPluginMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aPluginMenuItem.Name = "aPluginMenuItem";
            this.aPluginMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.aPluginMenuItem.Size = new System.Drawing.Size(217, 22);
            this.aPluginMenuItem.Text = "Select Plugin &A";
            this.aPluginMenuItem.Click += new System.EventHandler(this.aPluginMenuItem_Click);
            // 
            // bPluginMenuItem
            // 
            this.bPluginMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bPluginMenuItem.Enabled = false;
            this.bPluginMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bPluginMenuItem.Image")));
            this.bPluginMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bPluginMenuItem.Name = "bPluginMenuItem";
            this.bPluginMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.bPluginMenuItem.Size = new System.Drawing.Size(217, 22);
            this.bPluginMenuItem.Text = "Select Plugin &B";
            this.bPluginMenuItem.Click += new System.EventHandler(this.bPluginMenuItem_Click);
            // 
            // cPluginMenuItem
            // 
            this.cPluginMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cPluginMenuItem.Enabled = false;
            this.cPluginMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cPluginMenuItem.Image")));
            this.cPluginMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cPluginMenuItem.Name = "cPluginMenuItem";
            this.cPluginMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.cPluginMenuItem.Size = new System.Drawing.Size(217, 22);
            this.cPluginMenuItem.Text = "Select Plugin &C";
            this.cPluginMenuItem.Click += new System.EventHandler(this.cPluginMenuItem_Click);
            // 
            // dPluginMenuItem
            // 
            this.dPluginMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dPluginMenuItem.Enabled = false;
            this.dPluginMenuItem.Name = "dPluginMenuItem";
            this.dPluginMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.dPluginMenuItem.Size = new System.Drawing.Size(217, 22);
            this.dPluginMenuItem.Text = "Select Plugin &D";
            this.dPluginMenuItem.Click += new System.EventHandler(this.dPluginMenuItem_Click);
            // 
            // PluginMenuSeparator2
            // 
            this.PluginMenuSeparator2.Name = "PluginMenuSeparator2";
            this.PluginMenuSeparator2.Size = new System.Drawing.Size(214, 6);
            // 
            // infoPluginMenuItem
            // 
            this.infoPluginMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.infoPluginMenuItem.Enabled = false;
            this.infoPluginMenuItem.Name = "infoPluginMenuItem";
            this.infoPluginMenuItem.Size = new System.Drawing.Size(217, 22);
            this.infoPluginMenuItem.Text = "Selected Plugin &Info";
            this.infoPluginMenuItem.Click += new System.EventHandler(this.infoPluginMenuItem_Click);
            // 
            // paramPluginMenuItem
            // 
            this.paramPluginMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.paramPluginMenuItem.Enabled = false;
            this.paramPluginMenuItem.Name = "paramPluginMenuItem";
            this.paramPluginMenuItem.Size = new System.Drawing.Size(217, 22);
            this.paramPluginMenuItem.Text = "Selected Plugin Paramaters";
            this.paramPluginMenuItem.Click += new System.EventHandler(this.paramPluginMenuItem_Click);
            // 
            // editorPluginMenuItem
            // 
            this.editorPluginMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editorPluginMenuItem.Enabled = false;
            this.editorPluginMenuItem.Name = "editorPluginMenuItem";
            this.editorPluginMenuItem.Size = new System.Drawing.Size(217, 22);
            this.editorPluginMenuItem.Text = "Selected Plugin &Editor";
            this.editorPluginMenuItem.Click += new System.EventHandler(this.editorPluginMenuItem_Click);
            // 
            // hostMenuItem
            // 
            this.hostMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startEngineHostMenuItem,
            this.stopEngineHostMenuItem,
            this.HostMenuSeparator1,
            this.keyboardHostMenuItem,
            this.panicHostMenuItem});
            this.hostMenuItem.Name = "hostMenuItem";
            this.hostMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hostMenuItem.Text = "&Host";
            // 
            // startEngineHostMenuItem
            // 
            this.startEngineHostMenuItem.Name = "startEngineHostMenuItem";
            this.startEngineHostMenuItem.Size = new System.Drawing.Size(182, 22);
            this.startEngineHostMenuItem.Text = "&Start Engine";
            this.startEngineHostMenuItem.Click += new System.EventHandler(this.startEngineHostMenuItem_Click);
            // 
            // stopEngineHostMenuItem
            // 
            this.stopEngineHostMenuItem.Enabled = false;
            this.stopEngineHostMenuItem.Name = "stopEngineHostMenuItem";
            this.stopEngineHostMenuItem.Size = new System.Drawing.Size(182, 22);
            this.stopEngineHostMenuItem.Text = "Sto&p Engine";
            this.stopEngineHostMenuItem.Click += new System.EventHandler(this.stopEngineHostMenuItem_Click);
            // 
            // HostMenuSeparator1
            // 
            this.HostMenuSeparator1.Name = "HostMenuSeparator1";
            this.HostMenuSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // keyboardHostMenuItem
            // 
            this.keyboardHostMenuItem.Name = "keyboardHostMenuItem";
            this.keyboardHostMenuItem.Size = new System.Drawing.Size(182, 22);
            this.keyboardHostMenuItem.Text = "&Keyboard Bar";
            this.keyboardHostMenuItem.Click += new System.EventHandler(this.keyboardHostMenuItem_Click);
            // 
            // panicHostMenuItem
            // 
            this.panicHostMenuItem.Enabled = false;
            this.panicHostMenuItem.Name = "panicHostMenuItem";
            this.panicHostMenuItem.Size = new System.Drawing.Size(182, 22);
            this.panicHostMenuItem.Text = "Panic (All Notes Off)";
            this.panicHostMenuItem.Click += new System.EventHandler(this.panicHostMenuItem_Click);
            // 
            // devicesMenuItem
            // 
            this.devicesMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.waveDevicesMenuItem,
            this.midiDevicesMenuItem});
            this.devicesMenuItem.Name = "devicesMenuItem";
            this.devicesMenuItem.Size = new System.Drawing.Size(59, 20);
            this.devicesMenuItem.Text = "Devices";
            // 
            // waveDevicesMenuItem
            // 
            this.waveDevicesMenuItem.Name = "waveDevicesMenuItem";
            this.waveDevicesMenuItem.Size = new System.Drawing.Size(112, 22);
            this.waveDevicesMenuItem.Text = "&Wave...";
            this.waveDevicesMenuItem.Click += new System.EventHandler(this.waveDevicesMenuItem_Click);
            // 
            // midiDevicesMenuItem
            // 
            this.midiDevicesMenuItem.Name = "midiDevicesMenuItem";
            this.midiDevicesMenuItem.Size = new System.Drawing.Size(112, 22);
            this.midiDevicesMenuItem.Text = "&MIDI...";
            this.midiDevicesMenuItem.Click += new System.EventHandler(this.midiDevicesMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutHelpMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // aboutHelpMenuItem
            // 
            this.aboutHelpMenuItem.Name = "aboutHelpMenuItem";
            this.aboutHelpMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutHelpMenuItem.Text = "&About...";
            this.aboutHelpMenuItem.Click += new System.EventHandler(this.aboutHelpMenuItem_Click);
            // 
            // AuditToolBar
            // 
            this.AuditToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plugAToolStripButton,
            this.plugBToolStripButton,
            this.plugCToolStripButton,
            this.plugDToolStripButton,
            this.toolStripSeparator6,
            this.startEngineToolStripButton,
            this.stopEngineToolStripButton,
            this.toolStripSeparator7,
            this.keyboardToolStripButton,
            this.panicToolStripButton});
            this.AuditToolBar.Location = new System.Drawing.Point(0, 24);
            this.AuditToolBar.Name = "AuditToolBar";
            this.AuditToolBar.Size = new System.Drawing.Size(284, 25);
            this.AuditToolBar.TabIndex = 1;
            this.AuditToolBar.Text = "AuditToolbar";
            // 
            // plugAToolStripButton
            // 
            this.plugAToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.plugAToolStripButton.Enabled = false;
            this.plugAToolStripButton.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plugAToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.plugAToolStripButton.Name = "plugAToolStripButton";
            this.plugAToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.plugAToolStripButton.Text = "&A";
            this.plugAToolStripButton.ToolTipText = "select plugin A";
            this.plugAToolStripButton.Click += new System.EventHandler(this.aPluginMenuItem_Click);
            // 
            // plugBToolStripButton
            // 
            this.plugBToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.plugBToolStripButton.Enabled = false;
            this.plugBToolStripButton.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plugBToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("plugBToolStripButton.Image")));
            this.plugBToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.plugBToolStripButton.Name = "plugBToolStripButton";
            this.plugBToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.plugBToolStripButton.Text = "&B";
            this.plugBToolStripButton.ToolTipText = "select plugin B";
            this.plugBToolStripButton.Click += new System.EventHandler(this.bPluginMenuItem_Click);
            // 
            // plugCToolStripButton
            // 
            this.plugCToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.plugCToolStripButton.Enabled = false;
            this.plugCToolStripButton.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plugCToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("plugCToolStripButton.Image")));
            this.plugCToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.plugCToolStripButton.Name = "plugCToolStripButton";
            this.plugCToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.plugCToolStripButton.Text = "&C";
            this.plugCToolStripButton.ToolTipText = "select plugin C";
            this.plugCToolStripButton.Click += new System.EventHandler(this.cPluginMenuItem_Click);
            // 
            // plugDToolStripButton
            // 
            this.plugDToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.plugDToolStripButton.Enabled = false;
            this.plugDToolStripButton.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plugDToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("plugDToolStripButton.Image")));
            this.plugDToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.plugDToolStripButton.Name = "plugDToolStripButton";
            this.plugDToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.plugDToolStripButton.Text = "&D";
            this.plugDToolStripButton.ToolTipText = "select plugin D";
            this.plugDToolStripButton.Click += new System.EventHandler(this.dPluginMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // startEngineToolStripButton
            // 
            this.startEngineToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startEngineToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("startEngineToolStripButton.Image")));
            this.startEngineToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startEngineToolStripButton.Name = "startEngineToolStripButton";
            this.startEngineToolStripButton.Size = new System.Drawing.Size(35, 22);
            this.startEngineToolStripButton.Text = "&Start";
            this.startEngineToolStripButton.Click += new System.EventHandler(this.startEngineHostMenuItem_Click);
            // 
            // stopEngineToolStripButton
            // 
            this.stopEngineToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopEngineToolStripButton.Enabled = false;
            this.stopEngineToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("stopEngineToolStripButton.Image")));
            this.stopEngineToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopEngineToolStripButton.Name = "stopEngineToolStripButton";
            this.stopEngineToolStripButton.Size = new System.Drawing.Size(35, 22);
            this.stopEngineToolStripButton.Text = "St&op";
            this.stopEngineToolStripButton.Click += new System.EventHandler(this.stopEngineHostMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // keyboardToolStripButton
            // 
            this.keyboardToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.keyboardToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("keyboardToolStripButton.Image")));
            this.keyboardToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.keyboardToolStripButton.Name = "keyboardToolStripButton";
            this.keyboardToolStripButton.Size = new System.Drawing.Size(35, 22);
            this.keyboardToolStripButton.Text = "&Keys";
            this.keyboardToolStripButton.Click += new System.EventHandler(this.keyboardHostMenuItem_Click);
            // 
            // panicToolStripButton
            // 
            this.panicToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.panicToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("panicToolStripButton.Image")));
            this.panicToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.panicToolStripButton.Name = "panicToolStripButton";
            this.panicToolStripButton.Size = new System.Drawing.Size(40, 22);
            this.panicToolStripButton.Text = "&Panic";
            this.panicToolStripButton.Click += new System.EventHandler(this.panicHostMenuItem_Click);
            // 
            // AuditStatus
            // 
            this.AuditStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.engineStatusItem});
            this.AuditStatus.Location = new System.Drawing.Point(0, 239);
            this.AuditStatus.Name = "AuditStatus";
            this.AuditStatus.Size = new System.Drawing.Size(284, 22);
            this.AuditStatus.TabIndex = 2;
            this.AuditStatus.Text = "AuditStatus";
            // 
            // engineStatusItem
            // 
            this.engineStatusItem.Name = "engineStatusItem";
            this.engineStatusItem.Size = new System.Drawing.Size(120, 17);
            this.engineStatusItem.Text = "the engine is stopped";
            // 
            // AuditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.AuditStatus);
            this.Controls.Add(this.AuditToolBar);
            this.Controls.Add(this.AuditMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.AuditMenu;
            this.MaximizeBox = false;
            this.Name = "AuditorWindow";
            this.Text = "Auditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuditorWindow_FormClosing);
            this.AuditMenu.ResumeLayout(false);
            this.AuditMenu.PerformLayout();
            this.AuditToolBar.ResumeLayout(false);
            this.AuditToolBar.PerformLayout();
            this.AuditStatus.ResumeLayout(false);
            this.AuditStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip AuditMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPluginAMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPluginCMenuItem;
        private System.Windows.Forms.ToolStripSeparator PluginMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cPluginMenuItem;
        private System.Windows.Forms.ToolStripSeparator PluginMenuSeparator2;
        private System.Windows.Forms.ToolStripMenuItem infoPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startEngineHostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopEngineHostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHelpMenuItem;
        private System.Windows.Forms.ToolStrip AuditToolBar;
        private System.Windows.Forms.StatusStrip AuditStatus;
        private System.Windows.Forms.ToolStripButton plugAToolStripButton;
        private System.Windows.Forms.ToolStripButton plugBToolStripButton;
        private System.Windows.Forms.ToolStripButton plugCToolStripButton;
        private System.Windows.Forms.ToolStripButton plugDToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton keyboardToolStripButton;
        private System.Windows.Forms.ToolStripButton startEngineToolStripButton;
        private System.Windows.Forms.ToolStripButton stopEngineToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton panicToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem devicesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waveDevicesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem midiDevicesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paramPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPluginBMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPluginDMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyboardHostMenuItem;
        private System.Windows.Forms.ToolStripSeparator HostMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem panicHostMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel engineStatusItem;
        private System.Windows.Forms.OpenFileDialog loadPluginDialog;
    }
}

