using System;

namespace Faker.Core.Generator
{
    public class UInt64Generator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(ulong);
        }

        public object Generate(GeneratorContext context)
        {
            Span<byte> randomBytes = stackalloc byte[sizeof(ulong)];
            context.Random.NextBytes(randomBytes);
            var randomValue = BitConverter.ToUInt64(randomBytes);
            return randomValue;
        }
    }
}
