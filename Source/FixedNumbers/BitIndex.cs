namespace FixedNumbers
{
	readonly struct BitIndex
    {
        readonly byte[] firstBitIndex;

        public int this[int index]
        {
            get => firstBitIndex[index];
        }

        public BitIndex(int count)
        {
            firstBitIndex = new byte[count];
            for (int i = 0; i < firstBitIndex.Length; i++)
            {
                byte index = 0;
                int n = i;
                if ((n & 0xf0) != 0)
                {
                    index += 4;
                    n >>= 4;
                }

                if ((n & 0xc) != 0)
                {
                    index += 2;
                    n >>= 2;
                }

                if ((n & 0x2) != 0)
                {
                    index += 1;
                    n >>= 1;
                }

                if (n != 0)
                {
                    index += 1;
                }

                firstBitIndex[i] = index;
            }
        }
    }
}
