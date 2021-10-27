using System;

namespace Faker.Core.Exception
{
    public class NoEffectiveConstructorException : FakerException
    {
        public NoEffectiveConstructorException(Type type) : base(type.AssemblyQualifiedName)
        {
        }
        
        public NoEffectiveConstructorException(Type type, System.Exception? cause) : base(type.AssemblyQualifiedName, cause)
        {
        }
    }
}
