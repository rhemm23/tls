namespace TLS
{
    public class TlsKeyShareExtension : ITlsExtensionContent
    {
        public uint Size => (uint)(2 + KeyShareEntries.Sum(kv => kv.Value.Length + 4));
        public TlsExtensionType ExtensionType => TlsExtensionType.KeyShare;
        public Dictionary<TlsNamedGroup, byte[]> KeyShareEntries { get; }

        public TlsKeyShareExtension(Dictionary<TlsNamedGroup, byte[]> keyShareEntries)
        {
            KeyShareEntries = keyShareEntries;
        }

        public void Write(List<byte> output)
        {
            output.AddUShortBytes((ushort)KeyShareEntries.Sum(kv => kv.Value.Length + 4));
            foreach (KeyValuePair<TlsNamedGroup, byte[]> keyEntry in KeyShareEntries)
            {
                keyEntry.Key.Write(output);
                output.AddUShortBytes((ushort)keyEntry.Value.Length);
                output.AddRange(keyEntry.Value);
            }
        }
    }
}

