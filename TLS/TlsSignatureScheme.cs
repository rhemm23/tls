namespace TLS
{
    public class TlsSignatureScheme : IBufferableData
    {
        public static readonly TlsSignatureScheme RSA_PKCS1_SHA256 = new TlsSignatureScheme(0x04, 0x01);
        public static readonly TlsSignatureScheme RSA_PKCS1_SHA384 = new TlsSignatureScheme(0x05, 0x01);
        public static readonly TlsSignatureScheme RSA_PKCS1_SHA512 = new TlsSignatureScheme(0x06, 0x01);

        public static readonly TlsSignatureScheme ECDSA_SECP256R1_SHA256 = new TlsSignatureScheme(0x04, 0x03);
        public static readonly TlsSignatureScheme ECDSA_SECP384R1_SHA384 = new TlsSignatureScheme(0x05, 0x03);
        public static readonly TlsSignatureScheme ECDSA_SECP521R1_SHA512 = new TlsSignatureScheme(0x06, 0x03);

        public uint Size => 2;
        public byte First { get; }
        public byte Second { get; }

        public TlsSignatureScheme(byte first, byte second)
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

