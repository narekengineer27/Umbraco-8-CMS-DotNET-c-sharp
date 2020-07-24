using System.Collections;
using System.Collections.Generic;

namespace Umbraco.Plugins.Connector.Models
{
    public enum ApiSettingType
    {
        AppSettings,
        CustomErrors,
        ClientDependency,
        UmbracoConfig
    }
    public class ApiSetting
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public ApiSettingType SettingType { get; set; }
    }

    public class ApiSettings : IList<ApiSetting>
    {
        private List<ApiSetting> settings;
        public ApiSettings()
        {
            settings = settings ?? new List<ApiSetting>();
        }

        public ApiSetting this[int index] { get => settings[index]; set => settings[index] = value; }
        public ApiSetting this[string key]
        {
            get => settings.Find(x => x.Key.Equals(key)); set
            {
                var index = settings.FindIndex(x => x.Key.Equals(key));
                settings[index] = value;
            }
        }

        public int Count => settings.Count;

        public bool IsReadOnly => false;

        public void Add(ApiSetting item)
        {
           settings.Add(item);
        }

        public void Clear()
        {
            settings.Clear();
        }

        public bool Contains(ApiSetting item)
        {
           return settings.Contains(item);
        }

        public void CopyTo(ApiSetting[] array, int arrayIndex)
        {
            settings.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ApiSetting> GetEnumerator()
        {
            return settings.GetEnumerator();
        }

        public int IndexOf(ApiSetting item)
        {
            return settings.IndexOf(item);
        }

        public void Insert(int index, ApiSetting item)
        {
            settings.Insert(index, item);
        }

        public bool Remove(ApiSetting item)
        {
           return settings.Remove(item);
        }

        public void RemoveAt(int index)
        {
            settings.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return settings.GetEnumerator();
        }
    }
}
