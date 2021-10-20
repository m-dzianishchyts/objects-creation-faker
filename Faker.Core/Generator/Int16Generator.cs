using System;

namespace Faker.Core.Generator
{
    public class Int16Generator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(short);
        }

        public object Generate(GeneratorContext context)
        {
            Span<byte> randomBytes = stackalloc byte[sizeof(short)];
            context.Random.NextBytes(randomBytes);
            var randomValue = BitConverter.ToInt16(randomBytes);
            return randomValue;
        }
    }
}
