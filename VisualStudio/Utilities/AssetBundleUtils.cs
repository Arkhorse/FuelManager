using Il2CppInterop.Runtime;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.AddressableAssets;

namespace FuelManager.Utilities
{
    /// <summary>
    /// Alternative asset loading methods to avoid triggering the AssetLoader patches
    /// </summary>
    public class AssetBundleUtils
    {
#pragma warning disable CS8603
        /// <summary>
        /// Loads an asset without triggering the AssetLoader patches
        /// </summary>
        public static UnityEngine.Object LoadAsset(AssetBundle assetBundle, string name)
        {
            return LoadAsset(assetBundle, name, Il2CppType.Of<UnityEngine.Object>());
        }

        /// <summary>
        /// Loads an asset without triggering the AssetLoader patches
        /// </summary>
        public static T LoadAsset<T>(AssetBundle assetBundle, string name) where T : UnityEngine.Object
        {
            return LoadAsset(assetBundle, name, Il2CppType.Of<T>()).TryCast<T>();
        }

        /// <summary>
        /// Loads an asset without triggering the AssetLoader patches
        /// </summary>
        public static UnityEngine.Object LoadAsset([DisallowNull] AssetBundle assetBundle, [DisallowNull] string name, [DisallowNull] Il2CppSystem.Type type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BadMemeException("The input asset name cannot be empty.");
            }
            return assetBundle.LoadAsset_Internal(name, type);
        }

        /// <summary>
        /// Load an asset from Addressables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static T LoadAsset<T>(string assetName) => Addressables.LoadAssetAsync<T>(assetName).WaitForCompletion();
    }
}
