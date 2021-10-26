using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Faker.Core.Comparer;
using Faker.Core.Exception;
using Faker.Core.Generator;

namespace Faker.Core
{
    public class Faker : IFaker
    {
        private const BindingFlags CONSTRUCTORS_BINDING_FLAGS =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private const BindingFlags FIELDS_BINDING_FLAGS =
            BindingFlags.Instance | BindingFlags.Public;

        private const BindingFlags PROPERTIES_BINDING_FLAGS =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;

        private static readonly IComparer<ConstructorInfo> ConstructorAccessModComparer =
            new ConstructorAccessModComparer();

        private static readonly IComparer<ConstructorInfo> ConstructorParametersAmountComparer =
            new ConstructorParametersAmountComparer();

        private readonly ISet<Type> _creatingTypes;

        private readonly List<IGenerator> _generators;

        public Faker()
        {
            Type generatorType = typeof(IGenerator);
            IEnumerable<IGenerator> generatorImplementations =
                AppDomain.CurrentDomain.GetAssemblies()
                         .SelectMany(assembly => assembly.GetTypes())
                         .Where(type => type.GetInterfaces().Contains(generatorType) && type.IsClass)
                         .Select(Activator.CreateInstance)
                         .Where(nullable => nullable is not null)
                         .Select(obj => (IGenerator) obj!);
            _generators = new List<IGenerator>(generatorImplementations);
            _creatingTypes = new HashSet<Type>();
        }

        /// <summary>
        /// Generates value of type T.
        /// </summary>
        /// <typeparam name="T">Class or value type</typeparam>
        /// <returns>Mostly initialized value of type T</returns>
        /// <exception cref="NoEffectiveConstructorException">
        /// None of the available constructors were
        /// successfully used to create the object.
        /// </exception>
        /// <exception cref="CyclicDependencyException">
        /// There is a cycle in the type hierarchy.
        /// </exception>
        public T Create<T>()
        {
            var result = (T) Create(typeof(T));
            return result;
        }

        private object Create(Type type)
        {
            object newObject;
            var random = new Random();
            foreach (IGenerator generator in _generators.Where(generator => generator.CanGenerate(type)))
            {
                var generatorContext = new GeneratorContext(type, random, this);
                newObject = generator.Generate(generatorContext);
                return newObject;
            }

            newObject = InitializeViaAnyConstructor(type);
            InitializeFields(newObject);
            InitializeProperties(newObject);
            return newObject;
        }

        private object InitializeViaAnyConstructor(Type currentCreatingType)
        {
            _creatingTypes.Add(currentCreatingType);
            IEnumerable<ConstructorInfo> constructors =
                currentCreatingType.GetConstructors(CONSTRUCTORS_BINDING_FLAGS)
                                   .OrderByDescending(constructor => constructor, ConstructorAccessModComparer)
                                   .ThenByDescending(constructor => constructor, ConstructorParametersAmountComparer);
            foreach (var constructor in constructors)
            {
                object newObject = InitializeViaConstructor(constructor);
                return newObject;
            }

            if (!currentCreatingType.IsValueType)
                throw new NoEffectiveConstructorException(currentCreatingType);

            object value = Activator.CreateInstance(currentCreatingType)!;
            return value;
        }

        private object InitializeViaConstructor(ConstructorInfo constructor)
        {
            IList<Type> constructorDependencies =
                constructor.GetParameters()
                           .Select(parameter => parameter.ParameterType)
                           .ToList();
            CheckOnCyclicDependencies(constructorDependencies);
            var parameters = new List<object>();
            foreach (var constructorDependency in constructorDependencies)
            {
                _creatingTypes.Add(constructorDependency);
                object parameter = Create(constructorDependency);
                _creatingTypes.Remove(constructorDependency);
                parameters.Add(parameter);
            }

            object newObject = constructor.Invoke(parameters.ToArray());
            return newObject;
        }

        private void InitializeFields(object obj)
        {
            Type objType = obj.GetType();
            List<FieldInfo> fields = objType.GetFields(FIELDS_BINDING_FLAGS)
                                            .Where(field => HasDefaultValue(obj, field))
                                            .ToList();
            List<Type> fieldsTypes = fields.Select(field => field.FieldType).ToList();
            CheckOnCyclicDependencies(fieldsTypes);
            fields.ForEach(field => InitializeField(obj, field));
        }

        private void InitializeField(object obj, FieldInfo field)
        {
            Type fieldType = field.FieldType;
            _creatingTypes.Add(fieldType);
            object fieldValue = Create(fieldType);
            _creatingTypes.Remove(fieldType);
            field.SetValue(obj, fieldValue);
        }

        private void InitializeProperties(object obj)
        {
            Type objType = obj.GetType();
            List<PropertyInfo> properties = objType.GetProperties(PROPERTIES_BINDING_FLAGS)
                                                   .Where(property => HasDefaultValue(obj, property))
                                                   .ToList();
            List<Type> propertyTypes = properties.Select(field => field.PropertyType).ToList();
            CheckOnCyclicDependencies(propertyTypes);
            properties.ForEach(property => InitializeProperty(obj, property));
        }

        private void CheckOnCyclicDependencies(IEnumerable<Type> propertyTypes)
        {
            IEnumerable<Type> cycleCauses = propertyTypes.Intersect(_creatingTypes).ToList();
            if (!cycleCauses.Any())
                return;

            Type cycleCause = cycleCauses.First();
            List<Type> cyclicPath = _creatingTypes.Append(cycleCause).ToList();
            throw new CyclicDependencyException(cycleCauses.First(), cyclicPath);
        }

        private void InitializeProperty(object obj, PropertyInfo property)
        {
            Type propertyType = property.PropertyType;
            _creatingTypes.Add(propertyType);
            object propertyValue = Create(propertyType);
            _creatingTypes.Remove(propertyType);
            property.SetValue(obj, propertyValue);
        }

        private static bool HasDefaultValue(object obj, FieldInfo field)
        {
            object? fieldValue = field.GetValue(obj);
            return fieldValue == default;
        }

        private static bool HasDefaultValue(object obj, PropertyInfo property)
        {
            object? propertyValue = property.GetValue(obj);
            return propertyValue == default;
        }
    }
}
