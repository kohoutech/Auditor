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
using AuditorA.UI;

namespace AuditorA.Engine
{
    public class VSTPlugin
    {
        public VSTPanel panel;
        public Auditor auditorA;
        public String filename;
        public int plugnum;        

        public String name;
        public String vendor;
        public int version; 
        public int numPrograms;
        public int numParams;
        public int numInputs;
        public int numOutputs;
        public int flags;
        public int uniqueID;
        public int editorWidth;
        public int editorHeight;

        public VSTParam[] parameters;
        public String[] programs;
        int curProgramNum;

        public VSTPlugin(VSTPanel _panel, Auditor _auditorA, int _plugnum, String _filename)
        {
            panel = _panel;
            auditorA = _auditorA;
            plugnum = _plugnum;
            filename = _filename;        
        }

        public bool load()
        {
            bool result = auditorA.loadPlugin(plugnum, filename);
            if (result)
            {
                PluginInfo pluginfo = new PluginInfo();
                auditorA.getPluginInfo(plugnum, ref pluginfo);
                name = pluginfo.name;
                vendor = pluginfo.vendor;
                version = pluginfo.version;
                numPrograms = pluginfo.numPrograms;
                numParams = pluginfo.numParameters;
                numInputs = pluginfo.numInputs;
                numOutputs = pluginfo.numOutputs;
                flags = pluginfo.flags;
                uniqueID = pluginfo.uniqueID;
                editorWidth = pluginfo.editorWidth;
                editorHeight = pluginfo.editorHeight;

                parameters = new VSTParam[numParams];
                for (int i = 0; i < numParams; i++)
                {
                    String paramName = auditorA.getPluginParamName(plugnum, i);
                    float paramVal = auditorA.getPluginParamValue(plugnum, i);
                    parameters[i] = new VSTParam(i, paramName, paramVal);                    
                }

                if (numPrograms > 0)
                {
                    programs = new String[numPrograms];
                    for (int i = 0; i < numPrograms; i++)
                    {
                        String progName = auditorA.getPluginProgramName(plugnum, i);
                        programs[i] = progName;
                    }
                }
                else
                {
                    programs = new String[]{"no programs"};
                }
                curProgramNum = 0;

            }
            return result;
        }

        public void unload()
        {
            auditorA.unloadPlugin(plugnum);
        }

        public void setCurrent()
        {
            auditorA.selectPlugin(plugnum);
        }

        public void setParamValue(int paramNum, float paramVal) 
        {
            parameters[paramNum].value = paramVal;
            auditorA.setPluginParam(plugnum, paramNum, paramVal);
        }

        public void setProgram(int progNum)
        {
            curProgramNum = progNum;
            auditorA.setPluginProgram(plugnum, progNum);
        }

        public void openEditorWindow(IntPtr editorWindow)
        {
            auditorA.openEditorWindow(plugnum, editorWindow);
        }

        public void closeEditorWindow()
        {
            auditorA.closeEditorWindow(plugnum);
        }
    }

//-----------------------------------------------------------------------------

    [StructLayout(LayoutKind.Sequential)]
    public struct PluginInfo
    {
        public string name;
        public string vendor;
        public int version;
        public int numPrograms;
        public int numParameters;
        public int numInputs;
        public int numOutputs;
        public int flags;
        public int uniqueID;
        public int editorWidth;
        public int editorHeight;
    }

    public class VSTParam
    {
        public int num;
        public String name;
        public float value;

        public VSTParam(int _num, String _name, float _val)
        {
            num = _num;
            name = _name;
            value = _val;
        }
    }

    //public class VSTProgram
    //{
    //    public int num;
    //    public String name;

    //    public VSTProgram(int _num, String _name)
    //    {
    //        num = _num;
    //        name = _name;
    //    }
    //}
}

//Console.WriteLine(" plugin " + name + " parameter " + i + " name is " + paramName);
