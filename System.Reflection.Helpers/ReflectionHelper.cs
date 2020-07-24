using System.Collections.Generic;
using System.Linq;

namespace System.Reflection
{
    public static partial class Helper
    {
        /// <summary>
        /// Returns all the properties for a Type
        /// </summary>
        /// <param name="type">the Type object being probed</param>
        /// <returns>IEnumerable of PropertyInfo items</returns>
        public static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
        {
            Func<PropertyInfo, bool> func = null;
            if (!type.IsInterface)
            {
                return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            }
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            List<Type> types = new List<Type>();
            Queue<Type> types1 = new Queue<Type>();
            types.Add(type);
            types1.Enqueue(type);
            while (types1.Count > 0)
            {
                Type type1 = types1.Dequeue();
                Type[] interfaces = type1.GetInterfaces();
                for (int i = 0; i < (int)interfaces.Length; i++)
                {
                    Type type2 = interfaces[i];
                    if (!types.Contains(type2))
                    {
                        types.Add(type2);
                        types1.Enqueue(type2);
                    }
                }
                PropertyInfo[] properties = type1.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
                Func<PropertyInfo, bool> func1 = func;
                if (func1 == null)
                {
                    bool func2(PropertyInfo x) => !propertyInfos.Contains(x);
                    Func<PropertyInfo, bool> func3 = func2;
                    func = func2;
                    func1 = func3;
                }
                IEnumerable<PropertyInfo> propertyInfos1 = ((IEnumerable<PropertyInfo>)properties).Where<PropertyInfo>(func1);
                propertyInfos.InsertRange(0, propertyInfos1);
            }
            return propertyInfos.ToArray();
        }

        /// <summary>
        /// Returns all the properties for a Type (when conflicting with other assemblies)
        /// </summary>
        /// <param name="type">the Type object being probed</param>
        /// <returns>IEnumerable of PropertyInfo items</returns>
        public static IEnumerable<PropertyInfo> GetAllPropertiesNoConflict(this Type type)
        {
            return GetAllProperties(type);
        }

        /// <summary>
        /// Gets the Assembly from an Assembly Name (optional)
        /// </summary>
        /// <param name="assemblyName">The Name of the Assembly being Loaded (optional)</param>
        /// <returns>Assembly object</returns>
        /// <remarks>in case no assembly name is provided, the Executing Assembly will be returned</remarks>
        public static Assembly GetAssembly(string assemblyName = "")
        {
            Assembly assembly = !string.IsNullOrEmpty(assemblyName) ? Assembly.Load(assemblyName) : Assembly.GetExecutingAssembly();
            return assembly;
        }

        /// <summary>
        /// Gets a Type from the assembly
        /// </summary>
        /// <typeparam name="T">The Type being returned from the assembly</typeparam>
        /// <param name="assembly">The assembly being probed</param>
        /// <returns>The object searched in its own Type</returns>
        public static T GetAssemblyType<T>(this Assembly assembly)
        {
            return (T)Activator.CreateInstance(assembly.GetType(typeof(T).FullName, false, true));
        }

        /// <summary>
        /// Gets an instance of the Type
        /// </summary>
        /// <param name="type">the object being instantiated</param>
        /// <returns>An object instance of the object</returns>
        public static object GetInstance(this Type type)
        {
            if (type.GetType().Name != "RuntimeType")
                return Activator.CreateInstance(type.GetType());
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Gets an instance of the Type
        /// </summary>
        /// <typeparam name="T">The Type of the Instance being returned</typeparam>
        /// <param name="type">the object being instantiated</param>
        /// <returns>A Generic instance of the object</returns>
        public static T GetInstance<T>(this Type type)
        {
            if (type == null)
                return (T)Activator.CreateInstance(type.GetType());
            return (T)Activator.CreateInstance(type);
        }

        /// <summary>
        /// Gets all the properties in the Assembly
        /// </summary>
        /// <param name="assembly">The assembly being queried</param>
        /// <returns>IEnumerable of PropertyInfo items</returns>
        public static IEnumerable<PropertyInfo> GetProperties(this Assembly assembly)
        {
            return assembly.GetType().GetAllProperties();
        }

        /// <summary>
        /// Gets a specific property from an object
        /// </summary>
        /// <param name="_obj">The object being probed</param>
        /// <param name="name">The name of the property</param>
        /// <returns>PropertyInfo item</returns>
        public static PropertyInfo GetProperty(this object _obj, string name)
        {
            return _obj.GetType().GetProperty(name);
        }

        /// <summary>
        /// Gets a Type from the assembly
        /// </summary>
        /// <typeparam name="T">The Type being returned from the assembly</typeparam>
        /// <param name="assembly">The assembly being probed</param>
        /// <returns>A Type of the object searched</returns>
        public static Type GetType<T>(this Assembly assembly)
        {
            return assembly.GetType(typeof(T).FullName, false, true);
        }

        /// <summary>
        /// Gets an instance of the type that is present in the assembly
        /// </summary>
        /// <typeparam name="T">The Type being instantiated</typeparam>
        /// <param name="assembly">The assembly being probed</param>
        /// <returns>An Instantiated Type</returns>
        public static T GetTypeInstance<T>(this Assembly assembly)
        {
            return (T)Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Gets all the Types included in the Assembly that are of Type T
        /// </summary>
        /// <typeparam name="T">Generic Type that will be searched for in the assembly</typeparam>
        /// <param name="assemblyName">The Name of the Assembly being Loaded (optional)</param>
        /// <returns></returns>
        /// <remarks>in case no assembly name is provided, the Executing Assembly will be returned</remarks>
        public static List<Type> GetTypes<T>(string assemblyName = "")
        {
            var assembly = GetAssembly(assemblyName);

            var allTypes = assembly.GetTypes();
            List<Type> types = new List<Type>();
            foreach (var type in allTypes)
                if (type.Equals(typeof(T))) types.Add(type);
            return types;
        }

        /// <summary>
        /// Informs if the property has a value
        /// </summary>
        /// <param name="property">The property being probed</param>
        /// <returns>Boolean True or False whether the Property has a Value</returns>
        public static bool HasValue(this PropertyInfo property)
        {
            if (property == null) return false;

            var instance = Activator.CreateInstance(property.DeclaringType);
            var result = instance.GetProperty(property.Name).GetValue(instance, null);
            return result != null;
        }

        /// <summary>
        /// Gets the value of a specified property when you do not know its return type
        /// </summary>
        /// <param name="property">The property being probed</param>
        /// <returns>The value of the property in its original Type (i.e. int, string, bool, etc)</returns>
        public static object Value(this PropertyInfo property)
        {
            var instance = Activator.CreateInstance(property.DeclaringType);
            return instance.GetProperty(property.Name).GetValue(instance, null);
        }

        /// <summary>
        /// Gets the value for a specific property
        /// </summary>
        /// <typeparam name="T">The Type (PropertyInfo) being probed</typeparam>
        /// <typeparam name="U">The return type of the Value</typeparam>
        /// <param name="property">The object being probed</param>
        /// <returns>The object searched in its own Type (i.e. int, string, bool, etc)</returns>
        public static U Value<T, U>(this T property) where T : PropertyInfo
        {
            var instance = Activator.CreateInstance(typeof(T));
            return (U)typeof(T).GetProperty(typeof(T).Name).GetValue(instance);
        }

        /// <summary>
        /// Gets the value of a specified property when the return type is known
        /// </summary>
        /// <typeparam name="T">The Type of the return value (i.e. string, int, bool, etc)</typeparam>
        /// <param name="type">The PropertyInfo type being probed</param>
        /// <returns>The object searched in its own Type (i.e. int, string, bool, etc)</returns>
        public static T Value<T>(this PropertyInfo type)
        {
            var instance = Activator.CreateInstance(type.DeclaringType);
            return (T)instance.GetType().GetProperty(type.Name).GetValue(instance, null);
        }

        /// <summary>
        /// Gets the value of the property when it is an IEnumerable
        /// </summary>
        /// <typeparam name="T">The Type that the IEnumerable will return</typeparam>
        /// <param name="obj">the object being probed</param>
        /// <param name="name">the Name of Property being Returned</param>
        /// <returns>An IEnumerable of Objects</returns>
        /// <see cref="https://stackoverflow.com/questions/3024381/c-get-all-collection-properties-from-an-object"/> 
        public static IEnumerable<T> ValueCollection<T>(this PropertyInfo property)
        {
            IEnumerable<T> res = null;
            var instance = Activator.CreateInstance(property.DeclaringType);
            var get = property.GetGetMethod();
            if (!get.IsStatic && get.GetParameters().Length == 0) // skip indexed & static
            {
                var collection = (IEnumerable<T>)get.Invoke(instance, null);
                if (collection != null) res = collection;
            }

            return res;
        }

        /// <summary>
        /// Gets the value from the Property that is a list
        /// </summary>
        /// <typeparam name="T">The Type of items that will be in the list</typeparam>
        /// <param name="type">The propertyInfo being probed</param>
        /// <returns>A list of values from the property</returns>
        //public static List<T> ValueCollection<T>(this PropertyInfo type)
        //{
        //    var instance = Activator.CreateInstance(type.DeclaringType);
        //    return (List<T>)instance.GetType().GetProperty(type.Name).GetValue(instance, null);
        //}
        /// <summary>
        /// Gets the value from the Property that is a Dictionary
        /// </summary>
        /// <typeparam name="T">The Type of keys that will be in the dictionary</typeparam>
        /// <typeparam name="U">The Type of the values that will be in the dictionary</typeparam>
        /// <param name="type">The propertyInfo being probed</param>
        /// <returns>A Dictionary of values from the property</returns>
        public static Dictionary<T, U> ValueCollection<T, U>(this PropertyInfo type)
        {
            var instance = Activator.CreateInstance(type.DeclaringType);
            return (Dictionary<T, U>)instance.GetType().GetProperty(type.Name).GetValue(instance, null);
        }
    }
}
