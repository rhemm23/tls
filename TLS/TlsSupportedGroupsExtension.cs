namespace TLS
{
    public class TlsSupportedGroupsExtension : ITlsExtensionContent
    {
        public TlsExtensionType ExtensionType => TlsExtensionType.SupportedGroups;
        public uint Size => (uint)(2 + SupportedGroups.Sum(s => s.Size));
        public List<TlsNamedGroup> SupportedGroups { get; }

        public TlsSupportedGroupsExtension(List<TlsNamedGroup> supportedGroups)
        {
            SupportedGroups = supportedGroups;
        }

        public void Write(List<byte> output)
        {
            output.AddUShortBytes((ushort)SupportedGroups.Sum(s => s.Size));
            foreach (TlsNamedGroup supportedGroup in SupportedGroups)
            {
                supportedGroup.Write(output);
            }
        }
    }
}

