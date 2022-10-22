using Org.BouncyCastle.Math.EC.Rfc7748;

namespace TLS.Cryptography
{
    public class KeyPair
    {
        public byte[] PrivateKey { get; }
        public byte[] PublicKey { get; }

        public KeyPair()
        {
            PrivateKey = EC.GeneratePrivateKey();
            PublicKey = new byte[32];
            X25519.ScalarMultBase(PrivateKey, 0, PublicKey, 0);
        }
    }
}

