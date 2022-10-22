namespace TLS
{
    public class TlsNamedGroup : IBufferableData
    {
        public static readonly TlsNamedGroup SECP256R1 = new TlsNamedGroup(0x00, 0x17);
        public static readonly TlsNamedGroup SECP384R1 = new TlsNamedGroup(0x00, 0x18);
        public static readonly TlsNamedGroup SECP521R1 = new TlsNamedGroup(0x00, 0x19);
        public static readonly TlsNamedGroup X25519 = new TlsNamedGroup(0x00, 0x1D);
        public static readonly TlsNamedGroup X448 = new TlsNamedGroup(0x00, 0x1E);

        public uint Size => 2;
        public byte First { get; }
        public byte Second { get; }

        public TlsNamedGroup(byte first, byte second)
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

