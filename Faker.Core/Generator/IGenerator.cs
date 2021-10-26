using System;

namespace Faker.Core.Generator
{
    public interface IGenerator
    {
        bool CanGenerate(Type type);

        object Generate(GeneratorContext context);
    }
}
