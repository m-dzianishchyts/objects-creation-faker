using System.Collections.Generic;
using System.Reflection;

namespace Faker.Core.Comparer
{
    internal class ConstructorParametersAmountComparer : IComparer<ConstructorInfo>
    {
        public int Compare(ConstructorInfo? x, ConstructorInfo? y)
        {
            if (ReferenceEquals(x, y))
                return 0;
            if (ReferenceEquals(null, y))
                return 1;
            if (ReferenceEquals(null, x))
                return -1;
            int xParametersAmount = x.GetParameters().Length;
            int yParametersAmount = y.GetParameters().Length;
            int comparisonResult = xParametersAmount.CompareTo(yParametersAmount);
            return comparisonResult;
        }
    }
}
