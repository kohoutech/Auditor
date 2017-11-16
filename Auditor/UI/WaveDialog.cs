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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Auditor.UI
{
    class WaveDialog : Form
    {
        private Label lblWaveOut;
        private ComboBox cbxWaveIn;
        private ComboBox cbxWaveOut;
        private Button btnCancel;
        private Button btnOK;
        private Label lblWaveIn;

        public AuditorWindow auditwnd;
        public int waveInDeviceIdx;
        public int waveOutDeviceIdx;

        public WaveDialog(AuditorWindow _auditwnd)
        {
            InitializeComponent();
            auditwnd = _auditwnd;
            List<String> inList = auditwnd.auditorA.waveSystem.getInDevNameList();
            inList.Insert(0, "no input device");
            waveInDeviceIdx = -1;
            cbxWaveIn.DataSource = inList;
            List<String> outList = auditwnd.auditorA.waveSystem.getOutDevNameList();
            outList.Insert(0, "no output device");
            cbxWaveOut.DataSource = outList;
            waveOutDeviceIdx = -1;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveDialog));
            this.lblWaveIn = new System.Windows.Forms.Label();
            this.lblWaveOut = new System.Windows.Forms.Label();
            this.cbxWaveIn = new System.Windows.Forms.ComboBox();
            this.cbxWaveOut = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWaveIn
            // 
            this.lblWaveIn.AutoSize = true;
            this.lblWaveIn.Location = new System.Drawing.Point(12, 18);
            this.lblWaveIn.Name = "lblWaveIn";
            this.lblWaveIn.Size = new System.Drawing.Size(114, 13);
            this.lblWaveIn.TabIndex = 0;
            this.lblWaveIn.Text = "select Wave In device";
            // 
            // lblWaveOut
            // 
            this.lblWaveOut.AutoSize = true;
            this.lblWaveOut.Location = new System.Drawing.Point(12, 86);
            this.lblWaveOut.Name = "lblWaveOut";
            this.lblWaveOut.Size = new System.Drawing.Size(122, 13);
            this.lblWaveOut.TabIndex = 0;
            this.lblWaveOut.Text = "select Wave Out device";
            // 
            // cbxWaveIn
            // 
            this.cbxWaveIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWaveIn.FormattingEnabled = true;
            this.cbxWaveIn.Location = new System.Drawing.Point(12, 44);
            this.cbxWaveIn.Name = "cbxWaveIn";
            this.cbxWaveIn.Size = new System.Drawing.Size(297, 21);
            this.cbxWaveIn.TabIndex = 1;
            this.cbxWaveIn.SelectedIndexChanged += new System.EventHandler(this.cbxWaveIn_SelectedIndexChanged);
            // 
            // cbxWaveOut
            // 
            this.cbxWaveOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWaveOut.FormattingEnabled = true;
            this.cbxWaveOut.Location = new System.Drawing.Point(12, 108);
            this.cbxWaveOut.Name = "cbxWaveOut";
            this.cbxWaveOut.Size = new System.Drawing.Size(297, 21);
            this.cbxWaveOut.TabIndex = 2;
            this.cbxWaveOut.SelectedIndexChanged += new System.EventHandler(this.cbxWaveOut_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(143, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(234, 152);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // WaveDialog
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(321, 194);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbxWaveOut);
            this.Controls.Add(this.cbxWaveIn);
            this.Controls.Add(this.lblWaveOut);
            this.Controls.Add(this.lblWaveIn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaveDialog";
            this.ShowInTaskbar = false;
            this.Text = "Configure Wave Devices";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void cbxWaveIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            waveInDeviceIdx = cbxWaveIn.SelectedIndex - 1;
        }

        private void cbxWaveOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            waveOutDeviceIdx = cbxWaveOut.SelectedIndex - 1;
        }
    }
}
