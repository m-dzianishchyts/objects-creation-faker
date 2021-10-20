using System;

namespace Faker.Core.Generator
{
    public class UInt16Generator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(ushort);
        }

        public object Generate(GeneratorContext context)
        {
            Span<byte> randomBytes = stackalloc byte[sizeof(ushort)];
            context.Random.NextBytes(randomBytes);
            var randomValue = BitConverter.ToUInt16(randomBytes);
            return randomValue;
        }
    }
}
