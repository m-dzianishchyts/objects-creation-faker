using System;

namespace Faker.Core.Generator
{
    public class CharGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(char);
        }

        public object Generate(GeneratorContext context)
        {
            var randomValue = (char) context.Random.Next(char.MinValue, char.MaxValue + 1);
            return randomValue;
        }
    }
}
