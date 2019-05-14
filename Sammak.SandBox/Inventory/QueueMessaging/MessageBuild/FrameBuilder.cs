using System;
using System.Collections.Generic;
using System.Text;

namespace Sammak.SandBox.Inventory.QueueMessaging.MessageBuild
{
    public class FrameBuilder
    {
        public List<Frame> Build(byte[] body)
        {
            var channelNumber = 1;
            ushort heartbeat = 1;

            Frame frame1 = new Frame(1, channelNumber);
            //NetworkBinaryWriter writer1 = frame1.GetWriter();
            //writer1.Write((ushort)this.Method.ProtocolClassId);
            //writer1.Write((ushort)this.Method.ProtocolMethodId);
            //MethodArgumentWriter writer2 = new MethodArgumentWriter(writer1);
            //this.Method.WriteArgumentsTo(writer2);
            //writer2.Flush();
            List<Frame> frameList = new List<Frame>();
            frameList.Add(frame1);
            if (true /*this.Method.HasContent*/)
            {
                //byte[] body = this.Body;
                Frame frame2 = new Frame(2, channelNumber);
                //NetworkBinaryWriter writer3 = frame2.GetWriter();
                //writer3.Write((ushort)this.Header.ProtocolClassId);
                //this.Header.WriteTo(writer3, (ulong)body.Length);
                frameList.Add(frame2);
                int num1 = (int)Math.Min((uint)int.MaxValue, heartbeat);
                int num2 = num1 == 0 ? body.Length : num1 - 8;
                int index = 0;
                while (index < body.Length)
                {
                    int num3 = body.Length - index;
                    Frame frame3 = new Frame(3, channelNumber);
                    frame3.GetWriter().Write(body, index, num3 < num2 ? num3 : num2);
                    frameList.Add(frame3);
                    index += num2;
                }
            }

            return frameList;
        }
    }
}
