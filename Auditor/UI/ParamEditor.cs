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
using System.Drawing;
using AuditorA.Engine;

namespace Auditor.UI
{
    public class ParamEditor : Form
    {
        VSTPanel panel;
        VSTParam[] parameters;
        Label[] paramNames;
        Label[] paramValues;
        HScrollBar[] paramsliders;
        private Panel pnlParams;
        const int PARAMVSPACE = 25;

        public ParamEditor(VSTPanel _panel)
        {
            InitializeComponent();           
                        
            panel = _panel;
            parameters = panel.plugin.parameters;
            int paramcount = parameters.Length;
            paramNames = new Label[paramcount];
            paramValues = new Label[paramcount];
            paramsliders = new HScrollBar[paramcount];
            for (int i = 0; i < paramcount; i++)
            {
                VSTParam param = parameters[i];
                Label paramName = new Label();
                paramName.Text = param.name;
                paramName.AutoSize = true;
                paramName.Location = new Point(10, i * PARAMVSPACE + 10);
                paramNames[i] = paramName;
                pnlParams.Controls.Add(paramName);

                Label paramValue = new Label();
                paramValue.Text = param.value.ToString("F4");
                paramValue.AutoSize = true;
                paramValue.Location = new Point(85, i * PARAMVSPACE + 10);
                paramValues[i] = paramValue;
                pnlParams.Controls.Add(paramValue);

                HScrollBar paramSlider = new HScrollBar();
                paramSlider.Minimum = 0;
                paramSlider.Maximum = 10039;
                paramSlider.Tag = i;
                paramSlider.SmallChange = 1;
                paramSlider.LargeChange = 10000 / 250;
                paramSlider.Value = (int)(param.value * 10000);
                paramSlider.Location = new Point(135, i * PARAMVSPACE + 10);
                paramSlider.Width = 250;
                paramSlider.ValueChanged += new EventHandler(paramSliderValueChanged);
                paramsliders[i] = paramSlider;
                pnlParams.Controls.Add(paramSlider);
            }

            pnlParams.Height = 500;

            Label gutter = new Label();
            gutter.Text = "";
            gutter.Height = 10;
            gutter.Location = new Point(10, paramcount * PARAMVSPACE);
            pnlParams.Controls.Add(gutter);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParamEditor));
            this.pnlParams = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlParams
            // 
            this.pnlParams.AutoScroll = true;
            this.pnlParams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.pnlParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParams.Location = new System.Drawing.Point(0, 0);
            this.pnlParams.Name = "pnlParams";
            this.pnlParams.Size = new System.Drawing.Size(424, 361);
            this.pnlParams.TabIndex = 0;
            // 
            // ParamEditor
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(424, 361);
            this.Controls.Add(this.pnlParams);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ParamEditor";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        // Create the Scroll event handler.
        private void paramSliderValueChanged (Object sender, EventArgs e)
        {
            HScrollBar paramSlider = (HScrollBar)sender;
            int paramnum = (int)paramSlider.Tag;
            Label paramValue = paramValues[paramnum];
            float value = (paramSlider.Value)/10000.0f;
            paramValue.Text = value.ToString("F4");
            panel.plugin.setParamValue(paramnum, value);
        }

        private void scrollParams_Scroll(object sender, ScrollEventArgs e)
        {
            int ofs = e.NewValue;
            SetDisplayRectLocation(0, (-1 * ofs));
            AdjustFormScrollbars(true);            
        }
    }
}
