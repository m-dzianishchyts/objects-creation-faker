using System;

namespace Faker.Core.Generator
{
    public class SingleGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(float);
        }

        public object Generate(GeneratorContext context)
        {
            Span<byte> randomBytes = stackalloc byte[sizeof(float)];
            context.Random.NextBytes(randomBytes);
            var randomValue = BitConverter.ToSingle(randomBytes);
            return randomValue;
        }
    }
}