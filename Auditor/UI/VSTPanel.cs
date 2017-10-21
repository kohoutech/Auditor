/* ----------------------------------------------------------------------------
Auditor : an audio plugin host
Copyright (C) 2005-2017  George E Greaney

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
----------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using AuditorA;
using AuditorA.Engine;

namespace AuditorA.UI
{
    public class VSTPanel : UserControl
    {
        public VSTRack rack;                //container
        public AuditorWindow auditwin;      //container's container
        public int plugNum;
        public String plugPath;
        public String fileName;
        public String plugName;

        public VSTPlugin plugin;
        
        public const int PANELHEIGHT = 75;
        public const int PANELWIDTH = 400;

        private Label lblPanelLetter;
        private Label lblPlugName;
        private ComboBox cbxProgList;
        private Button btnPrevProg;
        private Button btnNextProg;
        private Button btnPlugClose;
        private Button btnPlugInfo;
        private Button btnPlugParam;
        private Button btnPlugEditor;
        private ToolTip paneltoolTip;
        private System.ComponentModel.IContainer components;
        
        public String panelLetters = "ABCD";
        public Brush darkLED;
        public Brush brightLED;

        public bool isCurrent;

        PluginInfoWnd pluginInfoWnd;
        ParamEditor paramEditorWnd;
        Form editorWindow;
        Size editorWindowSize;

        //cons
        public VSTPanel(VSTRack _rack, int _plugNum)
        {
            InitializeComponent();

            rack = _rack;
            auditwin = rack.auditwin;
            plugNum = _plugNum;
            plugName = "plugin " + panelLetters[plugNum];
            plugPath = null;
            fileName = null;
            plugin = null;

            this.Size = new Size(PANELWIDTH, PANELHEIGHT);
            this.lblPanelLetter.Text = "" + panelLetters[plugNum];
            this.darkLED = new SolidBrush(Color.FromArgb(0x40, 0, 0));
            this.brightLED = new SolidBrush(Color.FromArgb(0xff, 0, 0));

            isCurrent = false;

            editorWindow = null;
        }

        public bool loadPlugin(String _plugPath)
        {
            plugPath = _plugPath;
            fileName = Path.GetFileNameWithoutExtension(plugPath);
            plugin = new VSTPlugin(this, auditwin.auditorA, plugNum, plugPath);
            bool result = plugin.load();
            if (result)
            {
                plugName = (plugin.name.Length > 0) ? plugin.name : fileName;
                //if (plugNum == 2) plugName = "Another VST";
                //if (plugNum == 3) plugName = "Yet Another VST";
                lblPlugName.Text = plugName;
                editorWindowSize = new Size(plugin.editorWidth, plugin.editorHeight);
                cbxProgList.DataSource = plugin.programs;
            }
            return result;
        }

        public void shutDownPlugin()
        {
            if (editorWindow != null) editorWindow.Close();
        }

        public void setCurrentPlugin() 
        {
            plugin.setCurrent();
            isCurrent = true;
            Invalidate();
        }

        public void clearCurrentPlugin()
        {
            isCurrent = false;
            Invalidate();
        }

//- painting ------------------------------------------------------------------

        void drawRackScrew(Graphics g, int xpos, int ypos) 
        {
            g.DrawEllipse(Pens.Black, xpos, ypos, 10, 10);
            g.FillEllipse(Brushes.Gray, xpos, ypos, 10, 10);
            Pen slotPen = new Pen(Color.Black, 2);
            g.DrawLine(slotPen, xpos + 5, ypos, xpos + 5, ypos + 10);
            g.DrawLine(slotPen, xpos, ypos+5, xpos + 10, ypos+5);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //beveled edge
            g.DrawLine(Pens.SteelBlue, 1, PANELHEIGHT - 3, PANELWIDTH - 1, PANELHEIGHT - 3);        //bottom
            g.DrawLine(Pens.SteelBlue, PANELWIDTH - 3, 1, PANELWIDTH - 3, PANELHEIGHT - 1);           //right

            //rack screws
            drawRackScrew(g, 5, 10);
            drawRackScrew(g, 5, 55);
            drawRackScrew(g, 385, 10);
            drawRackScrew(g, 385, 55);            
            
            //running LED
            g.FillEllipse((isCurrent ? brightLED : darkLED), 34, 12, 16, 16);
            g.DrawEllipse(Pens.White, 34, 12, 16, 16);
            
        }

//- design time code ----------------------------------------------------------

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblPanelLetter = new System.Windows.Forms.Label();
            this.lblPlugName = new System.Windows.Forms.Label();
            this.cbxProgList = new System.Windows.Forms.ComboBox();
            this.btnPrevProg = new System.Windows.Forms.Button();
            this.btnNextProg = new System.Windows.Forms.Button();
            this.btnPlugClose = new System.Windows.Forms.Button();
            this.btnPlugInfo = new System.Windows.Forms.Button();
            this.btnPlugParam = new System.Windows.Forms.Button();
            this.btnPlugEditor = new System.Windows.Forms.Button();
            this.paneltoolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblPanelLetter
            // 
            this.lblPanelLetter.AutoSize = true;
            this.lblPanelLetter.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPanelLetter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPanelLetter.Location = new System.Drawing.Point(2, 27);
            this.lblPanelLetter.Name = "lblPanelLetter";
            this.lblPanelLetter.Size = new System.Drawing.Size(19, 19);
            this.lblPanelLetter.TabIndex = 0;
            this.lblPanelLetter.Text = "A";
            this.lblPanelLetter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlugName
            // 
            this.lblPlugName.AutoSize = true;
            this.lblPlugName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlugName.Location = new System.Drawing.Point(50, 10);
            this.lblPlugName.Name = "lblPlugName";
            this.lblPlugName.Size = new System.Drawing.Size(57, 19);
            this.lblPlugName.TabIndex = 1;
            this.lblPlugName.Text = "not loaded";
            this.lblPlugName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxProgList
            // 
            this.cbxProgList.FormattingEnabled = true;
            this.cbxProgList.Location = new System.Drawing.Point(54, 40);
            this.cbxProgList.Name = "cbxProgList";
            this.cbxProgList.Size = new System.Drawing.Size(172, 21);
            this.cbxProgList.TabIndex = 2;
            this.cbxProgList.SelectedIndexChanged += new System.EventHandler(this.cbxProgList_SelectedIndexChanged);
            // 
            // btnPrevProg
            // 
            this.btnPrevProg.Location = new System.Drawing.Point(30, 38);
            this.btnPrevProg.Name = "btnPrevProg";
            this.btnPrevProg.Size = new System.Drawing.Size(24, 24);
            this.btnPrevProg.TabIndex = 1;
            this.btnPrevProg.Text = "<";
            this.paneltoolTip.SetToolTip(this.btnPrevProg, "previous program");
            this.btnPrevProg.UseVisualStyleBackColor = true;
            this.btnPrevProg.Click += new System.EventHandler(this.btnPrevProg_Click);
            // 
            // btnNextProg
            // 
            this.btnNextProg.Location = new System.Drawing.Point(226, 38);
            this.btnNextProg.Name = "btnNextProg";
            this.btnNextProg.Size = new System.Drawing.Size(24, 24);
            this.btnNextProg.TabIndex = 3;
            this.btnNextProg.Text = ">";
            this.paneltoolTip.SetToolTip(this.btnNextProg, "next program");
            this.btnNextProg.UseVisualStyleBackColor = true;
            this.btnNextProg.Click += new System.EventHandler(this.btnNextProg_Click);
            // 
            // btnPlugClose
            // 
            this.btnPlugClose.BackColor = System.Drawing.Color.Red;
            this.btnPlugClose.Location = new System.Drawing.Point(349, 10);
            this.btnPlugClose.Name = "btnPlugClose";
            this.btnPlugClose.Size = new System.Drawing.Size(24, 24);
            this.btnPlugClose.TabIndex = 8;
            this.btnPlugClose.Text = "X";
            this.paneltoolTip.SetToolTip(this.btnPlugClose, "close plugin");
            this.btnPlugClose.UseVisualStyleBackColor = false;
            this.btnPlugClose.Click += new System.EventHandler(this.btnPlugClose_Click);
            // 
            // btnPlugInfo
            // 
            this.btnPlugInfo.Location = new System.Drawing.Point(264, 38);
            this.btnPlugInfo.Name = "btnPlugInfo";
            this.btnPlugInfo.Size = new System.Drawing.Size(24, 24);
            this.btnPlugInfo.TabIndex = 4;
            this.btnPlugInfo.Text = "I";
            this.paneltoolTip.SetToolTip(this.btnPlugInfo, "show plugin info");
            this.btnPlugInfo.UseVisualStyleBackColor = true;
            this.btnPlugInfo.Click += new System.EventHandler(this.btnPlugInfo_Click);
            // 
            // btnPlugParam
            // 
            this.btnPlugParam.Location = new System.Drawing.Point(287, 38);
            this.btnPlugParam.Name = "btnPlugParam";
            this.btnPlugParam.Size = new System.Drawing.Size(24, 24);
            this.btnPlugParam.TabIndex = 5;
            this.btnPlugParam.Text = "P";
            this.paneltoolTip.SetToolTip(this.btnPlugParam, "edit plugin parameters");
            this.btnPlugParam.UseVisualStyleBackColor = true;
            this.btnPlugParam.Click += new System.EventHandler(this.btnPlugParam_Click);
            // 
            // btnPlugEditor
            // 
            this.btnPlugEditor.Location = new System.Drawing.Point(310, 38);
            this.btnPlugEditor.Name = "btnPlugEditor";
            this.btnPlugEditor.Size = new System.Drawing.Size(24, 24);
            this.btnPlugEditor.TabIndex = 7;
            this.btnPlugEditor.Text = "E";
            this.paneltoolTip.SetToolTip(this.btnPlugEditor, "show plugin editor");
            this.btnPlugEditor.UseVisualStyleBackColor = true;
            this.btnPlugEditor.Click += new System.EventHandler(this.btnPlugEditor_Click);
            // 
            // VSTPanel
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnPlugEditor);
            this.Controls.Add(this.btnPlugParam);
            this.Controls.Add(this.btnPlugInfo);
            this.Controls.Add(this.btnPlugClose);
            this.Controls.Add(this.btnNextProg);
            this.Controls.Add(this.btnPrevProg);
            this.Controls.Add(this.cbxProgList);
            this.Controls.Add(this.lblPlugName);
            this.Controls.Add(this.lblPanelLetter);
            this.Name = "VSTPanel";
            this.Size = new System.Drawing.Size(398, 73);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

//- event handlers ------------------------------------------------------------

        private void btnPrevProg_Click(object sender, EventArgs e)
        {
            int curprog = cbxProgList.SelectedIndex;
            curprog--;
            if (curprog >= 0)
            {
                cbxProgList.SelectedIndex = curprog;
            }
        }

        private void cbxProgList_SelectedIndexChanged(object sender, EventArgs e)
        {
            plugin.setProgram(cbxProgList.SelectedIndex);
        }

        private void btnNextProg_Click(object sender, EventArgs e)
        {
            int curprog = cbxProgList.SelectedIndex;
            curprog++;
            if (curprog < plugin.programs.Length)
            {
                cbxProgList.SelectedIndex = curprog;
            }
        }

//- plugin info & param windows -----------------------------------------------

        private void btnPlugInfo_Click(object sender, EventArgs e)
        {
            pluginInfoWnd = new PluginInfoWnd(this);
            pluginInfoWnd.Text = plugName + " info";
            pluginInfoWnd.Show(auditwin);
        }

        private void btnPlugParam_Click(object sender, EventArgs e)
        {
            paramEditorWnd = new ParamEditor(this);
            paramEditorWnd.Text = plugName + " parameters";
            paramEditorWnd.Show(auditwin);
        }

//- plugin editor window ------------------------------------------------------

        public void showEditorWindow()
        {
            btnPlugEditor.Enabled = false;
            editorWindow = new Form();
            //editorWindow.Owner = auditwin;
            editorWindow.ClientSize = editorWindowSize;
            editorWindow.Text = plugName + " editor";
            editorWindow.Icon = auditwin.Icon;
            editorWindow.ShowInTaskbar = false;
            editorWindow.MaximizeBox = false;
            editorWindow.FormClosing += new FormClosingEventHandler(editorWindow_FormClosing);
            editorWindow.Show(auditwin);
            plugin.openEditorWindow(editorWindow.Handle);
        }

        private void btnPlugEditor_Click(object sender, EventArgs e)
        {
            showEditorWindow();
        }

        private void editorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnPlugEditor.Enabled = true;
            plugin.closeEditorWindow();
        }

//-----------------------------------------------------------------------------

        private void btnPlugClose_Click(object sender, EventArgs e)
        {
            auditwin.unloadPlugin(plugNum);
            plugin.unload();
        }
    }
}
