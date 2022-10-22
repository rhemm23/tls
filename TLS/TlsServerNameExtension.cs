namespace TLS
{
    public class TlsServerNameExtension : ITlsExtensionContent
    {
        public TlsExtensionType ExtensionType => TlsExtensionType.ServerName;
        public uint Size => (uint)(HostName.Length + 5);
        public string HostName { get; }

        public TlsServerNameExtension(string hostName)
        {
            HostName = hostName;
        }

        public void Write(List<byte> output)
        {
            output.AddUShortBytes((ushort)(HostName.Length + 3));
            output.Add((byte)TlsNameType.HostName);
            output.AddUShortBytes((ushort)HostName.Length);
            output.AddRange(HostName.Select(s => (byte)s));
        }
    }
}

