using System;

namespace Faker.Core.Generator
{
    public class DateTimeGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }

        public object Generate(GeneratorContext context)
        {
            long randomLong = GenerateInt64(context.Random);
            randomLong &= long.MaxValue;
            long randomTicks = randomLong % DateTime.MaxValue.Ticks;
            var dateTime = new DateTime(randomTicks);
            return dateTime;
        }

        private static long GenerateInt64(Random random)
        {
            byte[] buffer = new byte[sizeof(long)];
            random.NextBytes(buffer);
            var randomValue = BitConverter.ToInt64(buffer);
            return randomValue;
        }
    }
}
