using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Faker.Core.Generator
{
    public class ListGenerator : IGenerator
    {
        private const int LIST_MIN_LENGTH = 0;
        private const int LIST_MAX_LENGTH = 10;

        public bool CanGenerate(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        public object Generate(GeneratorContext context)
        {
            Type listContentType = context.TargetType.GenericTypeArguments.First();
            Type genericListType = typeof(List<>).MakeGenericType(listContentType);
            IList list = (IList) Activator.CreateInstance(genericListType)!;
            MethodInfo fakerCreateMethod = typeof(Faker).GetMethod(nameof(Faker.Create))!;
            MethodBase fakerCreateGenericMethod = fakerCreateMethod.MakeGenericMethod(listContentType);
            int listLength = context.Random.Next(LIST_MIN_LENGTH, LIST_MAX_LENGTH);
            for (var i = 0; i < listLength; i++)
            {
                object newObject = fakerCreateGenericMethod.Invoke(context.Faker, Array.Empty<object>())!;
                list.Add(newObject);
            }

            return list;
        }
    }
}
