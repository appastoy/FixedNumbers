using NUnit.Framework;

namespace FixedNumbers.Test
{
	public class ieee754
	{
		[Test]
		public void Zero()
		{
			var result = new ieee754_Float32(0f);
			Assert.That(result.IsNegative, Is.EqualTo(false));
			Assert.That(result.Exponent, Is.EqualTo(ieee754_Float32.EXPONENT_MIN));
			Assert.That(result.Mantissa, Is.EqualTo(0));
			Assert.That(result.Value, Is.EqualTo(0f));
		}

		[Test]
		public void NegativeZero()
		{
			var result = new ieee754_Float32(-0f);
			Assert.That(result.IsNegative, Is.EqualTo(true));
			Assert.That(result.Exponent, Is.EqualTo(ieee754_Float32.EXPONENT_MIN));
			Assert.That(result.Mantissa, Is.EqualTo(0));
			Assert.That(result.Value, Is.EqualTo(-0f));
		}

		[Test]
		public void One()
		{
			var result = new ieee754_Float32(1f);
			Assert.That(result.IsNegative, Is.EqualTo(false));
			Assert.That(result.Exponent, Is.EqualTo(0));
			Assert.That(result.Mantissa, Is.EqualTo(0));
			Assert.That(result.Value, Is.EqualTo(1f));
		}

		[Test]
		public void MinusOne()
		{
			var result = new ieee754_Float32(-1f);
			Assert.That(result.IsNegative, Is.EqualTo(true));
			Assert.That(result.Exponent, Is.EqualTo(0));
			Assert.That(result.Mantissa, Is.EqualTo(0));
			Assert.That(result.Value, Is.EqualTo(-1f));
		}

		[Test]
		public void NaN()
		{
			var result = new ieee754_Float32(float.NaN);
			Assert.That(result.IsNegative, Is.EqualTo(true));
			Assert.That(result.Exponent, Is.EqualTo(ieee754_Float32.EXPONENT_RESERVED));
			Assert.That(result.Mantissa, Is.Not.EqualTo(0));
			Assert.That(result.Value, Is.EqualTo(float.NaN));
		}

		[Test]
		public void PositiveInfinite()
		{
			var result = new ieee754_Float32(float.PositiveInfinity);
			Assert.That(result.IsNegative, Is.EqualTo(false));
			Assert.That(result.Exponent, Is.EqualTo(ieee754_Float32.EXPONENT_RESERVED));
			Assert.That(result.Mantissa, Is.EqualTo(0));
			Assert.That(result.Value, Is.EqualTo(float.PositiveInfinity));
		}

		[Test]
		public void NegativeInfinite()
		{
			var result = new ieee754_Float32(float.NegativeInfinity);
			Assert.That(result.IsNegative, Is.EqualTo(true));
			Assert.That(result.Exponent, Is.EqualTo(ieee754_Float32.EXPONENT_RESERVED));
			Assert.That(result.Mantissa, Is.EqualTo(0));
			Assert.That(result.Value, Is.EqualTo(float.NegativeInfinity));
		}
	}
}