namespace TLS
{
    public enum TlsContentType : byte
    {
        Invalid = 0,
        ChangeCipherSpec = 20,
        Alert = 21,
        Handshake = 22,
        ApplicationData = 23,
        Heartbeat = 24
    }
}

