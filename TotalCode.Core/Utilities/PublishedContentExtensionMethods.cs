namespace TotalCode.Core.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    public static class PublishedContentExtensionMethods
    {

        public static IEnumerable<T> GetValueAsViewModels<T, U>(this IPublishedElement content, string propAlias, string language = "")
            where U : IPublishedElement
        {
            if (content.HasValue(propAlias))
            {
                var items = content.Value<IEnumerable<U>>(propAlias, culture: string.IsNullOrEmpty(language) ? null : language, fallback: Fallback.ToLanguage);
                if (items != null && items.Any())
                {
                    return items.Select(c => (T)Activator.CreateInstance(typeof(T), c));
                }
            }

            return Enumerable.Empty<T>();
        }

        public static IEnumerable<T> GetValueAsViewModels<T>(this IPublishedElement content, string propAlias, string language = "")
        {

            var items = content.Value<IEnumerable<IPublishedContent>>(propAlias, culture: string.IsNullOrEmpty(language) ? null : language, fallback: Fallback.ToLanguage);
            if (items != null && items.Any())
            {
                return items.Select(c => (T)Activator.CreateInstance(typeof(T), c));
            }

            return Enumerable.Empty<T>();
        }

        public static T GetValueAsViewModel<T, U>(this IPublishedElement content, string propAlias, string language = "")
            where T : class
            where U : IPublishedElement
        {
            var element = content.Value<IEnumerable<IPublishedElement>>(propAlias, culture: string.IsNullOrEmpty(language) ? null : language, fallback: Fallback.ToLanguage).FirstOrDefault();
            if (element != null)
            {
                return (T)Activator.CreateInstance(typeof(T), element);
            }

            return null;
        }

        public static T GetValueAsViewModel<T>(this IPublishedElement content, string propAlias, string language = "")
            where T : class
        {
            if (content.HasValue(propAlias))
            {
                var element = content.Value<IEnumerable<IPublishedElement>>(propAlias, culture: string.IsNullOrEmpty(language) ? null : language, fallback: Fallback.ToLanguage).FirstOrDefault();
                if (element != null)
                {
                    return (T)Activator.CreateInstance(typeof(T), element);
                }
            }

            return null;
        }
    }
}
