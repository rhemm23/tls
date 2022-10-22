namespace TLS
{
    public class TlsPlaintext : IBufferableData
    {
        public ITlsContent Content { get; set; }
        public uint Size => Content.Size + 5;

        public TlsPlaintext(ITlsContent content)
        {
            Content = content;
        }
        
        public void Write(List<byte> output)
        {
            output.Add((byte)Content.ContentType);
            TlsProtocolVersion.TLS_1_3.Write(output);
            output.AddUShortBytes((ushort)Content.Size);
            Content.Write(output);
        }
    }
}

