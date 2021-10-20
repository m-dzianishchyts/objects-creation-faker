using System;

namespace Faker.Core.Generator
{
    public class ByteGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(byte);
        }

        public object Generate(GeneratorContext context)
        {
            var randomValue = (byte) context.Random.Next();
            return randomValue;
        }
    }
}
