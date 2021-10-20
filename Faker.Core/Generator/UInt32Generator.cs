using System;

namespace Faker.Core.Generator
{
    public class UInt32Generator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(uint);
        }

        public object Generate(GeneratorContext context)
        {
            Span<byte> randomBytes = stackalloc byte[sizeof(uint)];
            context.Random.NextBytes(randomBytes);
            var randomValue = BitConverter.ToUInt32(randomBytes);
            return randomValue;
        }
    }
}
