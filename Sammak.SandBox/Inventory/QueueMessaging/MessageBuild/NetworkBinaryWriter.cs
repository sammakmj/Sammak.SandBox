// Decompiled with JetBrains decompiler
// Type: RabbitMQ.Util.NetworkBinaryWriter
// Assembly: RabbitMQ.Client, Version=3.6.9.0, Culture=neutral, PublicKeyToken=89e7d7c5feba84ce
// MVID: B73D5E8E-ACED-4E48-9574-CC706772F6CE
// Assembly location: C:\Users\jeff.sammak\.nuget\packages\rabbitmq.client\3.6.9\lib\net45\RabbitMQ.Client.dll

using System.IO;
using System.Text;

namespace Sammak.SandBox.Inventory.QueueMessaging.MessageBuild
{
    /// <summary>
    /// Subclass of BinaryWriter that writes integers etc in correct network order.
    /// </summary>
    /// <remarks>
    /// <p>
    /// Kludge to compensate for .NET's broken little-endian-only BinaryWriter.
    /// </p><p>
    /// See also NetworkBinaryReader.
    /// </p>
    /// </remarks>
    public class NetworkBinaryWriter : BinaryWriter
    {
        /// <summary>
        /// Construct a NetworkBinaryWriter over the given input stream.
        /// </summary>
        public NetworkBinaryWriter(Stream output)
          : base(output)
        {
        }

        /// <summary>
        /// Construct a NetworkBinaryWriter over the given input
        /// stream, reading strings using the given encoding.
        /// </summary>
        public NetworkBinaryWriter(Stream output, Encoding encoding)
          : base(output, encoding)
        {
        }

        /// <summary>Helper method for constructing a temporary
        /// BinaryWriter streaming into a fresh MemoryStream
        /// provisioned with the given initialSize.</summary>
        public static BinaryWriter TemporaryBinaryWriter(int initialSize)
        {
            return new BinaryWriter((Stream)new MemoryStream(initialSize));
        }

        /// <summary>Helper method for extracting the byte[] contents
        /// of a BinaryWriter over a MemoryStream, such as constructed
        /// by TemporaryBinaryWriter.</summary>
        public static byte[] TemporaryContents(BinaryWriter w)
        {
            return ((MemoryStream)w.BaseStream).ToArray();
        }

        /// <summary>Override BinaryWriter's method for network-order.</summary>
        public override void Write(short i)
        {
            this.Write((byte)(((int)i & 65280) >> 8));
            this.Write((byte)((uint)i & (uint)byte.MaxValue));
        }

        /// <summary>Override BinaryWriter's method for network-order.</summary>
        public override void Write(ushort i)
        {
            this.Write((byte)(((int)i & 65280) >> 8));
            this.Write((byte)((uint)i & (uint)byte.MaxValue));
        }

        /// <summary>Override BinaryWriter's method for network-order.</summary>
        public override void Write(int i)
        {
            this.Write((byte)(((long)i & 4278190080L) >> 24));
            this.Write((byte)((i & 16711680) >> 16));
            this.Write((byte)((i & 65280) >> 8));
            this.Write((byte)(i & (int)byte.MaxValue));
        }

        /// <summary>Override BinaryWriter's method for network-order.</summary>
        public override void Write(uint i)
        {
            this.Write((byte)((i & 4278190080U) >> 24));
            this.Write((byte)((i & 16711680U) >> 16));
            this.Write((byte)((i & 65280U) >> 8));
            this.Write((byte)(i & (uint)byte.MaxValue));
        }

        /// <summary>Override BinaryWriter's method for network-order.</summary>
        public override void Write(long i)
        {
            uint num1 = (uint)(i >> 32);
            uint num2 = (uint)i;
            this.Write(num1);
            this.Write(num2);
        }

        /// <summary>Override BinaryWriter's method for network-order.</summary>
        public override void Write(ulong i)
        {
            uint num1 = (uint)(i >> 32);
            uint num2 = (uint)i;
            this.Write(num1);
            this.Write(num2);
        }

        /// <summary>Override BinaryWriter's method for network-order.</summary>
        public override void Write(float f)
        {
            BinaryWriter w = NetworkBinaryWriter.TemporaryBinaryWriter(4);
            w.Write(f);
            byte[] numArray = NetworkBinaryWriter.TemporaryContents(w);
            this.Write(numArray[3]);
            this.Write(numArray[2]);
            this.Write(numArray[1]);
            this.Write(numArray[0]);
        }

        /// <summary>Override BinaryWriter's method for network-order.</summary>
        public override void Write(double d)
        {
            BinaryWriter w = NetworkBinaryWriter.TemporaryBinaryWriter(8);
            w.Write(d);
            byte[] numArray = NetworkBinaryWriter.TemporaryContents(w);
            this.Write(numArray[7]);
            this.Write(numArray[6]);
            this.Write(numArray[5]);
            this.Write(numArray[4]);
            this.Write(numArray[3]);
            this.Write(numArray[2]);
            this.Write(numArray[1]);
            this.Write(numArray[0]);
        }
    }
}
