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

//J Glatt's Midi page: http://midi.teragonaudio.com/tech/midispec.htm

namespace Transonic.MIDI
{
    public class Message
    {

//- static methods ------------------------------------------------------------

        public static Message getMessage(MidiInStream stream, int status)
        {
            Message msg = null;
            if (status >= 0x80 && status < 0xf0)
            {
                msg = getChannelMessage(stream, status);
            }
            else if (status >= 0xf0 && status < 0xff)
            {
                msg = getSystemMessage(stream, status);
            }
            else if (status == 0xff)
            {
                msg = getMetaMessage(stream, status);
            }

            return msg;
        }

        private static Message getChannelMessage(MidiInStream stream, int status)
        {

            Message msg = null;
            int msgtype = status / 16;
            int channel = status % 16;
            switch (msgtype)
            {
                case 0x8 :
                    msg = new NoteOffMessage(stream, channel);
                    break;
                case 0x9:
                    msg = new NoteOnMessage(stream, channel);
                    break;
                case 0xa:
                    msg = new AftertouchMessage(stream, channel);
                    break;
                case 0xb:
                    msg = new ControllerMessage(stream, channel);
                    break;
                case 0xc:
                    msg = new PatchChangeMessage(stream, channel);
                    break;
                case 0xd:
                    msg = new ChannelPressureMessage(stream, channel);
                    break;
                case 0xe:
                    msg = new PitchWheelMessage(stream, channel);
                    break;
                default :
                    break;
            }
            //convert noteon msg w/ vel = 0 to noteoff msg
            if (msg is NoteOnMessage)
            {
                NoteOnMessage noteOn = (NoteOnMessage)msg;
                if (noteOn.velocity == 0)
                {
                    NoteOffMessage noteOff = new NoteOffMessage(noteOn.channel, noteOn.noteNumber, 0);
                    msg = noteOff;
                }
            }
            return msg;
        }

        private static Message getSystemMessage(MidiInStream stream, int status)
        {
            Message msg = null;
            if (status == 0xF0)
            {
                msg = new SysExMessage(stream); 
            }
            else
            {
                msg = new SystemMessage(stream, status);
            }
            return msg;
        }

        private static Message getMetaMessage(MidiInStream stream, int _status)
        {
            Message msg = null;
            int msgtype = stream.getOne();
            switch (msgtype) 
            {
                case 0x00:
                    msg = new SequenceNumberMessage(stream);
                    break;
                case 0x01:
                    msg = new TextMessage(stream);
                    break;
                case 0x02:
                    msg = new CopyrightMessage(stream);
                    break;
                case 0x03:
                    msg = new TrackNameMessage(stream);
                    break;
                case 0x04:
                    msg = new InstrumentMessage(stream);
                    break;
                case 0x05:
                    msg = new LyricMessage(stream);
                    break;
                case 0x06:
                    msg = new MarkerMessage(stream);
                    break;
                case 0x07:
                    msg = new CuePointMessage(stream);
                    break;
                case 0x08:
                    msg = new PatchNameMessage(stream);
                    break;
                case 0x09:
                    msg = new DeviceNameMessage(stream);
                    break;
                case 0x20:
                    msg = new MidiChannelMessage(stream);
                    break;
                case 0x21:
                    msg = new MidiPortMessage(stream);
                    break;
                case 0x2f:
                    msg = new EndofTrackMessage(stream);
                    break;
                case 0x51:
                    msg = new TempoMessage(stream);
                    break;
                case 0x54:
                    msg = new SMPTEOffsetMessage(stream);
                    break;
                case 0x58:
                    msg = new TimeSignatureMessage(stream);
                    break;
                case 0x59:
                    msg = new KeySignatureMessage(stream);
                    break;
                default:
                    msg = new UnknownMetaMessage(stream, msgtype);
                    break;
            }
            return msg;
        }


//- base class ----------------------------------------------------------------

        public Message()
        {
        }

        //for splitting a midi msg - handles subclass fields too
        public Message copy()
        {
            return (Message)this.MemberwiseClone();
        }

        //for sending a msg to an output device
        virtual public byte[] getDataBytes() 
        {
            return null;
        }
    }

//- subclasses ----------------------------------------------------------------

//-----------------------------------------------------------------------------
//  CHANNEL MESSAGES
//-----------------------------------------------------------------------------

    //channel message base class
    public class ChannelMessage : Message
    {
        public int channel;

        public ChannelMessage(int _channel) : base()
        {
            channel = _channel;
        }
    }

    public class NoteOnMessage : ChannelMessage     //0x90
    {
        public int noteNumber;
        public int velocity;

        public NoteOnMessage(int channel, int note, int vel)
            : base(channel)
        {
            noteNumber = note;
            velocity = vel;
        }

        public NoteOnMessage(MidiInStream stream, int channel)
            : base(channel)
        {
            noteNumber = stream.getOne();
            velocity = stream.getOne();
        }

        override public byte[] getDataBytes()
        {
            byte[] bytes = new byte[3];
            bytes[0] = (byte)(0x90 + channel);
            bytes[1] = (byte)noteNumber;
            bytes[2] = (byte)velocity;
            return bytes;
        }

        public override string ToString()
        {
            return "Note On (" + channel + ") note = " + noteNumber + ", velocity = " + velocity;
        }
    }

    public class NoteOffMessage : ChannelMessage   //0x80
    {
        public int noteNumber;
        public int velocity;

        public NoteOffMessage(int channel, int note, int vel)
            : base(channel)
        {
            noteNumber = note;
            velocity = vel;
        }

        public NoteOffMessage(MidiInStream stream, int channel)
            : base(channel)
        {
            noteNumber = stream.getOne();
            velocity = stream.getOne();
        }

        override public byte[] getDataBytes()
        {
            byte[] bytes = new byte[3];
            bytes[0] = (byte)(0x80 + channel);
            bytes[1] = (byte)noteNumber;
            bytes[2] = (byte)velocity;
            return bytes;
        }

        public override string ToString()
        {
            return "Note Off (" + channel + ") note = " + noteNumber;
        }
    }

    public class AftertouchMessage : ChannelMessage     //0xA0
    {
        public int noteNumber;
        public int pressure;

        public AftertouchMessage(int channel, int note, int press)
            : base(channel)
        {
            noteNumber = note;
            pressure = press;
        }

        public AftertouchMessage(MidiInStream stream, int channel)
            : base(channel)
        {
            noteNumber = stream.getOne();
            pressure = stream.getOne();
        }

        override public byte[] getDataBytes()
        {
            byte[] bytes = new byte[3];
            bytes[0] = (byte)(0xa0 + channel);
            bytes[1] = (byte)noteNumber;
            bytes[2] = (byte)pressure;
            return bytes;
        }
    }

    public class ControllerMessage : ChannelMessage     //0xB0
    {
        public int ctrlNumber;
        public int ctrlValue;

        public ControllerMessage(int channel, int num, int val)
            : base(channel)
        {
            ctrlNumber = num;
            ctrlValue = val;
        }

        public ControllerMessage(MidiInStream stream, int channel)
            : base(channel)
        {
            ctrlNumber = stream.getOne();
            ctrlValue = stream.getOne();
        }

        override public byte[] getDataBytes()
        {
            byte[] bytes = new byte[3];
            bytes[0] = (byte)(0xb0 + channel);
            bytes[1] = (byte)ctrlNumber;
            bytes[2] = (byte)ctrlValue;
            return bytes;
        }

        public override string ToString()
        {
            return "Controller (" + channel + ") number = " + ctrlNumber + ", value = " + ctrlValue;
        }
    }

    public class PatchChangeMessage : ChannelMessage       //0xC0
    {
        public int patchNumber;

        public PatchChangeMessage(int channel, int num)
            : base(channel)
        {
            patchNumber = num;
        }

        public PatchChangeMessage(MidiInStream stream, int channel)
            : base(channel)
        {
            patchNumber = stream.getOne();
        }

        override public byte[] getDataBytes()
        {
            byte[] bytes = new byte[2];
            bytes[0] = (byte)(0xc0 + channel);
            bytes[1] = (byte)patchNumber;
            return bytes;
        }

        public override string ToString()
        {
            return "Patch Change (" + channel + ") number = " + patchNumber;
        }
    }

    public class ChannelPressureMessage : ChannelMessage       //0xD0
    {
        public int pressure;

        public ChannelPressureMessage(int channel, int press)
            : base(channel)
        {
            pressure = press;
        }

        public ChannelPressureMessage(MidiInStream stream, int channel)
            : base(channel)
        {
            pressure = stream.getOne();
        }

        override public byte[] getDataBytes()
        {
            byte[] bytes = new byte[2];
            bytes[0] = (byte)(0xd0 + channel);
            bytes[1] = (byte)pressure;
            return bytes;
        }
    }

    public class PitchWheelMessage : ChannelMessage     //0xE0
    {
        public int wheel;

        public PitchWheelMessage(int channel, int _wheel)
            : base(channel)
        {
            wheel = _wheel;
        }

        public PitchWheelMessage(MidiInStream stream, int channel)
            : base(channel)
        {
            int b1 = stream.getOne();
            int b2 = stream.getOne();
            wheel = b1 * 128 + b2;
        }

        override public byte[] getDataBytes()
        {
            byte[] bytes = new byte[3];
            bytes[0] = (byte)(0xe0 + channel);
            bytes[1] = (byte)(wheel / 128);
            bytes[2] = (byte)(wheel % 128);
            return bytes;
        }
    }

//-----------------------------------------------------------------------------
//  SYSTEM MESSAGES
//-----------------------------------------------------------------------------

    public class SysExMessage : Message
    {
        List<int> sysExData;

        public SysExMessage(MidiInStream stream)
            : base()
        {
            sysExData = new List<int>();
            int b1 = stream.getOne();
            while (b1 != 0xf7)
            {
                sysExData.Add(b1);
                b1 = stream.getOne();
            }            
        }

        override public byte[] getDataBytes()
        {
            byte[] bytes = new byte[sysExData.Count];
            for (int i = 0; i < sysExData.Count; i++)
            {
                bytes[i] = (byte)sysExData[i];
            }
            return bytes;
        }
    }

    public enum SYSTEMMESSAGE { 
        QUARTERFRAME = 0Xf1,        //f1
        SONGPOSITION,               //f2
        SONGSELECT,                 //f3
        UNDEFINED1,                 //f4
        UNDEFINED2,                 //f5
        TUNEREQUEST,                //f6
        SYSEXEND,                   //f7
        MIDICLOCK,                  //f8
        MIDITICK,                   //f9
        MIDISTART,                  //fa
        MIDICONTINUE,               //fb
        MIDISTOP,                   //fc
        UNDEFINED3,                 //fd
        ACTIVESENSE = 0xfe          //fe
    }; 

    public class SystemMessage : Message
    {
        SYSTEMMESSAGE msgtype;
        int value;

        public SystemMessage(MidiInStream stream, int status)
            : base()
        {
            msgtype = (SYSTEMMESSAGE)status;
            value = 0;
            switch (msgtype)
            {
                case SYSTEMMESSAGE.QUARTERFRAME :
                case SYSTEMMESSAGE.SONGSELECT :
                    value = stream.getOne();
                    break;
                case SYSTEMMESSAGE.SONGPOSITION:
                    int b1 = stream.getOne();
                    int b2 = stream.getOne();
                    value = b1 * 128 + b2;
                    break;
                default :
                    break;
            }        
        }

        int[] SysMsgLen = {1, 2, 3, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0};

        override public byte[] getDataBytes()
        {
            byte[] bytes = new byte[SysMsgLen[(byte)msgtype - 0xF0]];
            bytes[0] = (byte)msgtype;
            switch (msgtype)
            {
                case SYSTEMMESSAGE.QUARTERFRAME:
                case SYSTEMMESSAGE.SONGSELECT:
                    bytes[1] = (byte)value;
                    break;
                case SYSTEMMESSAGE.SONGPOSITION:
                    bytes[1] = (byte)(value / 128);
                    bytes[2] = (byte)(value % 128);
                    break;
                default:
                    break;
            }
            return bytes;
        }
    }

//-----------------------------------------------------------------------------
//  META MESSAGES
//-----------------------------------------------------------------------------

    //J Glatt's Midi file page describing defined meta messages: http://midi.teragonaudio.com/tech/midifile.htm

    //meta message base class
    public class MetaMessage : Message
    {
        public int datalen;

        public MetaMessage(MidiInStream stream)
            : base()
        {
            datalen = (int)stream.getVariableLengthVal();
        }
    }

    public class SequenceNumberMessage : MetaMessage    //0xff 0x00
    {
        int b1, b2;

        public SequenceNumberMessage(MidiInStream stream)
            : base(stream)
        {
            b1 = 0;
            b2 = 0;
            if (datalen > 0)
            {
                b1 = stream.getOne();
                b2 = stream.getOne();
            }
        }
    }

    public class TextMessage : MetaMessage      //0xff 0x01
    {
        String text;

        public TextMessage(MidiInStream stream)
            : base(stream)
        {
            text = stream.getString(datalen);
        }
    }

    public class CopyrightMessage : MetaMessage     //0xff 0x02
    {
        String copyright;

        public CopyrightMessage(MidiInStream stream)
            : base(stream)
        {
            copyright = stream.getString(datalen);
        }
    }

    public class TrackNameMessage : MetaMessage     //0xff 0x03
    {
        public String trackName;

        public TrackNameMessage(MidiInStream stream)
            : base(stream)
        {
            trackName = stream.getString(datalen);
        }
    }

    public class InstrumentMessage : MetaMessage    //0xff 0x04
    {
        public String instrumentName;

        public InstrumentMessage(MidiInStream stream)
            : base(stream)
        {
            instrumentName = stream.getString(datalen);
        }
    }

    public class LyricMessage : MetaMessage     //0xff 0x05
    {
        public String lyric;

        public LyricMessage(MidiInStream stream)
            : base(stream)
        {
            lyric = stream.getString(datalen);
        }
    }

    public class MarkerMessage : MetaMessage        //0xff 0x06
    {
        public String marker;

        public MarkerMessage(MidiInStream stream)
            : base(stream)
        {
            marker = stream.getString(datalen);
        }
    }

    public class CuePointMessage : MetaMessage      //0xff 0x07
    {
        public String cuePoint;

        public CuePointMessage(MidiInStream stream)
            : base(stream)
        {
            cuePoint = stream.getString(datalen);
        }
    }

    public class PatchNameMessage : MetaMessage        //0xff 0x08
    {
        public String patchName;

        public PatchNameMessage(MidiInStream stream)
            : base(stream)
        {
            patchName = stream.getString(datalen);
        }
    }

    public class DeviceNameMessage : MetaMessage        //0xff 0x09
    {
        public String deviceName;

        public DeviceNameMessage(MidiInStream stream)
            : base(stream)
        {
            deviceName = stream.getString(datalen);
        }
    }

    //obsolete
    public class MidiChannelMessage : MetaMessage       //0xff 0x20
    {
        int cc;

        public MidiChannelMessage(MidiInStream stream)
            : base(stream)
        {
            cc = stream.getOne();
        }
    }

    //obsolete
    public class MidiPortMessage : MetaMessage          //0xff 0x21
    {
        int pp;

        public MidiPortMessage(MidiInStream stream)
            : base(stream)
        {
            pp = stream.getOne();
        }
    }

    public class EndofTrackMessage : MetaMessage        //0xff 0x2f
    {

        public EndofTrackMessage(MidiInStream stream)
            : base(stream)
        {
            //length should be 0
        }

        public override string ToString()
        {
            return "End of Track";
        }
    }

    public class TempoMessage : MetaMessage             //0xff 0x51
    {
        public int tempo;
        public Timing timing;

        public TempoMessage(MidiInStream stream)
            : base(stream)
        {
            int b1 = stream.getOne();
            int b2 = stream.getOne();
            int b3 = stream.getOne();
            tempo = ((b1 * 0x100 + b2) * 0x100) + b3;
            timing = null;
        }

        public override string ToString()
        {
            return "Tempo = " + tempo + " at time = " + timing.microsec;
        }
    }

    public class SMPTEOffsetMessage : MetaMessage       //0xff 0x54
    {
        int hour, min, sec, frame, frame100;

        public SMPTEOffsetMessage(MidiInStream stream)
            : base(stream)
        {
            hour = stream.getOne();
            min = stream.getOne();
            sec = stream.getOne();
            frame = stream.getOne();
            frame100 = stream.getOne();
        }
    }

    public class TimeSignatureMessage : MetaMessage         //0xff 0x58
    {
        int numerator;
        int denominator;
        int clicks;
        int clocksPerQuarter;

        public TimeSignatureMessage(MidiInStream stream)
            : base(stream)
        {
            numerator = stream.getOne();
            int b1 = stream.getOne();
            denominator = (int)Math.Pow(2.0, b1);
            clicks = stream.getOne();
            clocksPerQuarter = stream.getOne();
        }

        public override string ToString()
        {
            return "Time Signature = " + numerator + "/" + denominator + " clicks = " + clicks + " clocks/quarter = " + clocksPerQuarter;
        }
    }

    public class KeySignatureMessage : MetaMessage          //0xff 0x59
    {
        int sf;
        int mi;

        public KeySignatureMessage(MidiInStream stream)
            : base(stream)
        {
            sf = stream.getOne();
            mi = stream.getOne();
        }
    }

    public class UnknownMetaMessage : MetaMessage
    {
        int msgtype;

        public UnknownMetaMessage(MidiInStream stream, int _msgtype)
            : base(stream)
        {
            msgtype = _msgtype;
            stream.skipBytes(datalen);            
        }
    }

}

//Console.WriteLine("there's no sun in the shadow of the wizard");
