using Newtonsoft.Json;
using NUnit.Framework;
using Should;
using System;

namespace Bogosoft.Maybe.Tests
{
    [TestFixture, Category("Unit")]
    public class UnitTests
    {
        [TestCase]
        public void AttemptingToGetValueFromEmptyMayBeThrowsInvalidOperationException()
        {
            Exception exception = null;

            try
            {
                var test = MayBe<int>.Empty.Value;
            }
            catch(Exception e)
            {
                exception = e;
            }

            exception.ShouldNotBeNull();

            exception.ShouldBeType<InvalidOperationException>();

            exception.Message.ShouldEqual(Message.NoValue);
        }

        [TestCase]
        public void CanCreateMayBeFluently()
        {
            var maybe = "Hello, World!".Maybe();

            maybe.ShouldBeType<MayBe<string>>();
        }

        [TestCase]
        public void CanGetAlternateValueFromEmptyMayBe()
        {
            var maybe = MayBe<DateTimeOffset>.Empty;

            maybe.HasValue.ShouldBeFalse();

            var now = DateTimeOffset.Now;

            maybe.Or(now).ShouldEqual(now);
        }

        [TestCase]
        public void CanLosslesslySerializeAndDeserailizeMayBeWithoutValue()
        {
            var original = new MayBe<DateTime>();

            original.HasValue.ShouldBeFalse();

            original.ValueOrDefault.ShouldEqual(default(DateTime));

            var serialized = JsonConvert.SerializeObject(original);

            var deserialized = JsonConvert.DeserializeObject<MayBe<DateTime>>(serialized);

            deserialized.HasValue.ShouldBeFalse();

            deserialized.ValueOrDefault.ShouldEqual(default(DateTime));

            deserialized.ShouldEqual(original);
        }

        [TestCase]
        public void CanLosslesslySerializeAndDeserializeMayBeWithValue()
        {
            var maybe = DateTime.Now.Maybe();

            maybe.ShouldEqual(JsonConvert.DeserializeObject<MayBe<DateTime>>(JsonConvert.SerializeObject(maybe)));
        }

        [TestCase]
        public void CovarianceWorks()
        {
            var salutation = "Hello, World!";

            var derived = salutation.Maybe();

            IMayBe<object> generic = derived;

            generic.ToString().ShouldEqual(derived.ToString());
        }

        [TestCase]
        public void Equality()
        {
            var number = 1337;

            (number.Maybe() == number).ShouldBeTrue();

            (number.Maybe() != number).ShouldBeFalse();

            (number.Maybe() == number.Maybe()).ShouldBeTrue();

            (number.Maybe() != number.Maybe()).ShouldBeFalse();
        }

        [TestCase]
        public void Inequality()
        {
            var one = 1;
            var two = 2;

            (one.Maybe() == two).ShouldBeFalse();

            (one.Maybe() != two).ShouldBeTrue();

            (two.Maybe() == one).ShouldBeFalse();

            (two.Maybe() != one).ShouldBeTrue();

            (one.Maybe() == two.Maybe()).ShouldBeFalse();

            (one.Maybe() != two.Maybe()).ShouldBeTrue();
        }

        [TestCase]
        public void NullReferenceCreatesEmptyMayBe()
        {
            Person person = null;

            var maybe = person.Maybe();

            maybe.HasValue.ShouldBeFalse();
        }

        [TestCase]
        public void TwoEmptyMayBesAreEquivalent()
        {
            object obj1 = null;
            object obj2 = null;

            obj1.Maybe().Equals(obj2.Maybe()).ShouldBeTrue();
        }
    }
}