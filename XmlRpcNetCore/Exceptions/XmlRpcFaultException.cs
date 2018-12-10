/* 
XML-RPC.NET library
Copyright (c) 2001-2006, Charles Cook <charlescook@cookcomputing.com>

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
#if (!COMPACT_FRAMEWORK)
    using System.Runtime.Serialization;
#endif

    // used to return server-side errors to client code - also can be 
    // thrown by Service implmentation code to return custom Fault Responses
#if (!COMPACT_FRAMEWORK && !SILVERLIGHT)
    [Serializable]
#endif
    public class XmlRpcFaultException :
#if (!SILVERLIGHT)
    ApplicationException
#else
    Exception
#endif
    {
        // constructors
        //
        public XmlRpcFaultException(int theCode, string theString)
          : base("Server returned a fault exception: [" + theCode.ToString() +
                  "] " + theString)
        {
            FaultCode = theCode;
            FaultString = theString;
        }
#if (!COMPACT_FRAMEWORK && !SILVERLIGHT)
        // deserialization constructor
        protected XmlRpcFaultException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
            FaultCode = (int)info.GetValue("m_faultCode", typeof(int));
            FaultString = (string)info.GetValue("m_faultString", typeof(string));
        }
#endif
        // properties
        //
        public int FaultCode { get; }

        public string FaultString { get; }
#if (!COMPACT_FRAMEWORK && !SILVERLIGHT)
        // public methods
        //
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("m_faultCode", FaultCode);
            info.AddValue("m_faultString", FaultString);
            base.GetObjectData(info, context);
        }

#endif
    }
}
