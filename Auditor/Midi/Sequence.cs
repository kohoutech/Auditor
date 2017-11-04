/* ----------------------------------------------------------------------------
Transonic MIDI Library
Copyright (C) 1995-2017  George E Greaney

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

using Transonic.MIDI.System;

namespace Transonic.MIDI
{
    public class Sequence
    {
        public const int DEFAULTDIVISION = 120;
        public const int DEFAULTTEMPO = 500000;

        public MidiSystem midiSystem;
        public List<Track> tracks;
        public int division;
        public int duration;
        public List<Event> tempoMap;

        public Sequence(int _division)
        {
            midiSystem = null;
            division = _division;
            duration = 0;

            tracks = new List<Track>();
            tempoMap = new List<Event>();
        }

        public void addTrack(Track track)
        {
            tracks.Add(track);
        }

        public void deleteTrack(Track track)
        {
            tracks.Remove(track);
        }

        public void finalizeLoad()
        {
            calcTempoMap();
            for (int i = 1; i < tracks.Count; i++) 
            {
                tracks[i].finalizeLoad();
                if (duration < tracks[i].duration) duration = tracks[i].duration;
            }
        }

        public void setMidiSystem(MidiSystem system)
        {
            midiSystem = system;
            for (int i = 1; i < tracks.Count; i++)
            {
                //tracks[i].setInputDevice(system.inputDevices[0]);
                //tracks[i].setInputChannel(i);
                tracks[i].setOutputDevice(system.outputDevices[0]);
                tracks[i].setOutputChannel(i-1);
            }
        }

        //build the tempo map from tempo message ONLY from track 0; tempo messages in other tracks will be IGNORED
        public void calcTempoMap()
        {
            int time = 0;               //time in MICROseconds
            int tempo = 0;              //microseconds per quarter note
            int prevtick = 0;           //tick of prev tempo event

            Track tempoTrack = tracks[0];
            for (int i = 0; i < tempoTrack.events.Count; i++)
            {
                Event evt = tempoTrack.events[i];
                if (evt.msg is TempoMessage)
                {
                    TempoMessage tempoMsg = (TempoMessage)evt.msg;
                    int msgtick = (int)evt.time;                                //the tick this tempo message occurs at
                    int delta = (msgtick - prevtick);                           //amount of ticks at _prev_ tempo
                    time += (int)((((float)delta) / division) * tempo);         //calc time in microsec of this tempo event
                    tempoMsg.timing = new Timing(msgtick, time, 0);
                    tempoMap.Add(evt);

                    prevtick = msgtick;
                    tempo = tempoMsg.tempo;
                }
            }
        }

        public void dump()
        {
            for (int i = 0; i < tracks.Count; i++)
            {
                Console.WriteLine("contents of track[{0}]", i);
                tracks[i].dump();
            }
        }

        public void allNotesOff()
        {
            for (int trackNum = 1; trackNum < tracks.Count; trackNum++)
            {
                tracks[trackNum].allNotesOff();
            }
        }
    }

//-----------------------------------------------------------------------------

    //maps a tempo message or a time signature message to a elapsed time, so if move the cur pos
    //in a sequence, we can calculate what time that is; needs to be recalculated any time tempo or time sig change
    public class Timing
    {
        public int tick;
        public int microsec;
        public int beat;

        public Timing(int _tick, int _microsec, int _beat)
        {
            tick = _tick;
            microsec = _microsec;
            beat = _beat;
        }
    }


}
