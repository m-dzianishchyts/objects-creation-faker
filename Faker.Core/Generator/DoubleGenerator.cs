using System;

namespace Faker.Core.Generator
{
    public class DoubleGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(double);
        }

        public object Generate(GeneratorContext context)
        {
            Span<byte> randomBytes = stackalloc byte[sizeof(double)];
            context.Random.NextBytes(randomBytes);
            var randomValue = BitConverter.ToDouble(randomBytes);
            return randomValue;
        }
    }
}