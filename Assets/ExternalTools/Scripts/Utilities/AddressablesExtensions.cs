using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace ExternalTools.Scripts.Utilities
{
    public static class AddressablesExtensions
    {
        public static void ReleaseAssetForce(this AssetReference asset, Image image)
        {
            image.sprite = null;
            asset.ReleaseAsset();
        }

        public static void ReleaseAssetForce(this AssetReference asset, GameObject gameObject)
        {
            Object.Destroy(gameObject);
            asset.ReleaseAsset();
        }
    }
}