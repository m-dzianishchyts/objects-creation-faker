using System;

namespace Faker.Core.Generator
{
    public class Int64Generator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(long);
        }

        public object Generate(GeneratorContext context)
        {
            Span<byte> randomBytes = stackalloc byte[sizeof(long)];
            context.Random.NextBytes(randomBytes);
            var randomValue = BitConverter.ToInt64(randomBytes);
            return randomValue;
        }
    }
}
