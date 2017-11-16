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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Auditor.UI;
using Transonic.Wave;
using Transonic.Wave.Engine;
using Transonic.Widget;

namespace Auditor
{
    public partial class AuditorWindow : Form, IWaveView
    {
        public Waverly waverly;
        public Transport transport;
        public VSTRack rack;
        KeyboardBar keyboardBar;

        public bool[] plugLoaded;
        public ToolStripMenuItem[] plugloadItems;
        public ToolStripMenuItem[] plugselectItems;
        public ToolStripButton[] plugselectButtons;

        bool isEngineRunning;
        public int waveInDeviceIdx;
        public int waveOutDeviceIdx;
        public int midiInDeviceIdx;
        public int midiOutDeviceIdx;

        public AuditorWindow()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(VSTPanel.PANELWIDTH,
                VSTPanel.PANELHEIGHT * VSTRack.UNITCOUNT + this.AuditMenu.Height + this.AuditToolBar.Height + this.AuditStatus.Height);

            try
            {
                waverly = new Waverly(this);
                transport = waverly.getTransport();
                rack = new VSTRack(this);
                this.Controls.Add(rack);
                rack.Location = new Point(0, this.AuditMenu.Height + this.AuditToolBar.Height);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                string dum = Console.ReadLine();
            }

            plugLoaded = new bool[VSTRack.UNITCOUNT];
            for (int i = 0; i < VSTRack.UNITCOUNT; i++) plugLoaded[i] = false;

            plugloadItems = new ToolStripMenuItem[VSTRack.UNITCOUNT];
            plugloadItems[0] = loadPluginAMenuItem;
            plugloadItems[1] = loadPluginBMenuItem;
            plugloadItems[2] = loadPluginCMenuItem;
            plugloadItems[3] = loadPluginDMenuItem;
            plugselectItems = new ToolStripMenuItem[VSTRack.UNITCOUNT];
            plugselectItems[0] = aPluginMenuItem;
            plugselectItems[1] = bPluginMenuItem;
            plugselectItems[2] = cPluginMenuItem;
            plugselectItems[3] = dPluginMenuItem;
            plugselectButtons = new ToolStripButton[VSTRack.UNITCOUNT];
            plugselectButtons[0] = plugAToolStripButton;
            plugselectButtons[1] = plugBToolStripButton;
            plugselectButtons[2] = plugCToolStripButton;
            plugselectButtons[3] = plugDToolStripButton;

            isEngineRunning = false;
            waveInDeviceIdx = -1;
            waveOutDeviceIdx = -1;
            midiInDeviceIdx = -1;
            midiOutDeviceIdx = -1;
        }

        //save settings & clean up on shut down
        private void AuditorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            waverly.shutDown();
        }

//- file menu -----------------------------------------------------------------

        private void exitFileMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

//- plugin menu -----------------------------------------------------------------

        String pluginLetters = "ABCD";

        public void loadPlugin(int plugNum)
        {
            String pluginPath = "";
            loadPluginDialog.InitialDirectory = Application.StartupPath;
            loadPluginDialog.ShowDialog();
            pluginPath = loadPluginDialog.FileName;
            if (pluginPath.Length == 0) return;

            bool result = rack.loadPlugin(plugNum, pluginPath);

            if (result)
            {
                plugLoaded[plugNum] = true;
                plugloadItems[plugNum].Text = "Unload Plugin " + pluginLetters[plugNum];
                plugselectItems[plugNum].Enabled = true;
                plugselectButtons[plugNum].Enabled = true;
                setCurrentPlugin(plugNum);
            }
            else
            {
                MessageBox.Show("failed to load the plugin file: " + pluginPath, "Plugin Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void unloadPlugin(int plugNum)
        {
            rack.unloadPlugin(plugNum);

            plugLoaded[plugNum] = false;
            plugloadItems[plugNum].Text = "Load Plugin " + pluginLetters[plugNum];
            plugselectItems[plugNum].Enabled = false;
            plugselectButtons[plugNum].Enabled = false;
            if (rack.currentPlugin >= 0)
            {
                infoPluginMenuItem.Enabled = false;
                paramPluginMenuItem.Enabled = false;
                editorPluginMenuItem.Enabled = false;
            }
        }

        public void loadUnloadPlugin(int plugNum)
        {
            if (plugLoaded[plugNum])
            {
                unloadPlugin(plugNum);
            }
            else
            {
                loadPlugin(plugNum);
            }
        }

        private void loadPluginAMenuItem_Click(object sender, EventArgs e)
        {
            loadUnloadPlugin(0);
        }

        private void loadPluginBMenuItem_Click(object sender, EventArgs e)
        {
            loadUnloadPlugin(1);
        }

        private void loadPluginCMenuItem_Click(object sender, EventArgs e)
        {
            loadUnloadPlugin(2);
        }

        private void loadPluginDMenuItem_Click(object sender, EventArgs e)
        {
            loadUnloadPlugin(3);
        }

        private void setCurrentPlugin(int plugNum)
        {
            rack.selectPlugin(plugNum);
            infoPluginMenuItem.Enabled = true;
            paramPluginMenuItem.Enabled = true;
            editorPluginMenuItem.Enabled = true;
        }

        private void aPluginMenuItem_Click(object sender, EventArgs e)
        {
            setCurrentPlugin(0);
        }

        private void bPluginMenuItem_Click(object sender, EventArgs e)
        {
            setCurrentPlugin(1);
        }

        private void cPluginMenuItem_Click(object sender, EventArgs e)
        {
            setCurrentPlugin(2);
        }

        private void dPluginMenuItem_Click(object sender, EventArgs e)
        {
            setCurrentPlugin(3);
        }

        private void infoPluginMenuItem_Click(object sender, EventArgs e)
        {
            rack.showSelectedPluginInfo();
        }

        private void paramPluginMenuItem_Click(object sender, EventArgs e)
        {
            rack.showSelectedPluginParams();
        }

        private void editorPluginMenuItem_Click(object sender, EventArgs e)
        {
            rack.showSelectedPluginEditor();
        }

//- host menu -----------------------------------------------------------------

        public void showEngineRunning(bool running)
        {
            isEngineRunning = running;
            startEngineHostMenuItem.Enabled = !running;
            startEngineToolStripButton.Enabled = !running;
            stopEngineHostMenuItem.Enabled = running;
            stopEngineToolStripButton.Enabled = running;
            engineStatusItem.Text = isEngineRunning ? "the engine is running" : "the engine is stopped";
        }

        private void startEngineHostMenuItem_Click(object sender, EventArgs e)
        {
            transport.play();
            showEngineRunning(true);
        }

        private void stopEngineHostMenuItem_Click(object sender, EventArgs e)
        {
            transport.stop();
            showEngineRunning(false); 
        }

        public void enableKeyboardBarMenuItem(bool enable) 
        {
            keyboardHostMenuItem.Enabled = enable;
            keyboardToolStripButton.Enabled = enable;
        }

        private void keyboardHostMenuItem_Click(object sender, EventArgs e)
        {
            keyboardBar = new KeyboardBar(this);
            keyboardBar.Show(this);
            enableKeyboardBarMenuItem(false);
        }

        private void panicHostMenuItem_Click(object sender, EventArgs e)
        {
            //not implemented yet
        }

//- devices menu -----------------------------------------------------------------

        private void waveDevicesMenuItem_Click(object sender, EventArgs e)
        {
            WaveDialog waveDialog = new WaveDialog(this);
            waveDialog.ShowDialog();

            bool wasEngineRunning = isEngineRunning;
            if (waveInDeviceIdx != waveDialog.waveInDeviceIdx)
            {
                if (isEngineRunning)
                {
                    transport.stop();
                    isEngineRunning = false;
                }
                waveInDeviceIdx = waveDialog.waveInDeviceIdx;
                waverly.setWaveInDevice(waveOutDeviceIdx);
            }
            if (waveOutDeviceIdx != waveDialog.waveOutDeviceIdx)
            {
                if (isEngineRunning)
                {
                    transport.stop(); 
                    isEngineRunning = false;
                }
                waveOutDeviceIdx = waveDialog.waveOutDeviceIdx;
                waverly.setWaveOutDevice(waveInDeviceIdx);
            }
            if (wasEngineRunning && !isEngineRunning)
            {
                transport.play();
                isEngineRunning = true;
            }
        }

        private void midiDevicesMenuItem_Click(object sender, EventArgs e)
        {
            MidiDialog midiDialog = new MidiDialog(this);
            midiDialog.setMidiInDevice(midiInDeviceIdx);
            midiDialog.ShowDialog();

            bool wasEngineRunning = isEngineRunning;
            if (midiInDeviceIdx != midiDialog.midiInDeviceIdx)
            {
                if (isEngineRunning)
                {
                    transport.stop(); 
                    isEngineRunning = false;
                }
                midiInDeviceIdx = midiDialog.midiInDeviceIdx;
                waverly.setMidiInDevice(midiInDeviceIdx);
            }
            if (midiOutDeviceIdx != midiDialog.midiOutDeviceIdx)
            {
                if (isEngineRunning)
                {
                    transport.stop(); 
                    isEngineRunning = false;
                }
                midiOutDeviceIdx = midiDialog.midiOutDeviceIdx;
                waverly.setMidiOutDevice(midiOutDeviceIdx);
            }
            if (wasEngineRunning && !isEngineRunning)
            {
                transport.play();
                isEngineRunning = true;
            }
        }

//- help menu -----------------------------------------------------------------

        private void aboutHelpMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Auditor\nversion 1.1.0\n" + "\xA9 Transonic Software 2007-2017\n" + "http://transonic.kohoutech.com";
            MessageBox.Show(msg, "About");
        }

    }
}

//  Console.WriteLine(" there's no sun in the shadow of the wizard");
