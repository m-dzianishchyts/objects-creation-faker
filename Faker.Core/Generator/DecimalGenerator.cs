using System;

namespace Faker.Core.Generator
{
    public class DecimalGenerator : IGenerator
    {
        private const int DECIMAL_MAX_SCALE = 28;

        public bool CanGenerate(Type type)
        {
            return type == typeof(decimal);
        }

        public object Generate(GeneratorContext context)
        {
            int decimalLow = GenerateInt32(context.Random);
            int decimalMid = GenerateInt32(context.Random);
            int decimalHigh = GenerateInt32(context.Random);
            bool decimalSign = GenerateBool(context.Random);
            var decimalScale = (byte) context.Random.Next(DECIMAL_MAX_SCALE + 1);
            var randomValue = new decimal(decimalLow, decimalMid, decimalHigh, decimalSign, decimalScale);
            return randomValue;
        }

        private static int GenerateInt32(Random random)
        {
            Span<byte> randomBytes = stackalloc byte[sizeof(int)];
            random.NextBytes(randomBytes);
            var randomValue = BitConverter.ToInt32(randomBytes);
            return randomValue;
        }

        private static bool GenerateBool(Random random)
        {
            double randomDouble = random.NextDouble();
            bool randomValue = randomDouble < BoolGenerator.TRUTHFULNESS_THRESHOLD;
            return randomValue;
        }
    }
}
