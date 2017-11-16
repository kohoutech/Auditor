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
using System.Drawing.Drawing2D;

namespace Auditor.UI
{
    public class VSTRack : Control
    {
        public AuditorWindow auditwin;

        public const int UNITCOUNT = 4;
        public VSTPanel[] panels;
        public int currentPlugin;

        int rackWidth;
        int rackHeight;
        public Rectangle leftRail;
        public Rectangle rightRail;
        public const int RAILWIDTH = 20;

        //cons
        public VSTRack(AuditorWindow _auditwin)
        {
            auditwin = _auditwin;

            rackWidth = VSTPanel.PANELWIDTH;
            rackHeight = VSTPanel.PANELHEIGHT * UNITCOUNT;
            this.Size = new Size(rackWidth, rackHeight);
            this.BackColor = Color.Black;
            leftRail = new Rectangle(0, 0, RAILWIDTH, rackHeight);
            rightRail = new Rectangle(rackWidth - RAILWIDTH, 0, RAILWIDTH, rackHeight);

            panels = new VSTPanel[UNITCOUNT];
            currentPlugin = -1;                     //no plugins to select initially
        }

//- panel management ----------------------------------------------------------

        public bool loadPlugin(int plugNum, String plugPath)
        {
            VSTPanel panel = new VSTPanel(this, plugNum);
            bool result = panel.loadPlugin(plugPath);
            if (result)
            {
                panels[plugNum] = panel;
                panel.Location = new Point(0, plugNum * VSTPanel.PANELHEIGHT);
                this.Controls.Add(panel);
            }
            return result;
        }

        public void unloadPlugin(int plugNum)
        {
            if (currentPlugin == plugNum)       //if we remove the current panel, then no other panel is current (for now)
            {
                currentPlugin = -1;
            }
            panels[plugNum].shutDownPlugin();
            this.Controls.Remove(panels[plugNum]);
        }

        public void selectPlugin(int plugNum)
        {
            if (currentPlugin >= 0)
            {
                panels[currentPlugin].clearCurrentPlugin();      //unselect current plug
            }
            currentPlugin = plugNum;
            panels[currentPlugin].setCurrentPlugin();        //and select new plug
        }

        public void showSelectedPluginInfo()
        {
            
        }

        public void showSelectedPluginParams()
        {
        }

        public void showSelectedPluginEditor()
        {
            panels[currentPlugin].showEditorWindow();
        }

//- painting ------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //rails
            g.FillRectangle(Brushes.DarkGray, leftRail);
            g.FillRectangle(Brushes.DarkGray, rightRail);

            //screw holes
            for (int i = 0; i < UNITCOUNT; i++)
            {
                int rackofs = i * VSTPanel.PANELHEIGHT;
                g.FillEllipse(Brushes.Black, 5, rackofs+10, 10, 10);
                g.FillEllipse(Brushes.Black, 5, rackofs + 55, 10, 10);
                g.FillEllipse(Brushes.Black, 385, rackofs + 10, 10, 10);
                g.FillEllipse(Brushes.Black, 385, rackofs + 55, 10, 10);
            }
        }
    }
}
