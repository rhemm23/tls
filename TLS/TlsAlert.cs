namespace TLS
{
    public class TlsAlert : ITlsContent
    {
        public TlsContentType ContentType => TlsContentType.Alert;
        public uint Size => 2;

        public TlsAlertDescription AlertDescription { get; }
        public TlsAlertLevel AlertLevel { get; }

        public TlsAlert(TlsAlertLevel alertLevel, TlsAlertDescription alertDescription)
        {
            AlertDescription = alertDescription;
            AlertLevel = alertLevel;
        }

        public void Write(List<byte> output)
        {
            output.Add((byte)AlertLevel);
            output.Add((byte)AlertDescription);
        }
    }
}

