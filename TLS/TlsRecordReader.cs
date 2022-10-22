using System;
using System.IO;

namespace TLS
{
    public class TlsRecordReader : BinaryReader
    {
        public TlsRecordReader(Stream input) : base(input) { }

        public TlsExtensionType ReadExtensionType()
        {
            return (TlsExtensionType)ReadUShort();
        }

        public TlsNamedGroup ReadNamedGroup()
        {
            byte first = ReadByte();
            byte second = ReadByte();
            return new TlsNamedGroup(first, second);
        }

        public TlsCipherSuite ReadCipherSuite()
        {
            byte first = ReadByte();
            byte second = ReadByte();
            return new TlsCipherSuite(first, second);
        }

        public TlsProtocolVersion ReadProtocolVersion()
        {
            byte major = ReadByte();
            byte minor = ReadByte();
            return new TlsProtocolVersion(major, minor);
        }

        public uint ReadUInt24()
        {
            byte[] bytes = new byte[4];
            bytes[0] = 0x00;
            bytes[1] = ReadByte();
            bytes[2] = ReadByte();
            bytes[3] = ReadByte();

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes);
        }

        public int ReadInt() => BitConverter.ToInt32(ReadBigEndianBytes(4));
        public uint ReadUInt() => BitConverter.ToUInt32(ReadBigEndianBytes(4));

        public short ReadShort() => BitConverter.ToInt16(ReadBigEndianBytes(2));
        public ushort ReadUShort() => BitConverter.ToUInt16(ReadBigEndianBytes(2));

        public ITlsContent ReadRecord()
        {
            TlsContentType contentType = (TlsContentType)ReadByte();
            TlsProtocolVersion protocolVersion = ReadProtocolVersion();
            ushort length = ReadUShort();

            switch (contentType)
            {
                case TlsContentType.Alert:
                    return ReadAlert();

                case TlsContentType.Handshake:
                    return new TlsHandshake(ReadHandshake());

                case TlsContentType.ApplicationData:
                case TlsContentType.ChangeCipherSpec:

                default:
                    throw new Exception("Invalid record type encountered");
            }
        }

        public TlsAlert ReadAlert()
        {
            TlsAlertLevel alertLevel = (TlsAlertLevel)ReadByte();
            TlsAlertDescription alertDescription = (TlsAlertDescription)ReadByte();
            return new TlsAlert(alertLevel, alertDescription);
        }

        public ITlsHandshakeContent ReadHandshake()
        {
            TlsHandshakeType handshakeType = (TlsHandshakeType)ReadByte();
            uint handshakeLength = ReadUInt24();

            switch (handshakeType)
            {
                case TlsHandshakeType.ServerHello:
                    return ReadServerHello();

                default:
                    throw new NotImplementedException();
            }
        }

        public TlsServerHello ReadServerHello()
        {
            TlsProtocolVersion serverProtocol = ReadProtocolVersion();

            byte[] randomBytes = ReadBytes(32);
            byte legacySessionIdLength = ReadByte();
            byte[] legacySessionId = ReadBytes(legacySessionIdLength);

            TlsCipherSuite chosenCipherSuite = ReadCipherSuite();
            List<ITlsExtensionContent> extensions = new List<ITlsExtensionContent>();

            byte legacyCompression = ReadByte();
            int extensionLength = ReadUShort();

            while (extensionLength > 0)
            {
                TlsExtensionType extensionType = (TlsExtensionType)ReadUShort();
                ushort dataLength = ReadUShort();

                switch (extensionType)
                {
                    case TlsExtensionType.KeyShare:
                        extensions.Add(ReadKeyShareExtension());
                        break;

                    case TlsExtensionType.SupportedVersions:
                        extensions.Add(ReadSupportedVersionsExtension());
                        break;

                    default:
                        throw new Exception("Unrecognized extension");
                }

                extensionLength -= (4 + dataLength);
            }

            return new TlsServerHello(chosenCipherSuite, extensions);
        }

        private TlsSupportedVersionsExtension ReadSupportedVersionsExtension()
        {
            List<TlsProtocolVersion> supportedVersions = new List<TlsProtocolVersion>();
            int size = ReadUShort();

            while (size > 0)
            {
                supportedVersions.Add(ReadProtocolVersion());
                size -= 2;
            }

            return new TlsSupportedVersionsExtension(supportedVersions);
        }

        private TlsKeyShareExtension ReadKeyShareExtension()
        {
            Dictionary<TlsNamedGroup, byte[]> keyEntries = new Dictionary<TlsNamedGroup, byte[]>();
            TlsNamedGroup namedGroup = ReadNamedGroup();
            ushort keyLength = ReadUShort();
            byte[] key = ReadBytes(keyLength);
            keyEntries.Add(namedGroup, key);
            return new TlsKeyShareExtension(keyEntries);
        }

        private byte[] ReadBigEndianBytes(int count)
        {
            byte[] result = ReadBytes(count);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(result);
            }
            return result;
        }
    }
}

