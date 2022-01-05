using System;

namespace FixedNumbers
{
	/// <summary>
	/// 32비트 고정 소수점
	/// <br/>정수부 유효구간 약 -2,000,000 ~ 2,000,000
	/// <br/>실수부 정밀도 소수점 셋째자리 (최저값 0.001)
	/// </summary>
	public readonly struct Fixed32 : IEquatable<Fixed32>, IComparable<Fixed32>
	{
		public const int FloatingBits = 10;

		public static readonly Fixed32 MaxValue = new Fixed32(int.MaxValue);
		public static readonly Fixed32 MinValue = new Fixed32(int.MinValue);
		public static readonly Fixed32 Zero = new Fixed32(0);
		public static readonly Fixed32 One = new Fixed32(1 << FloatingBits);
		public static readonly Fixed32 MinusOne = -One;
		public static readonly Fixed32 Half = new Fixed32(1 << (FloatingBits-1));
		public static readonly Fixed32 MinusHalf = -Half;

		readonly int fixedValue;

		public float Value => FixedConversion.ConvertToFloat32(fixedValue, FloatingBits);
		
		internal Fixed32(float value) => fixedValue = FixedConversion.ConvertToFixed(value, FloatingBits);
		internal Fixed32(int fixedValue) => this.fixedValue = fixedValue;
        public bool Equals(Fixed32 other) => fixedValue == other.fixedValue;
		public override bool Equals(object obj) => obj is Fixed32 v && v.fixedValue == fixedValue;
		public override int GetHashCode() => fixedValue;
		public override string ToString() => Value.ToString();
		public int CompareTo(Fixed32 other) => fixedValue.CompareTo(other.fixedValue);

		public static implicit operator Fixed32(float value) => new Fixed32(value);
		public static implicit operator Fixed32(int value) => new Fixed32(value << FloatingBits);
		public static implicit operator float(Fixed32 value) => value.Value;
		public static explicit operator int(Fixed32 value) => value.fixedValue >> FloatingBits;
		public static implicit operator bool(Fixed32 lhs) => lhs.fixedValue != 0;
		public static Fixed32 operator -(Fixed32 value) => new Fixed32(-value.fixedValue);
		public static bool operator !(Fixed32 lhs) => lhs.fixedValue == 0;
		public static Fixed32 operator +(Fixed32 lhs, Fixed32 rhs) => new Fixed32(lhs.fixedValue + rhs.fixedValue);
		public static Fixed32 operator -(Fixed32 lhs, Fixed32 rhs) => new Fixed32(lhs.fixedValue - rhs.fixedValue);
		public static Fixed32 operator *(Fixed32 lhs, Fixed32 rhs) => new Fixed32((int)(((long)lhs.fixedValue * rhs.fixedValue) >> FloatingBits));
		public static Fixed32 operator /(Fixed32 lhs, Fixed32 rhs) => new Fixed32((int)(((long)lhs.fixedValue << FloatingBits) / rhs.fixedValue));
		public static bool operator ==(Fixed32 lhs, Fixed32 rhs) => lhs.fixedValue == rhs.fixedValue;
		public static bool operator !=(Fixed32 lhs, Fixed32 rhs) => lhs.fixedValue != rhs.fixedValue;
		public static bool operator <(Fixed32 lhs, Fixed32 rhs) => lhs.fixedValue < rhs.fixedValue;
		public static bool operator >(Fixed32 lhs, Fixed32 rhs) => lhs.fixedValue > rhs.fixedValue;
		public static bool operator <=(Fixed32 lhs, Fixed32 rhs) => lhs.fixedValue <= rhs.fixedValue;
		public static bool operator >=(Fixed32 lhs, Fixed32 rhs) => lhs.fixedValue >= rhs.fixedValue;
	}
}
