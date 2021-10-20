using System;

namespace Faker.Core.Generator
{
    public class Int32Generator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        public object Generate(GeneratorContext context)
        {
            Span<byte> randomBytes = stackalloc byte[sizeof(int)];
            context.Random.NextBytes(randomBytes);
            var randomValue = BitConverter.ToInt32(randomBytes);
            return randomValue;
        }
    }
}
