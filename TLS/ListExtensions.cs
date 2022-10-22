namespace TLS
{
    public static class ListExtensions
    {
        private static readonly Random s_random = new Random();

        public static void AddUShortBytes(this List<byte> bytes, ushort value)
        {
            bytes.Add((byte)(value >> 8));
            bytes.Add((byte)value);
        }

        public static void AddRandomBytes(this List<byte> bytes, int count)
        {
            byte[] buffer = new byte[count];
            s_random.NextBytes(buffer);
            bytes.AddRange(buffer);
        }
    }
}

