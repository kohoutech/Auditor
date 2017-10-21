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
using System.Runtime.InteropServices;

namespace AuditorA.Engine
{
    //adapted from PatchWorker
    public class MidiInput
    {
        //midi input callback delegate
        public delegate void MidiInProc(int handle, int msg, int instance, int param1, int param2);

        //p/invoke imports
        [DllImport("winmm.dll", SetLastError = true)]
        static extern MMRESULT midiInOpen(out IntPtr lphMidiIn, int uDeviceID, MidiInProc dwCallback,
            IntPtr dwInstance, int dwFlags);

        [DllImport("winmm.dll", SetLastError = true)]
        private static extern MMRESULT midiInStart(IntPtr lphMidiIn);

        [DllImport("winmm.dll", SetLastError = true)]
        private static extern MMRESULT midiInStop(IntPtr lphMidiIn);

        [DllImport("winmm.dll", SetLastError = true)]
        private static extern MMRESULT midiInClose(IntPtr lphMidiIn);

        //local vars
        MidiSystem midiSystem;
        public int devID;
        public String devName;
        public IntPtr devHandle;

        private MidiInProc midiInProc;
        private bool opened;
        private bool started;        
        
        const int CALLBACK_FUNCTION = 0x30000;

        //cons
        public MidiInput(MidiSystem _midiSystem, int _id, string _name)
        {
            midiSystem = _midiSystem;
            devID = _id;
            devName = _name;
            opened = false;
            started = false;        
            Console.WriteLine("created midi input device " + devName);
        }

// midi funcs -----------------------------------------------------------------

        public void open()
        {
            if (!opened)
            {
                midiInProc = HandleMessage;
                MMRESULT result = midiInOpen(out devHandle, devID, midiInProc, IntPtr.Zero, CALLBACK_FUNCTION);
                opened = true;
                Console.WriteLine("opened device " + devName + " result = " + result);
            }
        }

        public void start()
        {
            if (!started)
            {
                MMRESULT result = midiInStart(devHandle);
                started = true;
                Console.WriteLine("started device " + devName + " result = " + result);
            }
        }

        public void stop() 
        {
            if (started)
            {
                MMRESULT result = midiInStop(devHandle);
                started = false;
                Console.WriteLine("stopped device " + devName + " result = " + result);
            }
        }

        public void close()
        {
            if (opened)
            {
                MMRESULT result = midiInClose(devHandle);
                opened = false;
                Console.WriteLine("closed device " + devName + " result = " + result);
            }
        }

// midi input handler -----------------------------------------------------------------

        const int MIM_OPEN = 0x3C1;
        const int MIM_CLOSE = 0x3C2;
        const int MIM_DATA = 0x3C3;
        const int MIM_LONGDATA = 0x3C4;
        const int MIM_ERROR = 0x3C5;
        const int MIM_LONGERROR = 0x3C6;
        const int MIM_MOREDATA = 0x3CC;

        private void HandleMessage(int handle, int msg, int instance, int param1, int param2)
        {
            if (msg == MIM_OPEN)    
            {
            }
            else if (msg == MIM_CLOSE)
            {
            }
            else if (msg == MIM_DATA)
            {
                byte[] msgbytes = BitConverter.GetBytes(param1);
                midiSystem.auditorA.sendMidiMsg(msgbytes[0], msgbytes[1], msgbytes[2]);
                Console.WriteLine("name = " + devName + ", " + instance + ", " +
                    msgbytes[0].ToString("X2") + "." + msgbytes[1].ToString("X2") + "." + msgbytes[2].ToString("X2"));
            }
            else if (msg == MIM_LONGDATA)
            {             
            }
            else if (msg == MIM_MOREDATA)
            {
            }
            else if (msg == MIM_ERROR)
            {             
            }
            else if (msg == MIM_LONGERROR)
            {                
            }
        }
    }    
}
