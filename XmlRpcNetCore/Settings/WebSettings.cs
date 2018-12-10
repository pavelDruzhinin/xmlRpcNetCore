using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace XmlRpcNetCore
{
    public class WebSettings
    {
        public bool AllowAutoRedirect { get; set; } = true;

#if (!COMPACT_FRAMEWORK && !SILVERLIGHT)
        [Browsable(false)]
        public X509CertificateCollection ClientCertificates { get; } = new X509CertificateCollection();
#endif

#if (!COMPACT_FRAMEWORK)
        public string ConnectionGroupName
        {
            get { return ConnectionGroupName1; }
            set { ConnectionGroupName1 = value; }
        }
#endif

        [Browsable(false)]
        public ICredentials Credentials { get; set; }

#if (!COMPACT_FRAMEWORK && !FX1_0)
        public bool EnableCompression { get; set; }
#endif

        [Browsable(false)]
        public WebHeaderCollection Headers { get; } = new WebHeaderCollection();

#if (!COMPACT_FRAMEWORK && !FX1_0)
        public bool Expect100Continue { get; set; }
#endif

        public CookieContainer CookieContainer { get; } = new CookieContainer();

        public bool KeepAlive { get; set; } = true;

        public bool PreAuthenticate { get; set; }

#if (!SILVERLIGHT)
        [Browsable(false)]
        public System.Version ProtocolVersion { get; set; } = HttpVersion.Version11;
#endif

#if (!SILVERLIGHT)
        [Browsable(false)]
        public IWebProxy Proxy { get; set; }
#endif

        public int Timeout { get; set; } = 100000;

        public string Url { get; set; }

#if (!COMPACT_FRAMEWORK && !FX1_0 && !SILVERLIGHT)
        public bool UseNagleAlgorithm { get; set; }
#endif

        public string UserAgent { get; set; } = "XML-RPC.NET";

        public string ConnectionGroupName1 { get => ConnectionGroupName2; set => ConnectionGroupName2 = value; }
        public string ConnectionGroupName2 { get; set; } = null;
    }
}
