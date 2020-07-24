using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Umbraco.Core;

namespace Umbraco.Plugins.Connector.Helpers
{
    public static class ReflectionHelper
    {
        public static IEnumerable<Type> GetInheritedTypes<T>(this Assembly assembly)
        {
            return assembly.GetTypes().Where(t => t.Inherits<T>() && !t.IsInterface);
        }

        public static List<Type> GetInherited<T>(this Assembly assembly)
        {
            var types = assembly.GetTypes().Where(t => t.Inherits<T>() && !t.IsInterface).ToList();
            return types;
        }

        public static List<Type> ExcludeInherited<T>(this List<Type> originalList)
        {
            var exclusions = new List<Type>();
            foreach (var item in originalList)
                if (!item.Inherits<T>()) exclusions.Add(item);

            return exclusions;
        }

        public static string PrettyDisplayName(this PropertyDescriptor property)
        {
            var name = string.Empty;
            if (property.Attributes.Count > 0)
            {
                if (property.Attributes[typeof(DisplayAttribute)] is DisplayAttribute dd)
                    name = dd.Name;
                else name = property.Name;
            }
            return name;
        }
        public static List<string[]> WriteHeaders(this Type type)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(type);

            List<string[]> rows = new List<string[]>();
            var array = new string[props.Count];
            int column = 0;
            foreach (PropertyDescriptor prop in props)
            {
                array[column] = prop.PrettyDisplayName();
                column++;
            }
            rows.Add(array);

            return rows;
        }
        public static IEnumerable<object[]> WriteCells<T>(this IEnumerable<T> data, string locale)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            List<object[]> rows = new List<object[]>();

            foreach (T item in data)
            {
                int column = 0;
                var array = new object[props.Count];
                foreach (PropertyDescriptor prop in props)
                {
                    var type = prop.PropertyType;

                    CultureInfo ci = new CultureInfo(locale);
                    if (type == typeof(DateTime))
                    {
                        DateTime value = prop.GetValue(item) == null ? new DateTime() : (DateTime)DateTime.Parse(prop.GetValue(item).ToString());
                        array[column] = value.ToString("G", ci);
                    }
                    else if (type == typeof(decimal))
                    {
                        decimal value = prop.GetValue(item) == null ? 0.00m : decimal.Parse(prop.GetValue(item).ToString());
                        array[column] = value.ToString("c2", ci);
                    }
                    else if (type == typeof(int))
                    {
                        int value = prop.GetValue(item) == null ? 0 : int.Parse(prop.GetValue(item).ToString());
                        array[column] = value;
                    }
                    else if (type == typeof(long))
                    {
                        long value = prop.GetValue(item) == null ? 0 : long.Parse(prop.GetValue(item).ToString());
                        array[column] = value;
                    }
                    else
                    {
                        var value = prop.GetValue(item) == null ? "" : prop.Converter.ConvertToString(prop.GetValue(item));
                        array[column] = value;
                    }
                    column++;
                }
                rows.Add(array);
            }

            return rows;
        }
    }
}
