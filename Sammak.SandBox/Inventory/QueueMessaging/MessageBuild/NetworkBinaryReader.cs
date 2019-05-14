// Decompiled with JetBrains decompiler
// Type: RabbitMQ.Util.NetworkBinaryReader
// Assembly: RabbitMQ.Client, Version=3.6.9.0, Culture=neutral, PublicKeyToken=89e7d7c5feba84ce
// MVID: B73D5E8E-ACED-4E48-9574-CC706772F6CE
// Assembly location: C:\Users\jeff.sammak\.nuget\packages\rabbitmq.client\3.6.9\lib\net45\RabbitMQ.Client.dll

using System.IO;
using System.Text;

namespace Sammak.SandBox.Inventory.QueueMessaging.MessageBuild
{
    /// <summary>
    /// Subclass of BinaryReader that reads integers etc in correct network order.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Kludge to compensate for .NET's broken little-endian-only BinaryReader.
    /// Relies on BinaryReader always being little-endian.
    /// </para>
    /// </remarks>
    public class NetworkBinaryReader : BinaryReader
    {
        /// <summary>
        /// Construct a NetworkBinaryReader over the given input stream.
        /// </summary>
        public NetworkBinaryReader(Stream input)
          : base(input)
        {
        }

        /// <summary>
        /// Construct a NetworkBinaryReader over the given input
        /// stream, reading strings using the given encoding.
        /// </summary>
        public NetworkBinaryReader(Stream input, Encoding encoding)
          : base(input, encoding)
        {
        }

        /// <summary>Helper method for constructing a temporary
        /// BinaryReader over a byte[].</summary>
        public static BinaryReader TemporaryBinaryReader(byte[] bytes)
        {
            return new BinaryReader((Stream)new MemoryStream(bytes));
        }

        /// <summary>Override BinaryReader's method for network-order.</summary>
        public override double ReadDouble()
        {
            byte[] bytes = this.ReadBytes(8);
            byte num1 = bytes[0];
            bytes[0] = bytes[7];
            bytes[7] = num1;
            byte num2 = bytes[1];
            bytes[1] = bytes[6];
            bytes[6] = num2;
            byte num3 = bytes[2];
            bytes[2] = bytes[5];
            bytes[5] = num3;
            byte num4 = bytes[3];
            bytes[3] = bytes[4];
            bytes[4] = num4;
            return NetworkBinaryReader.TemporaryBinaryReader(bytes).ReadDouble();
        }

        /// <summary>Override BinaryReader's method for network-order.</summary>
        public override short ReadInt16()
        {
            uint num = (uint)base.ReadUInt16();
            return (short)((int)((num & 65280U) >> 8) | ((int)num & (int)byte.MaxValue) << 8);
        }

        /// <summary>Override BinaryReader's method for network-order.</summary>
        public override int ReadInt32()
        {
            uint num = base.ReadUInt32();
            return (int)((num & 4278190080U) >> 24) | (int)((num & 16711680U) >> 8) | ((int)num & 65280) << 8 | ((int)num & (int)byte.MaxValue) << 24;
        }

        /// <summary>Override BinaryReader's method for network-order.</summary>
        public override long ReadInt64()
        {
            ulong num = base.ReadUInt64();
            return (long)((num & 18374686479671623680UL) >> 56) | (long)((num & 71776119061217280UL) >> 40) | (long)((num & 280375465082880UL) >> 24) | (long)((num & 1095216660480UL) >> 8) | ((long)num & 4278190080L) << 8 | ((long)num & 16711680L) << 24 | ((long)num & 65280L) << 40 | ((long)num & (long)byte.MaxValue) << 56;
        }

        /// <summary>Override BinaryReader's method for network-order.</summary>
        public override float ReadSingle()
        {
            byte[] bytes = this.ReadBytes(4);
            byte num1 = bytes[0];
            bytes[0] = bytes[3];
            bytes[3] = num1;
            byte num2 = bytes[1];
            bytes[1] = bytes[2];
            bytes[2] = num2;
            return NetworkBinaryReader.TemporaryBinaryReader(bytes).ReadSingle();
        }

        /// <summary>Override BinaryReader's method for network-order.</summary>
        public override ushort ReadUInt16()
        {
            uint num = (uint)base.ReadUInt16();
            return (ushort)((num & 65280U) >> 8 | (uint)(((int)num & (int)byte.MaxValue) << 8));
        }

        /// <summary>Override BinaryReader's method for network-order.</summary>
        public override uint ReadUInt32()
        {
            uint num = base.ReadUInt32();
            return (uint)((int)((num & 4278190080U) >> 24) | (int)((num & 16711680U) >> 8) | ((int)num & 65280) << 8 | ((int)num & (int)byte.MaxValue) << 24);
        }

        /// <summary>Override BinaryReader's method for network-order.</summary>
        public override ulong ReadUInt64()
        {
            ulong num = base.ReadUInt64();
            return (ulong)((long)((num & 18374686479671623680UL) >> 56) | (long)((num & 71776119061217280UL) >> 40) | (long)((num & 280375465082880UL) >> 24) | (long)((num & 1095216660480UL) >> 8) | ((long)num & 4278190080L) << 8 | ((long)num & 16711680L) << 24 | ((long)num & 65280L) << 40 | ((long)num & (long)byte.MaxValue) << 56);
        }
    }
}
