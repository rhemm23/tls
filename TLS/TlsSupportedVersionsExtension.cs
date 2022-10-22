namespace TLS
{
    public class TlsSupportedVersionsExtension : ITlsExtensionContent
    {
        public TlsExtensionType ExtensionType => TlsExtensionType.SupportedVersions;
        public uint Size => (uint)(1 + SupportedVersions.Sum(s => s.Size));
        public List<TlsProtocolVersion> SupportedVersions { get; }

        public TlsSupportedVersionsExtension(List<TlsProtocolVersion> supportedVersions)
        {
            SupportedVersions = supportedVersions;
        }

        public void Write(List<byte> output)
        {
            output.Add((byte)SupportedVersions.Sum(s => s.Size));
            foreach (TlsProtocolVersion tlsProtocolVersion in SupportedVersions)
            {
                tlsProtocolVersion.Write(output);
            }
        }
    }
}

