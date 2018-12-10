using System.Collections.Generic;

namespace XmlRpcNetCore
{
    public class MappingStack : Stack<string>
    {
        public MappingStack(string parseType)
        {
            MappingType = parseType;
        }

        void Push(string str)
        {
            base.Push(str);
        }

        public string MappingType { get; } = string.Empty;
    }
}
