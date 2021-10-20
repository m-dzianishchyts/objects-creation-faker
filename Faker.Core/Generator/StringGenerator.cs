﻿using System;
using Faker.Core.Generator;

namespace Faker.Core.Generator
{
    public class StringGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        public object Generate(GeneratorContext context)
        {
            var stringLength = (byte) context.Random.Next();
            byte[] randomBytes = new byte[stringLength * sizeof(char)];
            context.Random.NextBytes(randomBytes);
            var randomValue = BitConverter.ToString(randomBytes);
            return randomValue;
        }
    }
}