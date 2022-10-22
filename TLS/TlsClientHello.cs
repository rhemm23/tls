namespace TLS
{
    public class TlsClientHello : ITlsHandshakeContent
    {
        public TlsHandshakeType HandshakeType => TlsHandshakeType.ClientHello;
        public uint Size => (uint)(73 + CipherSuites.Sum(s => s.Size) + Extensions.Sum(s => s.Size));

        public List<TlsCipherSuite> CipherSuites { get; }
        public List<TlsExtension> Extensions { get; }

        public TlsClientHello(List<TlsCipherSuite> cipherSuites, List<TlsExtension> extensions)
        {
            CipherSuites = cipherSuites;
            Extensions = extensions;
        }

        public void Write(List<byte> output)
        {
            TlsProtocolVersion.TLS_1_2.Write(output);

            output.AddRandomBytes(32);
            output.Add(32);
            output.AddRandomBytes(32);

            output.AddUShortBytes((ushort)CipherSuites.Sum(s => s.Size));
            foreach (TlsCipherSuite tlsCipherSuite in CipherSuites)
            {
                tlsCipherSuite.Write(output);
            }

            output.Add(0x01);
            output.Add(0x00);

            output.AddUShortBytes((ushort)Extensions.Sum(s => s.Size));
            foreach (TlsExtension extension in Extensions)
            {
                extension.Write(output);
            }
        }
    }
}

