using System.Text;
using System.Xml;
using System.IO;

namespace XmlRpcNetCore
{
    public static class XmlRpcXmlWriter
    {
        public static XmlWriter Create(Stream stm, XmlRpcFormatSettings settings)
        {
            var stmWriter = new EncodingStreamWriter(stm, settings.XmlEncoding);
            var xtw = XmlWriter.Create(stmWriter, ConfigureXmlFormat(settings));
            return xtw;
        }

        private static XmlWriterSettings ConfigureXmlFormat(XmlRpcFormatSettings settings)
        {
            if (settings.UseIndentation)

                return new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = new string(' ', settings.Indentation),
                    Encoding = settings.XmlEncoding,
                    NewLineHandling = NewLineHandling.None,
                    OmitXmlDeclaration = settings.OmitXmlDeclaration,
                };

            return new XmlWriterSettings
            {
                Indent = false,
                Encoding = settings.XmlEncoding,
                OmitXmlDeclaration = settings.OmitXmlDeclaration,
            };
        }

        private class EncodingStreamWriter : StreamWriter
        {
            private Encoding _encoding;

            public EncodingStreamWriter(Stream stm, Encoding encoding)
              : base(stm)
            {
                _encoding = encoding;
            }

            public override Encoding Encoding
            {
                get { return _encoding; }
            }
        }
    }
}
