namespace TLS
{
    public class TlsPSKExchangeModesExtension : ITlsExtensionContent
    {
        public TlsExtensionType ExtensionType => TlsExtensionType.PSKKeyExchangeModes;
        public List<TlsPSKExchangeMode> ExchangeModes { get; }
        public uint Size => (uint)(1 + ExchangeModes.Count);

        public TlsPSKExchangeModesExtension(List<TlsPSKExchangeMode> exchangeModes)
        {
            ExchangeModes = exchangeModes;
        }

        public void Write(List<byte> output)
        {
            output.Add((byte)ExchangeModes.Count);
            output.AddRange(ExchangeModes.Select(em => (byte)em));
        }
    }
}

