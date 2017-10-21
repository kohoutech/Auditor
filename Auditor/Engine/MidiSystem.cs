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
    public class MidiSystem
    {
        //p/invoke imports
        [DllImport("winmm.dll", SetLastError = true)]
        static extern uint midiInGetNumDevs();

        [DllImport("winmm.dll", SetLastError = true)]
        static extern MMRESULT midiInGetDevCaps(int uDeviceID, ref MIDIINCAPS caps, int cbMidiInCaps);

        [DllImport("winmm.dll", SetLastError = true)]
        static extern uint midiOutGetNumDevs();

        [DllImport("winmm.dll", SetLastError = true)]
        static extern MMRESULT midiOutGetDevCaps(int uDeviceID, ref MIDIOUTCAPS lpMidiOutCaps, int cbMidiOutCaps);

        public Auditor auditorA;
        public List<String> inputDeviceNames;
        public List<MidiInput> inputDevices;
        int curInputDevice;

        public List<String> outputDeviceNames;

        public MidiSystem(Auditor _auditorA)
        {
            auditorA = _auditorA;

            //input devices
            int deviceID;
            int incount = (int) midiInGetNumDevs();
            Console.WriteLine("midi in devices: " + incount);

            inputDeviceNames = new List<String>(incount + 1);
            inputDeviceNames.Add("no input device");
            inputDevices = new List<MidiInput>(incount);

            MIDIINCAPS inCaps = new MIDIINCAPS();
            for (deviceID = 0; deviceID < incount; deviceID++)
            {
                Console.WriteLine("getting midi in device: " + deviceID);
                MMRESULT result = midiInGetDevCaps(deviceID, ref inCaps, Marshal.SizeOf(inCaps));
                String inName = inCaps.szPname;
                Console.WriteLine("got midi in device: " + inName);

                inputDeviceNames.Add(inName);
                MidiInput inDev = new MidiInput(this, deviceID, inName);
                Console.WriteLine("adding midi in device: " + inName);
                inputDevices.Add(inDev);
            }
            curInputDevice = -1;

            //output devices
            uint outcount = midiOutGetNumDevs();
            Console.WriteLine("midi out devices: " + outcount);
            outputDeviceNames = new List<String>((int)outcount + 1);
            outputDeviceNames.Add("no output device");
            MIDIOUTCAPS outCaps = new MIDIOUTCAPS();
            for (deviceID = 0; deviceID < outcount; deviceID++)
            {
                MMRESULT result = midiOutGetDevCaps(deviceID, ref outCaps, Marshal.SizeOf(outCaps));
                String outName = outCaps.szPname;
                outputDeviceNames.Add(outName);
            }
        }

        public void shutDown()
        {
            for (int i = 0; i < inputDevices.Count; i++)
            {
                Console.WriteLine("shutting down midi device " + i);
                inputDevices[i].stop();
                inputDevices[i].close();
            }
        }


        public List<String> getInDevNameList()
        {
            return inputDeviceNames;
        }

        public List<String> getOutDevNameList()
        {
            return outputDeviceNames;
        }

        public void setMidiInDevice(int deviceIdx) 
        {
            //shut down prev input device
            if (curInputDevice >= 0)
            {
                inputDevices[curInputDevice].stop();
                inputDevices[curInputDevice].close();
            }
            if (deviceIdx >= 0)
            {
                Console.WriteLine("opening midi in device: " + deviceIdx);                
                inputDevices[deviceIdx].open();
                inputDevices[deviceIdx].start();             
            }
            curInputDevice = deviceIdx;
        }
    }

//-----------------------------------------------------------------------------

    [StructLayout(LayoutKind.Sequential)]
    struct MIDIINCAPS
    {
        public ushort wMid;             //manfacturer id
        public ushort wPid;             //product id
        public uint vDriverVersion;     //device driver ver num, high byte = major ver, lo byte = minor ver
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          //product name
        public uint dwSupport;          //must be 0
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MIDIOUTCAPS
    {
        public ushort wMid;             //manfacturer id
        public ushort wPid;             //product id
        public uint vDriverVersion;     //device driver ver num, high byte = major ver, lo byte = minor ver
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          //product name
        public ushort wTechnology;      //type of midi output device
        public ushort wVoices;          //num voices if internal synth, 0 if device is a port
        public ushort wNotes;           //max num of notes synth can play, 0 if device is a port
        public ushort wChannelMask;     //internal synth's channel, -1 if device is a port
        public uint dwSupport;          //optional functionality supported by the device
    }
}

//  Console.WriteLine(" there's no sun in the shadow of the wizard");
