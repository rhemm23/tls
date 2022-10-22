namespace TLS
{
    public class TlsExtension : IBufferableData
    {
        public ITlsExtensionContent Content { get; }
        public uint Size => Content.Size + 4;

        public TlsExtension(ITlsExtensionContent content)
        {
            Content = content;
        }

        public void Write(List<byte> output)
        {
            output.AddUShortBytes((ushort)Content.ExtensionType);
            output.AddUShortBytes((ushort)Content.Size);

            Content.Write(output);
        }
    }
}

