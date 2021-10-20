using System;

namespace Faker.Core.Generator
{
    public class SByteGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(sbyte);
        }

        public object Generate(GeneratorContext context)
        {
            var randomValue = (sbyte) context.Random.Next();
            return randomValue;
        }
    }
}
