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
using AuditorA.Engine;

namespace AuditorA
{
    public class Auditor
    {
        //auditorB callback delegate
        public delegate void AuditorBProc(int msg, int param1, float param3);

        //communication with auditorB
        [DllImport("AuditorB.DLL")]
        public static extern void AuditorInit();

        [DllImport("AuditorB.DLL")]
        public static extern void AuditorShutDown();

        [DllImport("AuditorB.DLL")]
        public static extern void AuditorStartEngine();

        [DllImport("AuditorB.DLL")]
        public static extern void AuditorStopEngine();

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorSetWaveIn(int deviceIdx);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorSetWaveOut(int deviceIdx);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorSetMIDIIn(int deviceIdx);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorSetMIDIOut(int deviceIdx);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AuditorLoadPlugin(String filename, int vstnum);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorUnloadPlugin(int vstnum);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorSelectPlugin(int vstnum);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorGetPluginInfo(int vstnum, ref PluginInfo info);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern String AuditorGetParamName(int vstnum, int paramnum);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern float AuditorGetParamValue(int vstnum, int paramnum);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorSetParamVal(int vstnum, int paramnum, float paramval);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern String AuditorGetProgramName(int vstnum, int prognum);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorSetProgram(int vstnum, int prognum);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorOpenEditor(int vstnum, IntPtr hwnd);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorCloseEditor(int vstnum);

        [DllImport("AuditorB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AuditorHandleMidiMsg(int b1, int b2, int b3);

        public WaveSystem waveSystem;
        public MidiSystem midiSystem;
        bool isEngineRunning;

//- host mangement ------------------------------------------------------------

        AuditorWindow auditwindow;

        public Auditor(AuditorWindow _auditwindow)
        {
            auditwindow = _auditwindow;
            //Console.WriteLine("creating wave system");
            waveSystem = new WaveSystem(this);
            //Console.WriteLine("creating midi system");
            midiSystem = new MidiSystem(this);          //get lists of intput/output wave devices
            //Console.WriteLine("initializing auditor B");
            AuditorInit();                          //init auditor B dll
            isEngineRunning = false;
        }

        public void shutDown()
        {
            midiSystem.shutDown();
            AuditorShutDown();                      //shut down auditor B dll
        }

        public void startEngine()
        {
            AuditorStartEngine();
            isEngineRunning = true;
        }

        public void stopEngine()
        {
            AuditorStopEngine();
            isEngineRunning = false;
        }

        public void setWaveInDevice(int deviceIdx)
        {
            AuditorSetWaveIn(deviceIdx);
        }

        public void setWaveOutDevice(int deviceIdx)
        {
            AuditorSetWaveOut(deviceIdx);
        }

        public void setMidiInDevice(int deviceIdx)
        {
            //AuditorSetMIDIIn(deviceIdx);
            midiSystem.setMidiInDevice(deviceIdx);
        }

        public void setMidiOutDevice(int deviceIdx)
        {
            //AuditorSetMIDIOut(deviceIdx);
        }

//- plugin mangement ----------------------------------------------------------

        public bool loadPlugin(int plugNum, String filepath) 
        {
            Console.WriteLine(" auditor A loading plugin " + filepath);
            bool result = AuditorLoadPlugin(filepath, plugNum);
            return result;
        }

        public void unloadPlugin(int plugnum)
        {
            AuditorUnloadPlugin(plugnum);
        }

        public void selectPlugin(int plugnum)
        {
            AuditorSelectPlugin(plugnum);
        }

        public void getPluginInfo(int plugNum, ref PluginInfo pluginfo)
        {
            AuditorGetPluginInfo(plugNum, ref pluginfo);
        }

        public String getPluginParamName(int plugnum, int paramnum)
        {
            return AuditorGetParamName(plugnum, paramnum);
        }

        public float getPluginParamValue(int plugnum, int paramnum)
        {
            return AuditorGetParamValue(plugnum, paramnum);
        }

        public void setPluginParam(int plugnum, int paramnum, float paramval)
        {
            AuditorSetParamVal(plugnum, paramnum, paramval);
        }

        public String getPluginProgramName(int plugnum, int prognum)
        {
            return AuditorGetProgramName(plugnum, prognum);
        }

        public void setPluginProgram(int plugnum, int prognum)
        {
            AuditorSetProgram(plugnum, prognum);
        }

        public void openEditorWindow(int plugnum, IntPtr hwnd)
        {
            AuditorOpenEditor(plugnum, hwnd);
        }

        public void closeEditorWindow(int plugnum)
        {
            AuditorCloseEditor(plugnum);
        }

        public void sendMidiMsg(int b1, int b2, int b3)
        {
            AuditorHandleMidiMsg(b1, b2, b3);
        }
    }

    public enum MMRESULT : uint
    {
        MMSYSERR_NOERROR = 0,
        MMSYSERR_ERROR = 1,
        MMSYSERR_BADDEVICEID = 2,
        MMSYSERR_NOTENABLED = 3,
        MMSYSERR_ALLOCATED = 4,
        MMSYSERR_INVALHANDLE = 5,
        MMSYSERR_NODRIVER = 6,
        MMSYSERR_NOMEM = 7,
        MMSYSERR_NOTSUPPORTED = 8,
        MMSYSERR_BADERRNUM = 9,
        MMSYSERR_INVALFLAG = 10,
        MMSYSERR_INVALPARAM = 11,
        MMSYSERR_HANDLEBUSY = 12,
        MMSYSERR_INVALIDALIAS = 13,
        MMSYSERR_BADDB = 14,
        MMSYSERR_KEYNOTFOUND = 15,
        MMSYSERR_READERROR = 16,
        MMSYSERR_WRITEERROR = 17,
        MMSYSERR_DELETEERROR = 18,
        MMSYSERR_VALNOTFOUND = 19,
        MMSYSERR_NODRIVERCB = 20,
        WAVERR_BADFORMAT = 32,
        WAVERR_STILLPLAYING = 33,
        WAVERR_UNPREPARED = 34
    }
}


//  Console.WriteLine(" there's no sun in the shadow of the wizard");
