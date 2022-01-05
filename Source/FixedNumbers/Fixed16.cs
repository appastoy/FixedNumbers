using System;
using System.Collections.Generic;
using System.Text;

namespace FixedNumbers
{
	/// <summary>
	/// 32비트 고정 소수점
	/// <br/>정수부 유효구간 약 -2,000,000 ~ 2,000,000
	/// <br/>실수부 정밀도 소수점 셋째자리 (최저값 0.001)
	/// </summary>
	public readonly struct Fixed16 : IEquatable<Fixed16>, IComparable<Fixed16>
	{
		public const int FloatingBits = 7;

		public static readonly Fixed16 MaxValue = new Fixed16(short.MaxValue);
		public static readonly Fixed16 MinValue = new Fixed16(short.MinValue);
		public static readonly Fixed16 Zero = new Fixed16(0);
		public static readonly Fixed16 One = new Fixed16(1 << FloatingBits);
		public static readonly Fixed16 MinusOne = -One;
		public static readonly Fixed16 Half = new Fixed16(1 << (FloatingBits - 1));
		public static readonly Fixed16 MinusHalf = -Half;

		readonly short fixedValue;

		public float Value => FixedConversion.ConvertToFloat32(fixedValue, FloatingBits);

		internal Fixed16(float value) => fixedValue = (short)FixedConversion.ConvertToFixed(value, FloatingBits);
		internal Fixed16(int fixedValue) => this.fixedValue = (short)fixedValue;
		public bool Equals(Fixed16 other) => fixedValue == other.fixedValue;
		public override bool Equals(object obj) => obj is Fixed16 v && v.fixedValue == fixedValue;
		public override int GetHashCode() => fixedValue;
		public override string ToString() => Value.ToString();
		public int CompareTo(Fixed16 other) => fixedValue.CompareTo(other.fixedValue);

		public static implicit operator Fixed16(float value) => new Fixed16(value);
		public static implicit operator Fixed16(short value) => new Fixed16(value << FloatingBits);
		public static implicit operator float(Fixed16 value) => value.Value;
		public static explicit operator int(Fixed16 value) => value.fixedValue >> FloatingBits;
		public static implicit operator bool(Fixed16 lhs) => lhs.fixedValue != 0;
		public static Fixed16 operator -(Fixed16 value) => new Fixed16(-value.fixedValue);
		public static bool operator !(Fixed16 lhs) => lhs.fixedValue == 0;
		public static Fixed16 operator +(Fixed16 lhs, Fixed16 rhs) => new Fixed16(lhs.fixedValue + rhs.fixedValue);
		public static Fixed16 operator -(Fixed16 lhs, Fixed16 rhs) => new Fixed16(lhs.fixedValue - rhs.fixedValue);
		public static Fixed16 operator *(Fixed16 lhs, Fixed16 rhs) => new Fixed16((short)((lhs.fixedValue * rhs.fixedValue) >> FloatingBits));
		public static Fixed16 operator /(Fixed16 lhs, Fixed16 rhs) => new Fixed16((short)((lhs.fixedValue << FloatingBits) / rhs.fixedValue));
		public static bool operator ==(Fixed16 lhs, Fixed16 rhs) => lhs.fixedValue == rhs.fixedValue;
		public static bool operator !=(Fixed16 lhs, Fixed16 rhs) => lhs.fixedValue != rhs.fixedValue;
		public static bool operator <(Fixed16 lhs, Fixed16 rhs) => lhs.fixedValue < rhs.fixedValue;
		public static bool operator >(Fixed16 lhs, Fixed16 rhs) => lhs.fixedValue > rhs.fixedValue;
		public static bool operator <=(Fixed16 lhs, Fixed16 rhs) => lhs.fixedValue <= rhs.fixedValue;
		public static bool operator >=(Fixed16 lhs, Fixed16 rhs) => lhs.fixedValue >= rhs.fixedValue;
	}
}
