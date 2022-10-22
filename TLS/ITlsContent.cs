namespace TLS
{
    public interface ITlsContent : IBufferableData
    {
        public TlsContentType ContentType { get; }
    }
}

