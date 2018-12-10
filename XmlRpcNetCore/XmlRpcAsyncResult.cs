/* 
XML-RPC.NET library
Copyright (c) 2001-2011, Charles Cook <charlescook@cookcomputing.com>

Permission is hereby granted, free of charge, to any person 
obtaining a copy of this software and associated documentation 
files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice shall be 
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
DEALINGS IN THE SOFTWARE.
*/

namespace XmlRpcNetCore
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;

    public class XmlRpcAsyncResult : IAsyncResult
    {
        public XmlRpcFormatSettings XmlRpcFormatSettings { get; private set; }

        // IAsyncResult members
        public object AsyncState { get; }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                bool completed = IsCompleted;
                if (manualResetEvent == null)
                {
                    lock (this)
                    {
                        if (manualResetEvent == null)
                            manualResetEvent = new ManualResetEvent(completed);
                    }
                }
                if (!completed && IsCompleted)
                    manualResetEvent.Set();
                return manualResetEvent;
            }
        }

        public bool CompletedSynchronously
        {
            get { return completedSynchronously; }
            set
            {
                if (completedSynchronously)
                    completedSynchronously = value;
            }
        }

        public bool IsCompleted { get; private set; }

        public CookieCollection ResponseCookies
        {
            get { return _responseCookies; }
        }

        public WebHeaderCollection ResponseHeaders
        {
            get { return _responseHeaders; }
        }

        // public members
        public void Abort()
        {
            if (Request != null)
                Request.Abort();
        }

        public Exception Exception { get; private set; }

        public XmlRpcClientProtocol ClientProtocol { get; }

        //internal members
        internal XmlRpcAsyncResult(XmlRpcClientProtocol ClientProtocol, XmlRpcRequest XmlRpcReq, XmlRpcFormatSettings xmlRpcFormatSettings, WebRequest Request,
          AsyncCallback UserCallback, object UserAsyncState, int retryNumber)
        {
            XmlRpcRequest = XmlRpcReq;
            this.ClientProtocol = ClientProtocol;
            this.Request = Request;
            AsyncState = UserAsyncState;
            userCallback = UserCallback;
            completedSynchronously = true;
            XmlRpcFormatSettings = xmlRpcFormatSettings;
        }

        internal void Complete(Exception ex)
        {
            Exception = ex;
            Complete();
        }

        internal void Complete()
        {
            try
            {
                if (ResponseStream != null)
                {
                    ResponseStream.Close();
                    ResponseStream = null;
                }
                if (ResponseBufferedStream != null)
                    ResponseBufferedStream.Position = 0;
            }
            catch (Exception ex)
            {
                if (Exception == null)
                    Exception = ex;
            }
            IsCompleted = true;
            try
            {
                if (manualResetEvent != null)
                    manualResetEvent.Set();
            }
            catch (Exception ex)
            {
                if (Exception == null)
                    Exception = ex;
            }

            userCallback?.Invoke(this);
        }

        internal WebResponse WaitForResponse()
        {
            if (!IsCompleted)
                AsyncWaitHandle.WaitOne();

            if (Exception != null)
                throw Exception;

            return Response;
        }

        internal bool EndSendCalled { get; set; }

        internal byte[] Buffer { get; set; }

        internal WebRequest Request { get; }

        internal WebResponse Response { get; set; }

        internal Stream ResponseStream { get; set; }

        internal XmlRpcRequest XmlRpcRequest { get; set; }

        internal Stream ResponseBufferedStream { get; set; }

        private AsyncCallback userCallback;
        bool completedSynchronously;
        ManualResetEvent manualResetEvent;
        internal CookieCollection _responseCookies;
        internal WebHeaderCollection _responseHeaders;
    }
}