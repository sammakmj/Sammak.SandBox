// Decompiled with JetBrains decompiler
// Type: RabbitMQ.Client.Impl.Frame
// Assembly: RabbitMQ.Client, Version=3.6.9.0, Culture=neutral, PublicKeyToken=89e7d7c5feba84ce
// MVID: B73D5E8E-ACED-4E48-9574-CC706772F6CE
// Assembly location: C:\Users\jeff.sammak\.nuget\packages\rabbitmq.client\3.6.9\lib\net45\RabbitMQ.Client.dll

//using RabbitMQ.Client.Exceptions;
//using RabbitMQ.Util;
using System;
using System.IO;
using System.Net.Sockets;

namespace Sammak.SandBox.Inventory.QueueMessaging.MessageBuild
{
    public class Frame
    {
        public MemoryStream m_accumulator;

        public Frame()
        {
        }

        public Frame(int type, int channel)
        {
            this.Type = type;
            this.Channel = channel;
            this.Payload = (byte[])null;
            this.m_accumulator = new MemoryStream();
        }

        public Frame(int type, int channel, byte[] payload)
        {
            this.Type = type;
            this.Channel = channel;
            this.Payload = payload;
            this.m_accumulator = (MemoryStream)null;
        }

        public int Channel { get; set; }

        public byte[] Payload { get; set; }

        public int Type { get; set; }

        public static void ProcessProtocolHeader(NetworkBinaryReader reader)
        {
            try
            {
                if (reader.ReadByte() != (byte)77 || reader.ReadByte() != (byte)81 || reader.ReadByte() != (byte)80)
                    throw new Exception("Invalid AMQP protocol header from server");
                throw new Exception("PacketNotRecognizedException");
                //throw new PacketNotRecognizedException((int)reader.ReadByte(), (int)reader.ReadByte(), (int)reader.ReadByte(), (int)reader.ReadByte());
            }
            catch (EndOfStreamException ex)
            {
                throw new Exception("Invalid AMQP protocol header from server", ex);
            }
        }

        public static Frame ReadFrom(NetworkBinaryReader reader)
        {
            int type;
            try
            {
                type = (int)reader.ReadByte();
            }
            catch (IOException ex)
            {
                if (ex.InnerException == null || !(ex.InnerException is SocketException) || ((SocketException)ex.InnerException).SocketErrorCode != SocketError.TimedOut)
                    throw ex;
                throw ex.InnerException;
            }
            if (type == 65)
                Frame.ProcessProtocolHeader(reader);
            int channel = (int)reader.ReadUInt16();
            int count = reader.ReadInt32();
            byte[] payload = reader.ReadBytes(count);
            if (payload.Length != count)
                throw new Exception("Short frame - expected " + (object)count + " bytes, got " + (object)payload.Length + " bytes");
            int num = (int)reader.ReadByte();
            if (num != 206)
                throw new Exception("Bad frame end marker: " + (object)num);
            return new Frame(type, channel, payload);
        }

        public void FinishWriting()
        {
            if (this.m_accumulator == null)
                return;
            this.Payload = this.m_accumulator.ToArray();
            this.m_accumulator = (MemoryStream)null;
        }

        public NetworkBinaryReader GetReader()
        {
            return new NetworkBinaryReader((Stream)new MemoryStream(this.Payload));
        }

        public NetworkBinaryWriter GetWriter()
        {
            return new NetworkBinaryWriter((Stream)this.m_accumulator);
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("(type={0}, channel={1}, {2} bytes of payload)", (object)this.Type, (object)this.Channel, this.Payload == null ? (object)"(null)" : (object)this.Payload.Length.ToString());
        }

        public void WriteTo(NetworkBinaryWriter writer)
        {
            this.FinishWriting();
            writer.Write((byte)this.Type);
            writer.Write((ushort)this.Channel);
            writer.Write((uint)this.Payload.Length);
            writer.Write(this.Payload);
            writer.Write((byte)206);
        }
    }
}
