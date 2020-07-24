using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Reflection
{
    public static partial class Helper
    {
        /// <summary>
        /// Gets a specific attribute of Type T Generic)
        /// </summary>
        /// <typeparam name="T">Attribute</typeparam>
        /// <param name="type">The Assembly Type being probed</param>
        /// <returns>returns an Attribute Object</returns>
        public static Attribute GetAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute(typeof(T), false);
        }

        /// <summary>
        /// Gets an Attribute from a Type
        /// </summary>
        /// <typeparam name="T">The Type of the object being probed</typeparam>
        /// <typeparam name="A">The Attribute type being returned</typeparam>
        /// <param name="source">The object being probed</param>
        /// <returns>Retuens the value in its original type</returns>
        public static A GetAttribute<T, A>(this T source) where T : Type where A : Attribute
        {
            return (A)source.GetCustomAttribute(typeof(A));
        }

        /// <summary>
        /// Gets an Attribute from a PropertyInfo Type
        /// </summary>
        /// <typeparam name="A">The Type of the Attribute being Returned</typeparam>
        /// <param name="source">the object being probed</param>
        /// <returns>Returns the value in its original type</returns>
        public static A GetAttribute<A>(this PropertyInfo source) where A : Attribute
        {
            return (A)source.GetCustomAttribute(typeof(A));
        }

        /// <summary>
        /// Gets a Dictionary o objects based on a string Key
        /// </summary>
        /// <param name="source">The object being probed</param>
        /// <returns>A Dictionary of objects based on string keys</returns>
        public static Dictionary<string, object> GetClassAttributes(this Type source)
        {
            Dictionary<string, object> _dict = new Dictionary<string, object>();
            object[] classAttributes = source.GetCustomAttributes(true);

            foreach (var attribute in classAttributes)
            {
                var attributeAttributes = attribute.GetType().GetProperties();
                foreach (var attributeAttribute in attributeAttributes)
                {
                    var propName = $"{attribute.GetType().Name}.{attributeAttribute.Name}";
                    if (!_dict.ContainsKey(propName) && attributeAttribute.Name != "TypeId")
                        _dict.Add(propName, attributeAttribute.GetValue(attribute, null));
                }
            }
            return _dict;
        }

        /// <summary>
        /// Gets the attributes for a specific Property based on 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="field"></param>
        /// <returns>Dictionary<string, object></returns>
        /// <see cref="https://stackoverflow.com/questions/6637679/reflection-get-attribute-name-and-value-on-property"/>
        /// <seealso cref="https://stackoverflow.com/questions/23329087/get-custom-attribute-from-specific-object-property-field"/>
        /// <example>var money = (decimal)model.GetAttributes(() => model.Money)["Money.Value"];</example>
        public static Dictionary<string, object> GetPropertyAttributes<T>(this T source, Expression<Func<object>> field) where T : class
        {
            MemberExpression member = (MemberExpression)field.Body;
            if (member == null) { return null; }

            Dictionary<string, object> _dict = new Dictionary<string, object>();
            object[] propertyAttributes = typeof(T).GetProperty(member.Member.Name).GetCustomAttributes(true);

            foreach (var propertyAttribute in propertyAttributes)
            {
                var attributeAttributes = propertyAttribute.GetType().GetProperties();
                foreach (var attributeAttribute in attributeAttributes)
                {
                    var propName = $"{propertyAttribute.GetType().Name}.{attributeAttribute.Name}";
                    if (!_dict.ContainsKey(propName) && attributeAttribute.Name != "TypeId")
                        _dict.Add(propName, attributeAttribute.GetValue(propertyAttribute, null));
                }
            }
            return _dict;
        }

        /// <summary>
        /// Asserts whether the type has a certain Attribute of Type T (Generic)
        /// </summary>
        /// <typeparam name="T">Attribute</typeparam>
        /// <param name="type">The Assembly Type being probed</param>
        /// <returns>Returns bool (true/false)</returns>
        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetAttribute<T>() != null;
        }

        /// <summary>
        /// Asserts whether the PropertyInfo has a certain Attribute of T (Generic)
        /// </summary>
        /// <typeparam name="T">Attribute</typeparam>
        /// <param name="prop">The PropertyInfo being probed</param>
        /// <returns>Returns bool (true/false)</returns>
        public static bool HasAttribute<T>(this PropertyInfo prop) where T : Attribute
        {
            return prop.GetAttribute<T>() != null;
        }

        /// <summary>
        /// Gets the Value of the Attribute by Attribute Name
        /// </summary>
        /// <typeparam name="T">The return type of the value (i.e. string, int, etc)</typeparam>
        /// <param name="attribute">The attribute being probed</param>
        /// <param name="name">The name of the attribute to be returned</param>
        /// <returns>Retuns the value in its original type</returns>
        public static T Value<T>(this Attribute attribute, string name = "")
        {
            return (T)attribute.GetType().GetProperty(name).GetValue(attribute, null);
        }
    }
}
