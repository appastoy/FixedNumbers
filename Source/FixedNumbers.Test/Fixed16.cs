using NUnit.Framework;
using System;

namespace FixedNumbers.Tests
{
    public class Fixed16Tests
    {
        [Test]
        public void Contants()
        {
            Assert.That(Fixed16.Zero.Value, Is.EqualTo(0f));
            Assert.That(Fixed16.One.Value, Is.EqualTo(1f));
            Assert.That(((Fixed16)2f).Value, Is.EqualTo(2f));
            Assert.That(Fixed16.MinusOne.Value, Is.EqualTo(-1f));
            Assert.That(((Fixed16)(-2f)).Value, Is.EqualTo(-2f));
            Assert.That(Fixed16.Half.Value, Is.EqualTo(0.5f));
            Assert.That(((Fixed16)0.25f).Value, Is.EqualTo(0.25f));
            Assert.That(Fixed16.MinusHalf.Value, Is.EqualTo(-0.5f));
            Assert.That(((Fixed16)(-0.25f)).Value, Is.EqualTo(-0.25f));
        }

        [Test]
        public void ArithmaticOperations()
        {
            Assert.That(((Fixed16)0.5f + 0.5f).Value, Is.EqualTo(1f));
            Assert.That(((Fixed16)0.5f - 0.5f).Value, Is.EqualTo(0f));
            Assert.That(((Fixed16)0.5f * 0.5f).Value, Is.EqualTo(0.25f));
            Assert.That(((Fixed16)0.5f / 0.5f).Value, Is.EqualTo(1f));

            Assert.That(((Fixed16)2.5f + 4.25f).Value, Is.EqualTo(6.75f));
            Assert.That(((Fixed16)2.5f - 4.25f).Value, Is.EqualTo(-1.75f));
            Assert.That(((Fixed16)2.5f * 4.25f).Value, Is.EqualTo(10.625f));
            Assert.That(((Fixed16)(-4.25f) / 0.125f).Value, Is.EqualTo(-34f));
        }

        [Test]
        public void LogicalOperations()
        {
            Assert.That((Fixed16)0.25f == 0.5f, Is.False);
            Assert.That((Fixed16)0.5f == 0.5f, Is.True);
            Assert.That((Fixed16)0.5f != 0.5f, Is.False);
            Assert.That((Fixed16)0.5f != 0.25f, Is.True);

            Assert.That((Fixed16)0.5f > 0.75f, Is.False);
            Assert.That((Fixed16)0.75f > 0.5f, Is.True);
            Assert.That((Fixed16)0.5f >= 0.75f, Is.False);
            Assert.That((Fixed16)0.5f >= 0.5f, Is.True);

            Assert.That((Fixed16)0.5f < 0.75f, Is.True);
            Assert.That((Fixed16)0.75f < 0.5f, Is.False);
            Assert.That((Fixed16)0.5f <= 0.75f, Is.True);
            Assert.That((Fixed16)0.75f <= 0.5f, Is.False);
        }

        [Test]
        public void Limit()
        {
            Assert.Fail();
        }
    }
}
