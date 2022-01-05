using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FixedNumbers.Tests")]
namespace FixedNumbers
{
	// https://nybounce.wordpress.com/2016/06/24/ieee-754-floating-point부동소수점-산술에-대하여/
	public readonly struct ieee754_Float32
	{
		// 부호
		public const uint SIGN_BIT_MASK = 0x80000000;

		// 가수부
		public const int MANTISSA_BIT_COUNT = 23;
		public const int MANTISSA_BIT_MASK = (1 << MANTISSA_BIT_COUNT) - 1;

		// 지수부
		// -127 ~ 128 의 표현 범위
		public const int EXPONENT_BIT_MASK = 0xff << MANTISSA_BIT_COUNT;

		// 바이어스 127을 더해줘서 0 ~ 255를 만든다.(0x7f) 범위
		public const int EXPONENT_BIAS = 127;

		// 실제 표현 가능한 범위는 -127 ~ 127이다.
		public const int EXPONENT_MIN = -127;
		public const int EXPONENT_MAX = 127;

		// 128은 바이어스를 더하면 255(0xff)가 되는데 이것은 특별한 경우를 위해 사용된다.
		// 지수부가 128인데 가수부가 0이면 INF(infinite: 무한대)이다.
		// 지수부가 128인데 가수부가 0이 아니면 NaN(Not a Number:유효하지 않음)이다.
		public const int EXPONENT_RESERVED = 128;

		public readonly uint RawValue;

		public unsafe float Value
		{
			get
			{
				fixed (uint* pRawValue = &RawValue)
					return *(float*)pRawValue;
			}
		}

		public bool IsNegative => (RawValue & SIGN_BIT_MASK) != 0;
		public int Exponent => (int)((RawValue & EXPONENT_BIT_MASK) >> MANTISSA_BIT_COUNT) - EXPONENT_BIAS;
		public int Mantissa => (int)(RawValue & MANTISSA_BIT_MASK);
		public bool IsZero => (RawValue & ~SIGN_BIT_MASK) == 0;
		public bool IsReserved => ((RawValue >> MANTISSA_BIT_COUNT) & 0xff) == 0xff;

		unsafe internal ieee754_Float32(float value)
        {
			RawValue = *(uint*)&value;
        }

		unsafe internal ieee754_Float32(bool isNegative, int exponent, int mantissa)
		{
			RawValue = (isNegative ? SIGN_BIT_MASK : 0) |
					   (uint)((exponent + EXPONENT_BIAS) << MANTISSA_BIT_COUNT) |
					   (uint)(mantissa & MANTISSA_BIT_MASK);
		}
	}
}
