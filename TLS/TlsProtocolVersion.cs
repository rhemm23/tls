namespace TLS
{
    public class TlsProtocolVersion : IBufferableData
    {
        public static readonly TlsProtocolVersion TLS_1_3 = new TlsProtocolVersion(0x03, 0x04);
        public static readonly TlsProtocolVersion TLS_1_2 = new TlsProtocolVersion(0x03, 0x03);

        public byte Major { get; set; }
        public byte Minor { get; set; }

        public uint Size => 2;

        public TlsProtocolVersion(byte major, byte minor)
        {
            Major = major;
            Minor = minor;
        }

        public void Write(List<byte> output)
        {
            output.Add(Major);
            output.Add(Minor);
        }
    }
}

