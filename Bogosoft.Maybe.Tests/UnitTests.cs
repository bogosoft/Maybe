using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace Bogosoft.Maybe.Tests
{
    [TestFixture, Category("Unit")]
    public class UnitTests
    {
        [TestCase]
        public void AttemptingToGetValueFromEmptyMaybeThrowsInvalidOperationException()
        {
            Exception exception = null;

            try
            {
                var test = Maybe<string>.Empty.Value;
            }
            catch(Exception e)
            {
                exception = e;
            }

            Assert.That(exception, Is.Not.Null);

            Assert.That(exception, Is.TypeOf<InvalidOperationException>());

            Assert.AreEqual(exception.Message, Message.NoValue);
        }

        [TestCase]
        public void CanCreateMaybeFluently()
        {
            var maybe = "Hello, World!".Maybe();

            Assert.That(maybe, Is.TypeOf<Maybe<string>>());
        }

        [TestCase]
        public void CanGetAlternateValueFromEmptyMaybe()
        {
            var maybe = Maybe<DateTimeOffset?>.Empty;

            Assert.That(maybe.HasValue, Is.False);

            var now = DateTimeOffset.Now;

            Assert.AreEqual(maybe.Or(now), now);
        }

        [TestCase]
        public void CanLosslesslySerializeAndDeserailizeMaybeWithoutValue()
        {
            var original = new Maybe<DateTime?>();

            Assert.That(original.HasValue, Is.False);

            Assert.That(original.ValueOrDefault, Is.Null);

            var serialized = JsonConvert.SerializeObject(original);

            var deserialized = JsonConvert.DeserializeObject<Maybe<DateTime?>>(serialized);

            Assert.That(deserialized.HasValue, Is.False);

            Assert.That(deserialized.ValueOrDefault, Is.Null);

            Assert.That(deserialized, Is.EqualTo(original));

            Assert.That(deserialized == original, Is.True);
        }

        [TestCase]
        public void CanLosslesslySerializeAndDeserializeMaybeWithValue()
        {
            var maybe = DateTime.Now.Maybe();

            Assert.That(maybe, Is.EqualTo(JsonConvert.DeserializeObject<Maybe<DateTime>>(JsonConvert.SerializeObject(maybe))));
        }

        [TestCase]
        public void Equality()
        {
            var number = 1337;

            Assert.That(number == number.Maybe(), Is.True);

            Assert.That(number != number.Maybe(), Is.False);

            Assert.That(number.Maybe() == number, Is.True);

            Assert.That(number.Maybe() != number, Is.False);

            Assert.That(number.Maybe() == number.Maybe(), Is.True);

            Assert.That(number.Maybe() != number.Maybe(), Is.False);

            Assert.That(number.Maybe().Equals(number), Is.True);

            Assert.That(number.Maybe().Equals(number.Maybe()), Is.True);

            string empty = null;

            Assert.That(empty == empty.Maybe(), Is.True);

            Assert.That(empty != empty.Maybe(), Is.False);

            Assert.That(empty.Maybe() == empty, Is.True);

            Assert.That(empty.Maybe() != empty, Is.False);

            Assert.That(empty.Maybe() == empty.Maybe(), Is.True);

            Assert.That(empty.Maybe() != empty.Maybe(), Is.False);

            Assert.That(empty.Maybe().Equals(empty), Is.True);

            Assert.That(empty.Maybe().Equals(empty.Maybe()), Is.True);
        }

        [TestCase]
        public void Inequality()
        {
            var one = 1;
            var two = 2;

            Assert.That(one == two.Maybe(), Is.False);

            Assert.That(one != two.Maybe(), Is.True);

            Assert.That(one.Maybe() == two, Is.False);

            Assert.That(one.Maybe() != two, Is.True);

            Assert.That(two.Maybe() == one, Is.False);

            Assert.That(two.Maybe() != one, Is.True);

            Assert.That(one.Maybe() == two.Maybe(), Is.False);

            Assert.That(one.Maybe() != two.Maybe(), Is.True);

            Assert.That(one.Maybe().Equals(two.Maybe()), Is.False);

            string empty = null;
            string hello = "Hello, World!";

            Assert.That(empty == hello.Maybe(), Is.False);

            Assert.That(empty != hello.Maybe(), Is.True);

            Assert.That(hello == empty.Maybe(), Is.False);

            Assert.That(hello != empty.Maybe(), Is.True);

            Assert.That(hello.Maybe() == empty.Maybe(), Is.False);

            Assert.That(hello.Maybe() != empty.Maybe(), Is.True);

            Assert.That(hello.Maybe().Equals(empty.Maybe()), Is.False);

            Assert.That(empty.Maybe().Equals(hello.Maybe()), Is.False);
        }

        [TestCase]
        public void NullReferenceCreatesEmptyMaybe()
        {
            string name = null;

            var maybe = name.Maybe();

            Assert.That(maybe.HasValue, Is.False);
        }

        [TestCase]
        public void TwoEmptyMayBesAreEquivalent()
        {
            object obj1 = null;
            object obj2 = null;

            Assert.That(obj1.Maybe().Equals(obj2.Maybe()), Is.True);
        }
    }
}