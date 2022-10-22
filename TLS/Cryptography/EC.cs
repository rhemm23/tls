using Org.BouncyCastle.Security;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace TLS.Cryptography
{
    public static class EC
    {
        private static readonly SecureRandom s_secureRandom = new SecureRandom();

        public static byte[] GeneratePrivateKey()
        {
            byte[] bytes = new byte[32];
            X25519.GeneratePrivateKey(s_secureRandom, bytes);
            return bytes;
        }
    }
}

