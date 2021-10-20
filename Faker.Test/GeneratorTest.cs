using System;
using Faker.Core.Generator;
using NUnit.Framework;

namespace Faker.Test
{
    [TestFixture]
    public class GeneratorTest
    {
        private GeneratorContext _generatorContext;

        private static TestCaseData[] s_generatorTestData =
        {
            new(new BoolGenerator(), typeof(bool)),
            new(new ByteGenerator(), typeof(byte)),
            new(new SByteGenerator(), typeof(sbyte)),
            new(new CharGenerator(), typeof(char)),
            new(new DecimalGenerator(), typeof(decimal)),
            new(new DoubleGenerator(), typeof(double)),
            new(new SingleGenerator(), typeof(float)),
            new(new Int32Generator(), typeof(int)),
            new(new UInt32Generator(), typeof(uint)),
            new(new Int64Generator(), typeof(long)),
            new(new UInt64Generator(), typeof(ulong)),
            new(new Int16Generator(), typeof(short)),
            new(new UInt16Generator(), typeof(ushort)),

            new(new StringGenerator(), typeof(string)),
            new(new DateTimeGenerator(), typeof(DateTime)),
        };

        [OneTimeSetUp]
        public void BoolGenerator_SetUp()
        {
            _generatorContext = new GeneratorContext(new Random(), new Core.Faker());
        }

        [Test]
        [TestCaseSource(nameof(s_generatorTestData))]
        public void Generator_CanGenerate_ExpectedType(IGenerator generator, Type expectedType)
        {
            Assert.IsTrue(generator.CanGenerate(expectedType));
        }

        [Test]
        [TestCaseSource(nameof(s_generatorTestData))]
        public void Generator_Generates_ExpectedType(IGenerator generator, Type expectedType)
        {
            object value = generator.Generate(_generatorContext);
            Assert.AreEqual(expectedType, value.GetType());
        }
    }
}
