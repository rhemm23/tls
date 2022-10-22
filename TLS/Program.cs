using System.Net.Sockets;
using TLS;

List<TlsCipherSuite> cipherSuites = new List<TlsCipherSuite>();
List<TlsExtension> extensions = new List<TlsExtension>();

extensions.Add(new TlsExtension(new TlsSupportedVersionsExtension(new List<TlsProtocolVersion>() { TlsProtocolVersion.TLS_1_3 })));
extensions.Add(new TlsExtension(new TlsSupportedGroupsExtension(new List<TlsNamedGroup>() { TlsNamedGroup.X25519, TlsNamedGroup.X448 })));
extensions.Add(new TlsExtension(new TlsSupportedSignatureAlgorithmsExtension(new List<TlsSignatureScheme>() { TlsSignatureScheme.ECDSA_SECP256R1_SHA256, TlsSignatureScheme.ECDSA_SECP384R1_SHA384, TlsSignatureScheme.ECDSA_SECP521R1_SHA512 })));
extensions.Add(new TlsExtension(new TlsServerNameExtension("www.google.com")));

cipherSuites.Add(TlsCipherSuite.TLS_AES_128_CCM_8_SHA256);
cipherSuites.Add(TlsCipherSuite.TLS_AES_128_CCM_SHA256);
cipherSuites.Add(TlsCipherSuite.TLS_AES_128_GCM_SHA256);
cipherSuites.Add(TlsCipherSuite.TLS_AES_256_GCM_SHA384);
cipherSuites.Add(TlsCipherSuite.TLS_CHACHA20_POLY1305_SHA256);

TlsClientHello hello = new TlsClientHello(cipherSuites, extensions);
ITlsContent content = new TlsHandshake(hello);
TlsPlaintext plaintext = new TlsPlaintext(content);

List<byte> packet = new List<byte>();
plaintext.Write(packet);

TcpClient tc = new TcpClient();
tc.Connect("www.google.com", 443);

using (NetworkStream ns = tc.GetStream())
{
    ns.Write(packet.ToArray(), 0, packet.Count);
    ns.Flush();

    byte[] buffer = new byte[17000];
    int bytes = ns.Read(buffer, 0, 17000);

    ITlsContent contentReceived = new TlsMessageReader(new List<byte>(buffer.Take(bytes))).Read();
    if (contentReceived is TlsAlert alert)
    {
        Console.WriteLine($"{alert.AlertLevel} - {alert.AlertDescription}");
    }
}

Console.ReadKey();
