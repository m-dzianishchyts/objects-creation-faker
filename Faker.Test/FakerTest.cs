using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Faker.Core;
using Faker.Core.Exception;
using Faker.Test.Data;
using NUnit.Framework;

namespace Faker.Test
{
    [TestFixture]
    public class FakerTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Assembly.LoadFile(@"C:\Users\maxiemar\RiderProjects\objects-creation-faker" +
                              @"\Faker.Generator.StringGenerator\bin\Debug\net5.0\Faker.Generator.StringGenerator.dll");
            Assembly.LoadFile(@"C:\Users\maxiemar\RiderProjects\objects-creation-faker" +
                              @"\Faker.Generator.DateTimeGenerator\bin\Debug\net5.0\Faker.Generator.DateTimeGenerator.dll");
            _faker = new Core.Faker();
        }

        private IFaker _faker = null!;

        private static TestCaseData[] s_availableTypes =
        {
            new(typeof(bool)),
            new(typeof(byte)),
            new(typeof(sbyte)),
            new(typeof(char)),
            new(typeof(decimal)),
            new(typeof(double)),
            new(typeof(float)),
            new(typeof(int)),
            new(typeof(uint)),
            new(typeof(long)),
            new(typeof(ulong)),
            new(typeof(short)),
            new(typeof(ushort)),

            new(typeof(string)),
            new(typeof(DateTime)),
            new(typeof(List<int>))
        };

        private static TestCaseData[] s_unavailableTypes =
        {
            new(typeof(BigInteger)),
            new(typeof(Uri))
        };

        [Test]
        [TestCaseSource(nameof(s_availableTypes))]
        public void Faker_Create_AvailableTypes_DoesNotThrow(Type type)
        {
            MethodInfo genericMethodCreate = MakeGenericMethodCreate(type);
            Assert.DoesNotThrow(() => genericMethodCreate.Invoke(_faker, Array.Empty<object>()));
        }

        [Test]
        [TestCaseSource(nameof(s_availableTypes))]
        public void Faker_Creates_ExpectedType(Type type)
        {
            MethodInfo genericMethodCreate = MakeGenericMethodCreate(type);
            Assert.AreEqual(type, genericMethodCreate.Invoke(_faker, Array.Empty<object>())?.GetType());
        }

        [Test]
        [TestCaseSource(nameof(s_unavailableTypes))]
        public void Faker_Create_UnavailableTypes_Throw(Type type)
        {
            MethodInfo genericMethodCreate = MakeGenericMethodCreate(type);
            Assert.Throws(Is.InstanceOf(typeof(ApplicationException)),
                          () => { genericMethodCreate.Invoke(_faker, Array.Empty<object>()); });
        }

        [Test]
        public void Faker_Create_Class_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => _faker.Create<A1>());
        }

        [Test]
        public void Faker_Create_Class_NotNull()
        {
            A1 obj = _faker.Create<A1>();
            Assert.IsNotNull(obj);
        }

        [Test]
        public void Faker_Prefers_Constructor_WithLargerNumberOfParams()
        {
            A2 obj = _faker.Create<A2>();
            Assert.AreEqual(A2.ONE_PARAMETER_CONSTRUCTOR_FIELD_VALUE, obj.field);
        }

        [Test]
        public void Faker_Create_WithPrivateConstructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => _faker.Create<A3>());
        }

        [Test]
        public void Faker_Prefers_PublicConstructor()
        {
            A4 obj = _faker.Create<A4>();
            Assert.AreEqual(A4.PUBLIC_CONSTRUCTOR_FIELD_VALUE, obj.field);
        }

        [Test]
        public void Faker_Initializes_ConstructorParameters()
        {
            A5 obj = _faker.Create<A5>();
            Assert.IsNotNull(obj.field);
        }

        [Test]
        public void Faker_Initializes_Fields()
        {
            A6 obj = _faker.Create<A6>();
            Assert.IsNotNull(obj.field);
        }

        [Test]
        public void Faker_Initialized_Properties()
        {
            A7 obj = _faker.Create<A7>();
            Assert.IsNotNull(obj.Property);
        }

        [Test]
        public void Faker_Ignores_NonPublicFields()
        {
            A8 obj = _faker.Create<A8>();
            Assert.IsTrue(obj.IsPrivateFieldInitialized());
            Assert.IsTrue(obj.IsProtectedFieldInitialized());
            Assert.IsTrue(obj.IsInternalFieldInitialized());
            Assert.IsTrue(obj.IsProtectedInternalFieldInitialized());
            Assert.IsTrue(obj.IsPrivateProtectedFieldInitialized());
        }

        [Test]
        public void Faker_Ignores_NonPublicProperties()
        {
            A9 obj = _faker.Create<A9>();
            Assert.IsTrue(obj.IsPrivatePropertyInitialized());
            Assert.IsTrue(obj.IsProtectedPropertyInitialized());
            Assert.IsTrue(obj.IsInternalPropertyInitialized());
            Assert.IsTrue(obj.IsProtectedInternalPropertyInitialized());
            Assert.IsTrue(obj.IsPrivateProtectedPropertyInitialized());
        }

        [Test]
        public void Faker_Ignores_InitializedFields()
        {
            A10 obj = _faker.Create<A10>();
            Assert.AreEqual(A10.FIELD_INITIALIZATION_VALUE, obj.initializedField);
        }

        [Test]
        public void Faker_Ignores_InitializedProperties()
        {
            A11 obj = _faker.Create<A11>();
            Assert.AreEqual(A11.PROPERTY_INITIALIZATION_VALUE, obj.Property);
        }

        [Test]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Faker_Create_Struct_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => _faker.Create<A12>());
        }

        [Test]
        public void Faker_Create_WithShallowCyclicDependency_Throws()
        {
            Assert.Throws(Is.InstanceOf(typeof(FakerException)), () => _faker.Create<A13>());
        }

        [Test]
        public void Faker_Create_WithDeepCyclicDependency_Throws()
        {
            Assert.Throws(Is.InstanceOf(typeof(FakerException)), () => _faker.Create<A14>());
        }

        [Test]
        public void Faker_Create_GenericList_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => _faker.Create<List<string>>());
        }

        [Test]
        public void Faker_Create_GenericList_Of_GenericLists_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => _faker.Create<List<List<string>>>());
        }

        private MethodInfo MakeGenericMethodCreate(Type type)
        {
            MethodInfo methodCreate = _faker.GetType().GetMethod(nameof(Core.Faker.Create))!;
            MethodInfo genericMethodCreate = methodCreate.MakeGenericMethod(type);
            return genericMethodCreate;
        }
    }
}
