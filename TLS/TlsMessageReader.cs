namespace TLS
{
    public class TlsMessageReader
    {
        private List<byte> _data;

        public TlsMessageReader(List<byte> data)
        {
            _data = data;
        }

        public ITlsContent Read()
        {
            if (_data == null || _data.Count < 5)
            {
                throw new Exception("Insufficient data to parse TLS header");
            }

            TlsContentType contentType = (TlsContentType)_data[0];
            TlsProtocolVersion recordVersion = new TlsProtocolVersion(_data[1], _data[2]);
            ushort recordLength = (ushort)((_data[3] << 8) | _data[4]);

            if (_data.Count != recordLength + 5)
            {
                //throw new Exception("Record length in TLS header does not match the data present");
            }

            switch (contentType)
            {
                case TlsContentType.Alert:
                    return new TlsAlert((TlsAlertLevel)_data[5], (TlsAlertDescription)_data[6]);

                case TlsContentType.Handshake:
                    TlsHandshakeType handshakeType = (TlsHandshakeType)_data[5];
                    uint size = ((uint)_data[6] << 16) | ((uint)_data[7] << 8) | _data[8];
                    if (handshakeType == TlsHandshakeType.ServerHello)
                    {
                        Console.WriteLine("here");
                    }
                    throw new NotImplementedException();
            }
            return null;
        }
    }
}

