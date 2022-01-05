namespace FixedNumbers
{
    // http://arkainoh.blogspot.com/2017/11/fixed-point.html
    internal static class FixedConversion
	{
		static readonly BitIndex bitIndex = new BitIndex(256);

        public static int ConvertToFixed(float value, int floatBitCount)
        {
            var ieee_float = new ieee754_Float32(value);
            if (ieee_float.IsZero)
                return 0;

            if (ieee_float.IsReserved)
                throw new System.ArgumentException("NaN or Infinity can NOT convert to Fixed32.");

            var fixedResult = ieee_float.Mantissa | 1 << ieee754_Float32.MANTISSA_BIT_COUNT;

            // 실수 비트 수
            var fraction = ieee754_Float32.MANTISSA_BIT_COUNT - ieee_float.Exponent;

            // 만약 실수 비트수가 결과값의 비트수를 넘어가면,
            if (fraction > 32)
            {
                // 오프셋만큼 가수부를 밀어주고, 실수 비트는 결과값 비트수로 변경한다.
                var offset = fraction - 32;
                fixedResult >>= offset;
                fraction = 32;
            }

            // 지정한 실수 비트 수와의 차이만큼 교정해준다.
            var offsetFloatBitCount = fraction - floatBitCount;
            fixedResult >>= offsetFloatBitCount;

            // 음수면, 2의 보수로 음수화 한다.
            if (ieee_float.IsNegative)
                fixedResult = ~fixedResult + 1;

            return fixedResult;
        }

        public static float ConvertToFloat32(int fixedValue, int floatBitCount)
        {
             var isNegative = fixedValue < 0;
            if (isNegative)
            {
                fixedValue = -fixedValue;
            }

            var index = FindFirstActiveBitIndex((uint)fixedValue);
            var exponent = fixedValue == 0 ? -ieee754_Float32.EXPONENT_BIAS : index - floatBitCount - 1;
            var fraction = ieee754_Float32.MANTISSA_BIT_COUNT - index + 1;
            var mantissa = fraction < 0 ? fixedValue >> -fraction : fixedValue << fraction;
            var floatValue = new ieee754_Float32(isNegative, exponent, mantissa);

            return floatValue.Value;
        }

        public static int FindFirstActiveBitIndex(uint value)
        {
            var index = 0;
            if (value == 0)
            {
                return index;
            }

            if ((value & 0xffff0000) != 0)
            {
                index += 16;
                value >>= 16;
            }

            if ((value & 0xff00) != 0)
            {
                index += 8;
                value >>= 8;
            }

            return bitIndex[(int)value] + index;
        }
    }
}
