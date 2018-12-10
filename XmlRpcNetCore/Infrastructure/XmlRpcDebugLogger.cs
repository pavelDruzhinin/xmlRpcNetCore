using System;
using System.Diagnostics;
using System.IO;

namespace XmlRpcNetCore
{
    public class XmlRpcDebugLogger : XmlRpcLogger
    {
        protected override void OnRequest(object sender, XmlRpcRequestEventArgs e)
        {
            DumpStream(e.RequestStream);
        }

        protected override void OnResponse(object sender, XmlRpcResponseEventArgs e)
        {
            DumpStream(e.ResponseStream);
        }

        private void DumpStream(Stream stm)
        {
            TextReader trdr = new StreamReader(stm);
            var s = trdr.ReadToEnd();
            Debug.WriteLine(s);
            stm.Position = 0;
        }
    }
}
