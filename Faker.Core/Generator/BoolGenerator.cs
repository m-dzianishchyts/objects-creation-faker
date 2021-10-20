using System;

namespace Faker.Core.Generator
{
    public class BoolGenerator : IGenerator
    {
        internal const double TRUTHFULNESS_THRESHOLD = 0.5;
        
        public bool CanGenerate(Type type)
        {
            return type == typeof(bool);
        }

        public object Generate(GeneratorContext context)
        {
            double randomDouble = context.Random.NextDouble();
            bool randomValue = randomDouble < TRUTHFULNESS_THRESHOLD;
            return randomValue;
        }
    }
}
