using System;
using Jolt.Functional;
using NUnit.Framework;

namespace Jolt.Test.Functional
{
    [TestFixture]
    public sealed class CompositeTestFixture
    {
        [Test]
        public void Compose_2ArgFunc_With2ArgFunc_First()
        {
            var outer = Create2ArgFunc('|');
            var inner = Create2ArgFunc(',');

            var composed = Compose.First(outer, inner);

            Assert.That(composed("a", "b", "c"), Is.EqualTo("a,b|c"));
        }

        [Test]
        public void Compose_2ArgFunc_With2ArgFunc_Second()
        {
            var outer = Create2ArgFunc('|');
            var inner = Create2ArgFunc(',');

            var composed = Compose.Second(outer, inner);

            Assert.That(composed("a", "b", "c"), Is.EqualTo("a|b,c"));
        }

        [Test]
        public void Compose_2ArgFunc_With3ArgFunc_First()
        {
            var outer = Create2ArgFunc('|');
            var inner = Create3ArgFunc(',');

            var composed = Compose.First(outer, inner);

            Assert.That(composed("a", "b", "c", "d"), Is.EqualTo("a,b,c|d"));
        }

        [Test]
        public void Compose_2ArgFunc_With3ArgFunc_Second()
        {
            var outer = Create2ArgFunc('|');
            var inner = Create3ArgFunc(',');

            var composed = Compose.Second(outer, inner);

            Assert.That(composed("a", "b", "c", "d"), Is.EqualTo("a|b,c,d"));
        }

        [Test]
        public void Compose_2ArgFunc_With4ArgFunc_First()
        {
            var outer = Create2ArgFunc('|');
            var inner = Create4ArgFunc(',');

            var composed = Compose.First(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e"), Is.EqualTo("a,b,c,d|e"));
        }

        [Test]
        public void Compose_2ArgFunc_With4ArgFunc_Second()
        {
            var outer = Create2ArgFunc('|');
            var inner = Create4ArgFunc(',');

            var composed = Compose.Second(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e"), Is.EqualTo("a|b,c,d,e"));
        }

        [Test]
        public void Compose_3ArgFunc_With2ArgFunc_First()
        {
            var outer = Create3ArgFunc('|');
            var inner = Create2ArgFunc(',');

            var composed = Compose.First(outer, inner);

            Assert.That(composed("a", "b", "c", "d"), Is.EqualTo("a,b|c|d"));
        }

        [Test]
        public void Compose_3ArgFunc_With2ArgFunc_Second()
        {
            var outer = Create3ArgFunc('|');
            var inner = Create2ArgFunc(',');

            var composed = Compose.Second(outer, inner);

            Assert.That(composed("a", "b", "c", "d"), Is.EqualTo("a|b,c|d"));
        }

        [Test]
        public void Compose_3ArgFunc_With2ArgFunc_Third()
        {
            var outer = Create3ArgFunc('|');
            var inner = Create2ArgFunc(',');

            var composed = Compose.Third(outer, inner);

            Assert.That(composed("a", "b", "c", "d"), Is.EqualTo("a|b|c,d"));
        }

        [Test]
        public void Compose_3ArgFunc_With3ArgFunc_First()
        {
            var outer = Create3ArgFunc('|');
            var inner = Create3ArgFunc(',');

            var composed = Compose.First(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e"), Is.EqualTo("a,b,c|d|e"));
        }

        [Test]
        public void Compose_3ArgFunc_With3ArgFunc_Second()
        {
            var outer = Create3ArgFunc('|');
            var inner = Create3ArgFunc(',');

            var composed = Compose.Second(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e"), Is.EqualTo("a|b,c,d|e"));
        }

        [Test]
        public void Compose_3ArgFunc_With3ArgFunc_Third()
        {
            var outer = Create3ArgFunc('|');
            var inner = Create3ArgFunc(',');

            var composed = Compose.Third(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e"), Is.EqualTo("a|b|c,d,e"));
        }

        [Test]
        public void Compose_3ArgFunc_With4ArgFunc_First()
        {
            var outer = Create3ArgFunc('|');
            var inner = Create4ArgFunc(',');

            var composed = Compose.First(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f"), Is.EqualTo("a,b,c,d|e|f"));
        }

        [Test]
        public void Compose_3ArgFunc_With4ArgFunc_Second()
        {
            var outer = Create3ArgFunc('|');
            var inner = Create4ArgFunc(',');

            var composed = Compose.Second(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f"), Is.EqualTo("a|b,c,d,e|f"));
        }

        [Test]
        public void Compose_3ArgFunc_With4ArgFunc_Third()
        {
            var outer = Create3ArgFunc('|');
            var inner = Create4ArgFunc(',');

            var composed = Compose.Third(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f"), Is.EqualTo("a|b|c,d,e,f"));
        }

        [Test]
        public void Compose_4ArgFunc_With2ArgFunc_First()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create2ArgFunc(',');

            var composed = Compose.First(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e"), Is.EqualTo("a,b|c|d|e"));
        }

        [Test]
        public void Compose_4ArgFunc_With2ArgFunc_Second()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create2ArgFunc(',');

            var composed = Compose.Second(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e"), Is.EqualTo("a|b,c|d|e"));
        }

        [Test]
        public void Compose_4ArgFunc_With2ArgFunc_Third()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create2ArgFunc(',');

            var composed = Compose.Third(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e"), Is.EqualTo("a|b|c,d|e"));
        }

        [Test]
        public void Compose_4ArgFunc_With2ArgFunc_Fourth()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create2ArgFunc(',');

            var composed = Compose.Fourth(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e"), Is.EqualTo("a|b|c|d,e"));
        }

        [Test]
        public void Compose_4ArgFunc_With3ArgFunc_First()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create3ArgFunc(',');

            var composed = Compose.First(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f"), Is.EqualTo("a,b,c|d|e|f"));
        }

        [Test]
        public void Compose_4ArgFunc_With3ArgFunc_Second()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create3ArgFunc(',');

            var composed = Compose.Second(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f"), Is.EqualTo("a|b,c,d|e|f"));
        }

        [Test]
        public void Compose_4ArgFunc_With3ArgFunc_Third()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create3ArgFunc(',');

            var composed = Compose.Third(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f"), Is.EqualTo("a|b|c,d,e|f"));
        }

        [Test]
        public void Compose_4ArgFunc_With3ArgFunc_Fourth()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create3ArgFunc(',');

            var composed = Compose.Fourth(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f"), Is.EqualTo("a|b|c|d,e,f"));
        }

        [Test]
        public void Compose_4ArgFunc_With4ArgFunc_First()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create4ArgFunc(',');

            var composed = Compose.First(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f", "g"), Is.EqualTo("a,b,c,d|e|f|g"));
        }

        [Test]
        public void Compose_4ArgFunc_With4ArgFunc_Second()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create4ArgFunc(',');

            var composed = Compose.Second(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f", "g"), Is.EqualTo("a|b,c,d,e|f|g"));
        }

        [Test]
        public void Compose_4ArgFunc_With4ArgFunc_Third()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create4ArgFunc(',');

            var composed = Compose.Third(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f", "g"), Is.EqualTo("a|b|c,d,e,f|g"));
        }

        [Test]
        public void Compose_4ArgFunc_With4ArgFunc_Fourth()
        {
            var outer = Create4ArgFunc('|');
            var inner = Create4ArgFunc(',');

            var composed = Compose.Fourth(outer, inner);

            Assert.That(composed("a", "b", "c", "d", "e", "f", "g"), Is.EqualTo("a|b|c|d,e,f,g"));
        }

        private static Func<string, string, string> Create2ArgFunc(char separator) => (a, b) => $"{a}{separator}{b}";

        private static Func<string, string, string, string> Create3ArgFunc(char separator) => (a, b, c) => $"{a}{separator}{b}{separator}{c}";

        private static Func<string, string, string, string, string> Create4ArgFunc(char separator) => (a, b, c, d) => $"{a}{separator}{b}{separator}{c}{separator}{d}";
    }
}