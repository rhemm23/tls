namespace TLS
{
    public class TlsSupportedSignatureAlgorithmsExtension : ITlsExtensionContent
    {
        public TlsExtensionType ExtensionType => TlsExtensionType.SignatureAlgorithms;
        public uint Size => (uint)(2 + SupportedSignatureSchemes.Sum(s => s.Size));
        public List<TlsSignatureScheme> SupportedSignatureSchemes { get; }

        public TlsSupportedSignatureAlgorithmsExtension(List<TlsSignatureScheme> supportedSignatureSchemes)
        {
            SupportedSignatureSchemes = supportedSignatureSchemes;
        }

        public void Write(List<byte> output)
        {
            output.AddUShortBytes((ushort)SupportedSignatureSchemes.Sum(s => s.Size));
            foreach (TlsSignatureScheme signatureScheme in SupportedSignatureSchemes)
            {
                signatureScheme.Write(output);
            }
        }
    }
}

