namespace TLS
{
    public class TlsHandshake : ITlsContent
    {
        public TlsContentType ContentType => TlsContentType.Handshake;
        public ITlsHandshakeContent Content { get; }
        public uint Size => Content.Size + 4;

        public TlsHandshake(ITlsHandshakeContent content)
        {
            Content = content;
        }

        public void Write(List<byte> output)
        {
            output.Add((byte)Content.HandshakeType);
            output.Add((byte)(Content.Size >> 16));
            output.Add((byte)(Content.Size >> 8));
            output.Add((byte)Content.Size);

            Content.Write(output);
        }
    }
}

