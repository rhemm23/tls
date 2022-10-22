namespace TLS
{
    public interface IBufferableData
    {
        public uint Size { get; }

        public void Write(List<byte> output);
    }
}

