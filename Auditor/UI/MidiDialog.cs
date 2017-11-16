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
    class MidiDialog : Form
    {
        private Label lblMidiOut;
        private ComboBox cbxMidiIn;
        private ComboBox cbxMidiOut;
        private Button btnCancel;
        private Button btnOK;
        private Label lblMidiIn;

        public AuditorWindow auditwnd;
        public int midiInDeviceIdx;
        public int midiOutDeviceIdx;

        public MidiDialog(AuditorWindow _auditwnd)
        {
            InitializeComponent();
            auditwnd = _auditwnd;
            List<String> inList = auditwnd.auditorA.midiSystem.getInDevNameList();  
            midiInDeviceIdx = 0;
            cbxMidiIn.DataSource = inList;
            List<String> outList = auditwnd.auditorA.midiSystem.getOutDevNameList();
            cbxMidiOut.DataSource = outList;
            midiOutDeviceIdx = 0;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MidiDialog));
            this.lblMidiIn = new System.Windows.Forms.Label();
            this.lblMidiOut = new System.Windows.Forms.Label();
            this.cbxMidiIn = new System.Windows.Forms.ComboBox();
            this.cbxMidiOut = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMidiIn
            // 
            this.lblMidiIn.AutoSize = true;
            this.lblMidiIn.Location = new System.Drawing.Point(12, 18);
            this.lblMidiIn.Name = "lblMidiIn";
            this.lblMidiIn.Size = new System.Drawing.Size(108, 13);
            this.lblMidiIn.TabIndex = 0;
            this.lblMidiIn.Text = "select MIDI In device";
            // 
            // lblMidiOut
            // 
            this.lblMidiOut.AutoSize = true;
            this.lblMidiOut.Location = new System.Drawing.Point(12, 86);
            this.lblMidiOut.Name = "lblMidiOut";
            this.lblMidiOut.Size = new System.Drawing.Size(116, 13);
            this.lblMidiOut.TabIndex = 0;
            this.lblMidiOut.Text = "select MIDI Out device";
            // 
            // cbxMidiIn
            // 
            this.cbxMidiIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMidiIn.FormattingEnabled = true;
            this.cbxMidiIn.Location = new System.Drawing.Point(12, 44);
            this.cbxMidiIn.Name = "cbxMidiIn";
            this.cbxMidiIn.Size = new System.Drawing.Size(297, 21);
            this.cbxMidiIn.TabIndex = 1;
            this.cbxMidiIn.SelectedIndexChanged += new System.EventHandler(this.cbxWaveIn_SelectedIndexChanged);
            // 
            // cbxMidiOut
            // 
            this.cbxMidiOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMidiOut.FormattingEnabled = true;
            this.cbxMidiOut.Location = new System.Drawing.Point(12, 108);
            this.cbxMidiOut.Name = "cbxMidiOut";
            this.cbxMidiOut.Size = new System.Drawing.Size(297, 21);
            this.cbxMidiOut.TabIndex = 2;
            this.cbxMidiOut.SelectedIndexChanged += new System.EventHandler(this.cbxWaveOut_SelectedIndexChanged);
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
            // MidiDialog
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(321, 194);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbxMidiOut);
            this.Controls.Add(this.cbxMidiIn);
            this.Controls.Add(this.lblMidiOut);
            this.Controls.Add(this.lblMidiIn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MidiDialog";
            this.ShowInTaskbar = false;
            this.Text = "Configure MIDI Devices";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void setMidiInDevice(int deviceIdx)
        {
            cbxMidiIn.SelectedIndex = deviceIdx + 1;
        }

        private void cbxWaveIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            midiInDeviceIdx = cbxMidiIn.SelectedIndex - 1;            
        }

        public void setMidiOutDevice(int deviceIdx)
        {
            cbxMidiOut.SelectedIndex = deviceIdx + 1;
        }

        private void cbxWaveOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            midiOutDeviceIdx = cbxMidiOut.SelectedIndex - 1;
        }
    }
}
