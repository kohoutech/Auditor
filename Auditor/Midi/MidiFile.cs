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
using System.IO;

using Transonic.MIDI.System;

//J Glatt's Midi page: http://midi.teragonaudio.com/tech/midifile.htm

namespace Transonic.MIDI
{
    public class MidiFile
    {
        static uint currentTime;       //event time in ticks
        static int runningStatus;

//- loading -------------------------------------------------------------------

        public static Sequence readMidiFile(String filename)
        {
            MidiInStream stream = new MidiInStream(filename);

            //read midi file header
            String sig = stream.getString(4);
            uint hdrsize = stream.getFour();
            int fileFormat = stream.getTwo();
            int trackCount = stream.getTwo();
            int division = stream.getTwo();

            if (!sig.Equals("MThd") || hdrsize != 6)
            {
                throw new MidiFileException(filename + " is not a valid MIDI file ", 0);
            }

            Sequence seq = new Sequence(division);

            //read midi track data
            for (int i = 0; i < trackCount; i++)
            {
                Track track = loadTrackData(stream);
                seq.addTrack(track);
            }

            seq.finalizeLoad();
            return seq;
        }

        private static Track loadTrackData(MidiInStream stream)
        {
            //read track header
            String trackSig = stream.getString(4);
            uint trackDataLength = stream.getFour();

            Track track = new Track();
            currentTime = 0;
            runningStatus = 0;
            Event evt;
            do
            {
                evt = loadEventData(stream);
                track.addEvent(evt);
            } while (!(evt.msg is EndofTrackMessage));
            return track;
        }

        private static Event loadEventData(MidiInStream stream)
        {
            Message msg = null;
            currentTime += stream.getVariableLengthVal();      //add delta time to current num of ticks
            int status = stream.getOne();
            if (status < 0x80)              //running status 
            {
                stream.pushBack(1);
                status = runningStatus;
            }
            msg = Message.getMessage(stream, status);
            runningStatus = status;
            Event evt = new Event(currentTime, msg);
            return evt;
        }

//- saving -------------------------------------------------------------------

        public static void writeMidiFile(Sequence seq, String filename)
        {
            MidiOutStream stream = new MidiOutStream(filename);

            //midi file header
            stream.putString("MThd");
            stream.putFour(6);                      //header size
            stream.putTwo(1);                       //type 1 midi file
            stream.putTwo(seq.tracks.Count);        //track count
            stream.putTwo(seq.division);            //division

            for (int trackNum = 0; trackNum < seq.tracks.Count; trackNum++)
            {                
                seq.tracks[trackNum].saveTrack(stream);
            }
        }
    }

//-----------------------------------------------------------------------------

    //midi files store data in big endian format!
    public class MidiInStream
    {
        public String filename;
        byte[] midiData;
        int dataSize;
        int dataPos;

        //read midi data from file
        public MidiInStream(String _filename)
        {
            filename = _filename;
            try
            {
                midiData = File.ReadAllBytes(filename);
            }
            catch (FileNotFoundException e)
            {
                throw new MidiFileException("couldn't open " + filename, 0);
            }
            catch (Exception e)
            {
                throw new MidiFileException("couldn't read MIDI data from " + filename, 0);
            }
            dataSize = midiData.Length;
            dataPos = 0;
        }

        //read midi data from incoming midi bytes
        public MidiInStream(byte[] data)
        {
            midiData = data;
            dataSize = midiData.Length;
            dataPos = 0;
        }

        public int getDataPos()
        {
            return dataPos;
        }

        public void checkStream(int size)
        {
            if (dataPos + size > dataSize)
            {
                throw new MidiFileException("tried to read past end of file " + filename, dataPos);
            }
        }

        public int getOne()
        {
            checkStream(1);
            byte a = midiData[dataPos++];
            int result = (int)(a);
            return result;
        }

        public int getTwo()
        {
            checkStream(2);
            byte a = midiData[dataPos++];
            byte b = midiData[dataPos++];
            int result = (int)(a * 256 + b);
            return result;
        }

        //returns unsigned 4 byte val
        public uint getFour()
        {
            checkStream(4);
            byte a = midiData[dataPos++];
            byte b = midiData[dataPos++];
            byte c = midiData[dataPos++];
            byte d = midiData[dataPos++];
            uint result = (uint)(a * 256 + b);
            result = (result * 256 + c);
            result = (result * 256 + d);
            return result;
        }

        public uint getVariableLengthVal()
        {
            uint result = 0;                        //largest var len quant allowed = 0xffffffff
            uint b = (uint)getOne();
            while (b >= 0x80)
            {
                uint d = b % 128;
                result *= 128;
                result += d;
                b = (uint)getOne();
            }
            result *= 128;
            result += b;
            return result;
        }

        public String getString(int length)
        {
            checkStream(length);
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char a = (char)midiData[dataPos++];
                result.Append(a);
            }
            return result.ToString();
        }

        public void skipBytes(int skip)
        {
            checkStream(skip);
            dataPos += skip;
        }

        public void pushBack(int backup)
        {
            dataPos -= backup;
        }
    }

//-----------------------------------------------------------------------------

    //midi files store data in big endian format!
    public class MidiOutStream
    {
        public String filename;
        List<byte> midiData;

        public MidiOutStream(String _filename)
        {
            filename = _filename;
            midiData = new List<byte>();
        }

        public void putOne(int val)
        {
            byte a = (byte)(val % 256);
            midiData.Add(a);
        }

        public void putTwo(int val)
        {
            byte b = (byte)(val % 256);
            val /= 256;
            byte a = (byte)(val % 256);
            midiData.Add(a);
            midiData.Add(b);
        }

        public void putFour(int val)
        {
            byte d = (byte)(val % 256);
            val /= 256;
            byte c = (byte)(val % 256);
            val /= 256;
            byte b = (byte)(val % 256);
            val /= 256;
            byte a = (byte)(val % 256);
            midiData.Add(a);
            midiData.Add(b);
            midiData.Add(c);
            midiData.Add(d);            
        }

        public List<byte> getVarLenQuantity(uint delta)
        {
            List<byte> result = new List<byte>();
            for (int i = 0; i < 4; i++)
            {
                if (delta >= 0x80)
                {
                    result.Add((byte)(delta % 0x80));
                    delta /= 0x80;
                }
                else
                {
                    result.Add((byte)delta);
                    break;
                }
            }
            result.Reverse();
            for (int i = 0; i < result.Count - 1; i++)
                result[i] += 0x80;
            return result;
        }

        public void putString(String s)
        {
            byte[] data = Encoding.ASCII.GetBytes(s);
            midiData.AddRange(data);
        }

        public void putData(byte[] data)
        {
            midiData.AddRange(data);
        }

        public void writeOut()
        {
            File.WriteAllBytes(filename, midiData.ToArray());
        }
    }

//-----------------------------------------------------------------------------

    public class MidiFileException : Exception
    {
        public MidiFileException(String errorMsg, int pos) : base(errorMsg + " at pos [" + pos.ToString() + "]")
        {
        }
    }
}

//Console.WriteLine("there's no sun in the shadow of the wizard");
