namespace TLS
{
    public class TlsCipherSuite : IBufferableData
    {
        public static readonly TlsCipherSuite TLS_AES_128_GCM_SHA256 = new TlsCipherSuite(0x13, 0x01);
        public static readonly TlsCipherSuite TLS_AES_256_GCM_SHA384 = new TlsCipherSuite(0x13, 0x02);
        public static readonly TlsCipherSuite TLS_CHACHA20_POLY1305_SHA256 = new TlsCipherSuite(0x13, 0x03);
        public static readonly TlsCipherSuite TLS_AES_128_CCM_SHA256 = new TlsCipherSuite(0x13, 0x04);
        public static readonly TlsCipherSuite TLS_AES_128_CCM_8_SHA256 = new TlsCipherSuite(0x13, 0x05);

        public uint Size => 2;

        public byte First { get; }
        public byte Second { get; }

        public TlsCipherSuite(byte first, byte second)
        {
            First = first;
            Second = second;
        }

        public void Write(List<byte> output)
        {
            output.Add(First);
            output.Add(Second);
        }
    }
}

