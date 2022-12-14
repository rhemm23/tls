namespace TLS
{
    public enum TlsExtensionType : ushort
    {
        ServerName = 0,
        MaxFragmentLength = 1,
        ClientCertificateUrl = 2,
        TrustedCAKeys = 3,
        TruncatedHMAC = 4,
        StatusRequest = 5,
        UserMapping = 6,
        ClientAuthz = 7,
        ServerAuthz = 8,
        CertType = 9,
        SupportedGroups = 10,
        ECPointFormats = 11,
        SRP = 12,
        SignatureAlgorithms = 13,
        UseSRTP = 14,
        Heartbeat = 15,
        ApplicationLayerProtocolNegotiation = 16,
        StatusRequestV2 = 17,
        SignedCertificateTimestamp = 18,
        ClientCertificateType = 19,
        ServerCertificateType = 20,
        Padding = 21,
        EncryptThenMac = 22,
        ExtendedMasterSecret = 23,
        TokenBinding = 24,
        CachedInfo = 25,
        TLSLTS = 26,
        CompressCertificate = 27,
        RecordSizeLimit = 28,
        PwdProtect = 29,
        PwdClear = 30,
        PasswordSalt = 31,
        TicketPinning = 32,
        TLSCertWithExternPSK = 33,
        DelegatedCredentials = 34,
        SessionTicket = 35,
        TLMSP = 36,
        TLMSPProxying = 37,
        TLMSPDelegate = 38,
        SupportedEKTCiphers = 39,
        PreSharedKey = 41,
        EarlyData = 42,
        SupportedVersions = 43,
        Cookie = 44,
        PSKKeyExchangeModes = 45,
        CertificateAuthorities = 47,
        OidFilters = 48,
        PostHandshakeAuth = 49,
        SignatureAlgorithmsCert = 50,
        KeyShare = 51,
        TransparencyInfo = 52,
        ConnectionId = 54,
        ExternalIdHash = 55,
        ExternalSessionId = 56,
        QuicTransportParameters = 57,
        TicketRequest = 58,
        DNSSecChain = 59
    }
}

