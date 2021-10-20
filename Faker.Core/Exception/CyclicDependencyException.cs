using System;
using System.Collections.Generic;

namespace Faker.Core.Exception
{
    public class CyclicDependencyException : FakerException
    {
        public CyclicDependencyException(Type cyclicDependentType)
            : base($"{cyclicDependentType} contains cyclic dependency")
        {
        }

        public CyclicDependencyException(Type cyclicDependentType, IEnumerable<Type> cyclicPath)
            : base(FormatDetailedMessage(cyclicDependentType, cyclicPath))
        {
        }

        private static string FormatBaseMessage(Type cyclicDependentType)
        {
            return $"{cyclicDependentType} contains cyclic dependency";
        }

        private static string FormatDetailedMessage(Type cyclicDependentType, IEnumerable<Type> cyclicPath)
        {
            return FormatBaseMessage(cyclicDependentType) + ": " + string.Join(" > ", cyclicPath);
        }
    }
}
