namespace TLS
{
    public interface ITlsHandshakeContent : IBufferableData
    {
        public TlsHandshakeType HandshakeType { get; }
    }
}

