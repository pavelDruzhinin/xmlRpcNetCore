# Xml-Rpc.NET Core

Hi all!

I fork **Xml-Rpc.NET** project(http://xml-rpc.net/) to remake it for .NET Core, 
but .Net Core doesn't support System.Remoting(https://github.com/dotnet/corefx/issues/14303).

Well, I commented out lines in files `XmlRpcClientFormatterSinkProvider.cs`, `XmlRpcClientFormatterSink.cs` and `XmlRpcProxyGen.cs`. 

You can use `HttpWebRequest` for requests to server and classes `XmlRpcRequestSerializer`,
`XmlRpcResponseDeserializer` for serialization

**Good luck!**
