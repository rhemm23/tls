using System;

namespace TLS
{
    public class TlsServerHello : ITlsHandshakeContent
    {
        public List<ITlsExtensionContent> Extensions { get; }
        public TlsCipherSuite CipherSuite { get; }

        public TlsServerHello(TlsCipherSuite cipherSuite, List<ITlsExtensionContent> extensions)
        {
            CipherSuite = cipherSuite;
            Extensions = extensions;
        }

        public TlsHandshakeType HandshakeType => TlsHandshakeType.ServerHello;
        public uint Size => throw new NotImplementedException();

        public void Write(List<byte> output)
        {
            throw new NotImplementedException();
        }
    }
}

