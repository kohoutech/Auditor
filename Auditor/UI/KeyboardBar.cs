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

namespace AuditorA.UI
{
    public class KeyboardBar : Form
    {
        AuditorWindow auditWindow;
        Key[] keys;
        Key[] whitekeys;
        Key[] blackkeys;
        Rectangle keyframe;
        Key mouseKey;
        
        int octavecount;
        int keytop;
        int keyleft;
        int whitekeycount;
        int whitekeywidth;
        int whitekeyheight;
        int blackkeycount;
        int blackkeywidth;
        int blackkeyheight;
        int octavewidth;
        int keywidth;
        int barwidth;
        int barheight;
        int keycount;

        public KeyboardBar(AuditorWindow _auditWindow)
        {
            auditWindow = _auditWindow;
            InitDimensions();
            InitializeComponent();
            initKeyboard();

            mouseKey = null;
        }

        private void InitDimensions()
        {
            octavecount = 5;
            keytop = 20;
            keyleft = 10;
            whitekeycount = octavecount * 7 + 1;
            whitekeywidth = 16;
            whitekeyheight = 88;
            blackkeycount = octavecount * 5;
            blackkeywidth = 10;
            blackkeyheight = 55;
            octavewidth = (whitekeywidth * 7);
            keywidth = (octavewidth * octavecount) + whitekeywidth;
            barwidth = keyleft + keywidth + 10;
            barheight = keytop + whitekeyheight + 5;
            keycount = whitekeycount + blackkeycount;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyboardBar));
            this.SuspendLayout();
            // 
            // KeyboardBar
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(876, 261);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KeyboardBar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auditor Keyboard Bar";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KeyboardBar_FormClosed);
            this.ResumeLayout(false);

        }

        void initKeyboard()
        {
            this.ClientSize = new System.Drawing.Size(10 + 576 + 10, 113);
            keyframe = new Rectangle(keyleft, keytop, keywidth, whitekeyheight);
            keys = new Key[keycount];
            int midibase = 36;                           //low C
            int midinum = midibase;
            for (int key = 0; key < keycount; key++)
            {
                keys[key] = new Key(midinum++);
            }

            whitekeys = new Key[whitekeycount];
            blackkeys = new Key[blackkeycount];

            int keynum = 0;
            int whitekeynum = 0;
            int blackkeynum = 0;
            for (int oct = 0; oct < octavecount; oct++)
            {
                whitekeys[whitekeynum++] = keys[keynum++];
                blackkeys[blackkeynum++] = keys[keynum++];
                whitekeys[whitekeynum++] = keys[keynum++];
                blackkeys[blackkeynum++] = keys[keynum++];
                whitekeys[whitekeynum++] = keys[keynum++];
                whitekeys[whitekeynum++] = keys[keynum++];
                blackkeys[blackkeynum++] = keys[keynum++];
                whitekeys[whitekeynum++] = keys[keynum++];
                blackkeys[blackkeynum++] = keys[keynum++];
                whitekeys[whitekeynum++] = keys[keynum++];
                blackkeys[blackkeynum++] = keys[keynum++];
                whitekeys[whitekeynum++] = keys[keynum++];
            };
            whitekeys[whitekeynum] = keys[keycount - 1];      //high C

            //white keys
            Rectangle whitekeyrect = new Rectangle(keyleft, keytop, whitekeywidth, whitekeyheight);
            for (int wk = 0; wk < whitekeycount; wk++)
            {
                whitekeys[wk].setShape(Key.KeyColor.WHITE, whitekeyrect);
                whitekeyrect.Offset(whitekeywidth, 0);
            }

            //black keys
            blackkeynum = 0;
            int[] blackkeyspacing = { 18, 29, 18, 18, 29 };
            Rectangle blackkeyrect = new Rectangle(keyleft + 10, keytop, blackkeywidth, blackkeyheight);
            for (int oct = 0; oct < octavecount; oct++)
            {
                for (int i = 0; i < 5; i++)
                {
                    blackkeys[blackkeynum++].setShape(Key.KeyColor.BLACK, blackkeyrect);
                    blackkeyrect.Offset(blackkeyspacing[i], 0);
                }
            }
        }

        //if we close the keyboard window, re-enable the keyboard toolbar button
        private void KeyboardBar_FormClosed(object sender, FormClosedEventArgs e)
        {
            auditWindow.enableKeyboardBarMenuItem(true);
        }

//- mouse events --------------------------------------------------------------

        public Key hitTest(Point p)
        {
            Key result = null;
            if (keyframe.Contains(p))
            {
                int oct = (p.X - keyleft) / (whitekeywidth * 7);
                if (p.Y < (keytop + blackkeyheight))
                {
                    for (int i = (oct * 5); i < (oct * 5 + 5); i++)
                    {
                        if (blackkeys[i].shape.Contains(p)) {
                            result = blackkeys[i];
                            break;
                        }
                    }
                } 
                if (result == null && oct < 5) 
                {
                    for (int i = (oct * 7); i < (oct * 7 + 7); i++)
                    {
                        if (whitekeys[i].shape.Contains(p)) {
                            result = whitekeys[i];
                            break;
                        }
                    }
                    
                }
                if (result == null) 
                {
                    if (whitekeys[whitekeycount-1].shape.Contains(p))
                        result = whitekeys[whitekeycount-1];
                }
            }
            return result;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mouseKey = hitTest(e.Location);
            if (mouseKey != null)
            {
                if (!mouseKey.sustain)
                {
                    mouseKey.pressed = true;
                    auditWindow.auditorA.sendMidiMsg(0x90, mouseKey.midinum, 0x40);
                    mouseKey.sustain = (e.Button == MouseButtons.Right);
                    Invalidate();
                }
                else
                {
                    mouseKey.sustain = false;
                }
            }
        }

        //allow user to drag mouse over or off keyboard
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseKey != null)
            {
                Key newkey = hitTest(e.Location);
                if (newkey != mouseKey)
                {
                    if (!mouseKey.sustain)
                    {
                        mouseKey.pressed = false;
                        auditWindow.auditorA.sendMidiMsg(0x80, mouseKey.midinum, 0x0);
                    }
                    mouseKey = newkey;
                    if (mouseKey != null)       //if dragged to another key
                    {
                        if (!mouseKey.sustain)
                        {
                            mouseKey.pressed = true;
                            auditWindow.auditorA.sendMidiMsg(0x90, mouseKey.midinum, 0x40);
                            mouseKey.sustain = (e.Button == MouseButtons.Right);
                        }
                        else
                        {
                            mouseKey.sustain = false;
                        }
                    }
                    Invalidate();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (mouseKey != null)
            {
                if (!mouseKey.sustain)
                {
                    mouseKey.pressed = false;
                    auditWindow.auditorA.sendMidiMsg(0x80, mouseKey.midinum, 0x0);
                    Invalidate();
                }
                mouseKey = null;
            }
        }

//- key events ----------------------------------------------------------------

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    //if (e.KeyCode == Keys.Delete)
        //}

        //protected override void OnKeyUp(KeyEventArgs e)
        //{
        //    //if (e.KeyCode == Keys.Delete)
        //}

//- painting ------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.DrawRectangle(Pens.Black, keyframe); //keyboard outline
            for (int i = 0; i < whitekeycount; i++)
            {
                g.DrawRectangle(Pens.Black, whitekeys[i].shape);
                g.FillRectangle(whitekeys[i].pressed ? Brushes.Red : Brushes.White, whitekeys[i].interior);
                
            }
            for (int i = 0; i < blackkeycount; i++)
            {
                g.FillRectangle(Brushes.Black, blackkeys[i].shape);
                g.FillRectangle(blackkeys[i].pressed ? Brushes.Gray : Brushes.Black, blackkeys[i].interior);
            }
        }

    }

//-----------------------------------------------------------------------------

    public class Key
    {
        public enum KeyColor
        {
            WHITE,
            BLACK
        }

        public int midinum;
        public bool pressed;
        public bool sustain;
        public Rectangle shape;
        public Rectangle interior;
        public KeyColor color;

        public Key(int _midinum)
        {
            midinum = _midinum;
            pressed = false;
            sustain = false;
        }

        public void setShape(KeyColor _color, Rectangle _shape) 
        {
            color = _color;
            shape = _shape;
            interior = new Rectangle(shape.X + 1, shape.Y + 1, shape.Width - 2, shape.Height - 2);
        }
    }
}
