﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Firefly.Http;

namespace Firefly.Tests.Fakes
{
    public class FakeOutput
    {
        public FakeOutput()
        {
            MemoryStream = new MemoryStream();
            Encoding = Encoding.UTF8;
        }

        public bool ProduceData(ArraySegment<byte> data, Action resume)
        {
            MemoryStream.Write(data.Array, data.Offset, data.Count);
            return false;
        }

        public void ProduceEnd(ProduceEndType produceEndType)
        {
            if (produceEndType == ProduceEndType.SocketShutdownSend)
                ShutdownSend = true;
            else
                Ended = true;

            if (produceEndType == ProduceEndType.ConnectionKeepAlive)
                KeepAlive = true;
            else if (produceEndType == ProduceEndType.SocketDisconnect)
                KeepAlive = false;
        }


        public bool ShutdownSend { get; set; }
        public bool Ended { get; set; }
        public bool KeepAlive { get; set; }
        public MemoryStream MemoryStream { get; set; }
        public Encoding Encoding { get; set; }

        public string Text
        {
            get { return Encoding.GetString(MemoryStream.ToArray()); }
        }
    }
}