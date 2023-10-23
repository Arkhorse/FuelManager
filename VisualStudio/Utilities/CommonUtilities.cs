using System.Diagnostics.CodeAnalysis;
using Il2CppSystem.Text.RegularExpressions;

namespace FuelManager
{
    public static class CommonUtilities
    {
        [return: NotNullIfNotNull(nameof(name))]
        public static GearItem GetGearItemPrefab(string name) => GearItem.LoadGearItemPrefab(name);

        /// <summary>
        /// Normalizes the name given to remove extra bits using regex for most of the changes
        /// </summary>
        /// <param name="name">The name of the thing to normalize</param>
        /// <returns>Normalized name without <c>(Clone)</c> or any numbers appended</returns>
        [return: NotNullIfNotNull(nameof(name))]
        public static string? NormalizeName(string name)
        {
            string name0 = Regex.Replace(name, @"(?:\(\d{0,}\))", string.Empty);
            string name1 = Regex.Replace(name0, @"(?:\s\d{0,})", string.Empty);
            string name2 = name1.Replace("(Clone)", string.Empty, StringComparison.InvariantCultureIgnoreCase);
            string name3 = name2.Replace("\0", string.Empty);
            return name3.Trim();
        }

        [return: NotNullIfNotNull("component")]
        public static T? GetComponentSafe<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetComponentSafe<T>(component.gameObject);
        }

        [return: NotNullIfNotNull("gameObject")]
        public static T? GetComponentSafe<T>(this GameObject? gameObject) where T : Component
        {
            return gameObject == null ? default : gameObject.GetComponent<T>();
        }

        [return: NotNullIfNotNull("component")]
        public static T? GetOrCreateComponent<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetOrCreateComponent<T>(component.gameObject);
        }

        [return: NotNullIfNotNull("gameObject")]
        public static T? GetOrCreateComponent<T>(this GameObject? gameObject) where T : Component
        {
            if (gameObject == null) return default;

            T? result = GetComponentSafe<T>(gameObject) ?? gameObject.AddComponent<T>();
            return result;
        }

        [return: NotNullIfNotNull("component")]
        public static GameObject? GetGameObject(this Component? component)
        {
            try
            {
                return component == null ? default : component.gameObject;
            }
            catch (Exception exception)
            {
                Logging.LogError($"Returning null since this could not obtain a GameObject from the component. Stack trace:\n{exception.Message}");
            }

            return null;
        }

        public static T GetItem<T>(string name, string? reference = null) where T : Component
        {
            GameObject? gameObject = AssetBundleUtils.LoadAsset<GameObject>(name) ?? throw new ArgumentException("Could not load '" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + ".");
            T targetType = GetComponentSafe<T>(gameObject);

            return targetType ?? throw new ArgumentException("'" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + " is not a '" + typeof(T).Name + "'.");
        }

        public static T[] GetItems<T>(string[] names, string? reference = null) where T : Component
        {
            T[] result = new T[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                result[i] = GetItem<T>(names[i], reference);
            }

            return result;
        }
    }
}
