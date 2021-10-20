using System.Collections.Generic;
using System.Reflection;

namespace Faker.Core.Comparer
{
    internal class ConstructorAccessModComparer : IComparer<ConstructorInfo>
    {
        public int Compare(ConstructorInfo? x, ConstructorInfo? y)
        {
            if (ReferenceEquals(x, y))
                return 0;
            if (ReferenceEquals(null, y))
                return 1;
            if (ReferenceEquals(null, x))
                return -1;
            MethodAttributes xAccessMode = x.Attributes | MethodAttributes.MemberAccessMask;
            MethodAttributes yAccessMode = y.Attributes | MethodAttributes.MemberAccessMask;
            int comparisonResult = xAccessMode.CompareTo(yAccessMode);
            return comparisonResult;
        }
    }
}
