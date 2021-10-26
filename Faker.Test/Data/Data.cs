using System.Runtime.CompilerServices;

namespace Faker.Test.Data
{
    #region Faker instantiates class

    public class A1
    {
    }

    #endregion

    #region Faker prefers a constructor with a larger number of parameters

    public class A2
    {
        public const int ONE_PARAMETER_CONSTRUCTOR_FIELD_VALUE = 13;
        public const int PARAMETERLESS_CONSTRUCTOR_FIELD_VALUE = 42;

        public int field;

        public A2(int anyParameter)
        {
            field = ONE_PARAMETER_CONSTRUCTOR_FIELD_VALUE;
        }

        public A2()
        {
            field = PARAMETERLESS_CONSTRUCTOR_FIELD_VALUE;
        }
    }

    #endregion

    #region Faker can use private constructor

    public class A3
    {
        private A3()
        {
        }
    }

    #endregion

    #region Faker prefers public constructor

    public class A4
    {
        public const int PUBLIC_CONSTRUCTOR_FIELD_VALUE = 12;
        public const int PRIVATE_CONSTRUCTOR_FIELD_VALUE = 99;

        public int field;

        public A4(int anyParameter)
        {
            field = PUBLIC_CONSTRUCTOR_FIELD_VALUE;
        }

        private A4(float anyParameter)
        {
            field = PRIVATE_CONSTRUCTOR_FIELD_VALUE;
        }
    }

    #endregion

    #region Faker initializes constructor paremeters

    public class A5
    {
        public B5 field;

        public A5(B5 field)
        {
            this.field = field;
        }
    }

    public class B5
    {
    }

    #endregion

    #region Faker initializes fields

    public class A6
    {
        public B6 field;
    }

    public class B6
    {
    }

    #endregion

    #region Faker initializes properties

    public class A7
    {
        public B7? Property { get; set; }
    }

    public class B7
    {
    }

    #endregion

    #region Faker ignores non-public fields

    public class A8
    {
        private B8 privateField;
        protected B8 protectedField;
        internal B8 internalField;
        protected internal B8 protectedInternalField;
        private protected B8 privateProtectedField;

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public bool IsPrivateFieldInitialized()
        {
            return privateField is null;
        }
        
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public bool IsProtectedFieldInitialized()
        {
            return protectedField is null;
        }
        
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public bool IsInternalFieldInitialized()
        {
            return internalField is null;
        }
        
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public bool IsProtectedInternalFieldInitialized()
        {
            return protectedInternalField is null;
        }
        
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public bool IsPrivateProtectedFieldInitialized()
        {
            return privateProtectedField is null;
        }
    }

    public class B8
    {
    }

    #endregion

    #region Faker ignores non-public properties
    
    public class A9
    {
        private B9? PrivateProperty { get; set; }
        protected B9? ProtectedProperty { get; set; }
        internal B9? InternalProperty { get; set; }
        protected internal B9? ProtectedInternalProperty { get; set; }
        private protected B9? PrivateProtectedProperty { get; set; }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public bool IsPrivatePropertyInitialized()
        {
            return PrivateProperty is null;
        }
        
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public bool IsProtectedPropertyInitialized()
        {
            return ProtectedProperty is null;
        }
        
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public bool IsInternalPropertyInitialized()
        {
            return InternalProperty is null;
        }
        
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public bool IsProtectedInternalPropertyInitialized()
        {
            return ProtectedInternalProperty is null;
        }
        
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public bool IsPrivateProtectedPropertyInitialized()
        {
            return PrivateProtectedProperty is null;
        }
    }

    public class B9
    {
    }

    #endregion

    #region Faker ignores initialized fields

    public class A10
    {
        public const int FIELD_INITIALIZATION_VALUE = 13;
        public int initializedField = FIELD_INITIALIZATION_VALUE;
    }

    #endregion

    #region Faker ignores initialized properties

    public class A11
    {
        public const int PROPERTY_INITIALIZATION_VALUE = 23;
        public int Property { get; set; } = PROPERTY_INITIALIZATION_VALUE;
    }

    #endregion

    #region Faker instantiates structs

    public struct A12
    {
    }

    #endregion

    #region Faker fails on shallow cyclic dependency

    public class A13
    {
        public A13(A13 cyclicDependency)
        {
        }
    }

    #endregion
    
    #region Faker fails on deep cyclic dependency

    public class A14
    {
        public A14(B14 dependency)
        {
        }
    }

    public class B14
    {
        public B14(C14 dependency)
        {
        }
    }

    public class C14
    {
        public C14(B14 cyclicDependency)
        {
        }
    }

    #endregion
}
