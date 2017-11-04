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

using Transonic.MIDI;
using Transonic.MIDI.System;

namespace Transonic.MIDI.Engine
{
    public class Transport
    {
        IMidiView window;           //for updating the UI

        Sequence seq;
        float playbackSpeed;
        int division;
        int trackCount;

        MidiTimer timer;
        long startTime;
        long startOffset;
        long tickLen;          //length of one tick in 0.1 microsecs (100 ms)
        long tickTime;         //cumulative tick time
        public int tickNum;    //cur tick number

        int[] trackPos;        //pos of the next event in each track
        int tempoPos;          //pos of next tempo event
        TempoMessage curTempo;

        bool isPlaying;

        public Transport(IMidiView _window)
        {
            window = _window;

            timer = new MidiTimer();
            timer.Timer += new EventHandler(OnPulse);
            isPlaying = false;
            playbackSpeed = 1.0f;
            
            division = Sequence.DEFAULTDIVISION;
            setTempo(null);                         //default tempo & division
        }

        public void setSequence(Sequence _seq)
        {
            seq = _seq;
            division = seq.division;
            trackCount = seq.tracks.Count;
            trackPos = new int[trackCount];
            rewindSequence();
        }

        //tempo is len of quarter note in microsecs, division is number of ticks / quarter note
        public void setTempo(TempoMessage msg)
        {
            curTempo = msg;
            if (msg != null)
            {
                tickLen = (long)((curTempo.tempo / (division * playbackSpeed)) * 10.0f);     //len of each tick in 0.1 microsecs (or 100 nanosecs)
            }
            else
            {
                tickLen = (long)((Sequence.DEFAULTTEMPO / (division * playbackSpeed)) * 10.0f);
            }
        }

//- operation methods ---------------------------------------------------------

        public void init()
        {            
        }

        public void shutdown()
        {
            if (isPlaying)
            {
                stopSequence();
            }
        }

        public void rewindSequence()
        {
            if (!isPlaying)
            {
                //set initial tempo
                tempoPos = 0;
                if ((seq.tempoMap[0] != null) && (seq.tempoMap[0].time == 0))
                {
                    setTempo((TempoMessage)seq.tempoMap[0].msg);
                    tempoPos++;
                }
                else
                {
                    setTempo(null);
                }

                tickNum = 0;
                tickTime = tickLen;                    //time of first tick (not 0 - that would be no ticks)

                for (int i = 1; i < trackPos.Length; i++)
                    trackPos[i] = 0;
                startOffset = 0;
            }
        }

        public void playSequence()
        {
            if (!isPlaying)
            {
                startTime = DateTime.Now.Ticks - startOffset;

                timer.start(1);               //timer interval = 1 msec
                isPlaying = true;
            }
        }

        public void stopSequence()
        {
            if (isPlaying)
            {
                long now = DateTime.Now.Ticks;
                startOffset = (now - startTime);        //so timer will be correct when we restart this up again
                timer.stop();
                seq.allNotesOff();
                isPlaying = false;
            }
        }

        public void sequenceDone()
        {
            timer.stop();
            seq.allNotesOff();
            isPlaying = false;
            window.sequenceDone();
        }

        public void halfSpeedSequence(bool on)
        {
            playbackSpeed = on ? 0.5f : 1.0f;
            setTempo(curTempo);
        }

        //set cur pos in sequence to a specific tick, only if stoppped
        public void setSequencePos(int _ticknum)
        {
            if (!isPlaying)
            {
                tickNum = _ticknum;
                setTempo(null);                          //default tempo & division

                //find the most recent tempo event - if no tempo events, go with the default
                tempoPos = 0;
                while ((tempoPos < seq.tempoMap.Count) && (tickNum > seq.tempoMap[tempoPos].time))
                {
                    tempoPos++;
                }
                tempoPos--;
                if (tempoPos >= 0)
                {
                    TempoMessage msg = (TempoMessage)seq.tempoMap[tempoPos].msg;
                    setTempo(msg);

                    int tickOfs = _ticknum - curTempo.timing.tick;                          //num of ticks from prev tempo msg to now
                    tickTime = (curTempo.timing.microsec * 10L) + (tickOfs * tickLen);      //prev tempo's time (in usec/10) + time of ticks to now
                }
                else
                {
                    setTempo(null);
                }

                startOffset = tickTime;
                startTime = DateTime.Now.Ticks - startOffset;

                //set cur pos in each track
                //find and send most recent patch change message for each track
                for (int trackNum = 1; trackNum < trackCount; trackNum++)
                {
                    Track track = seq.tracks[trackNum];
                    List<Event> events = seq.tracks[trackNum].events;
                    trackPos[trackNum] = 0;
                    PatchChangeMessage patchmsg = null;
                    for (int i = 0; i < events.Count; i++)
                    {
                        if (events[i].msg is PatchChangeMessage)
                            patchmsg = (PatchChangeMessage)events[i].msg;
                        if (events[i].time > _ticknum)
                            break;
                        trackPos[trackNum]++;
                    }
                    if (patchmsg != null)
                    {
                        track.sendMessage(patchmsg);
                    }
                }
            }
        }

        public int getMilliSecTime()
        {
            return (int)(tickTime / 10000L);            //ret tick time in msec
        }

//- timer method --------------------------------------------------------------

        //when transport is playing, this will be called approx every millisec
        //one or more ticks may have happened during this time, so we get the current time in 0.1 MICROSECs
        //and calc the running time since start up & compare with the time of the next tick
        //if we've passed it, then inc tick number and calc time of the following tick
        //and then handle all the messages on every track for the tick that we just passed
        //keep doing this until we're caught up to the current tick number (ie its ahead of where we are now)
        //since 1 ms < threshold of simultaneity (~2 - 5 ms), all events should sound like there are happening at the same time
        private void OnPulse(object sender, EventArgs e)
        {
            long now = DateTime.Now.Ticks;          //one system's clock tick = 0.1 microsec
            long runningTime = (now - startTime);

            while (runningTime > tickTime)          //we've passed one or more ticks
            {
                tickNum++;                          //update tick number
                tickTime = tickTime + tickLen;      //and get time of next tick

                //handle tempo msgs
                while ((tempoPos < seq.tempoMap.Count) && (tickNum >= seq.tempoMap[tempoPos].time))
                {
                    TempoMessage msg = (TempoMessage)seq.tempoMap[tempoPos].msg;
                    setTempo(msg);
                    tempoPos++;           
                }

                //handle track events for each track
                //alldone will be true when we've reached the end of every track
                bool alldone = true;
                for (int trackNum = 1; trackNum < trackCount; trackNum++)
                {
                    Track track = seq.tracks[trackNum];
                    List<Event> events = track.events;

                    bool done = (trackPos[trackNum] >= events.Count);
                    while (!done && tickNum >= events[trackPos[trackNum]].time)
                    {
                        Message msg = events[trackPos[trackNum]].msg;           //get next msg for this track

                        if (!(msg is MetaMessage))
                        {
                            track.sendMessage(msg);                     //send it out                            
                        }
                        window.handleMessage(trackNum, msg);        //and update the UI 

                        trackPos[trackNum]++;
                        done = (trackPos[trackNum] >= events.Count);
                    }

                    alldone = alldone && done;
                }
                if (alldone)
                {
                    sequenceDone();
                }
            }
        }
    }

//- UI interface --------------------------------------------------------------

    //for communication with the UI
    public interface IMidiView
    {
        //public void sequenceBegin();
        void handleMessage(int track, Transonic.MIDI.Message message);

        void sequenceDone();
    }
}

//Console.WriteLine("there's no sun in the shadow of the wizard");
