﻿using System;

namespace Faker.Core.Generator
{
    public class GeneratorContext
    {
        public Type TargetType { get; }
        public Random Random { get; }
        public IFaker Faker { get; }

        public GeneratorContext(Type targetType, Random random, IFaker faker)
        {
            TargetType = targetType;
            Random = random;
            Faker = faker;
        }
    }
}
