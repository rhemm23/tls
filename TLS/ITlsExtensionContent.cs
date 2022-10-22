namespace TLS
{
    public interface ITlsExtensionContent : IBufferableData
    {
        public TlsExtensionType ExtensionType { get; }
    }
}

