using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ConveyorGame.Services.Addressables
{
    [System.Serializable]
    public class AssetReferenceComponent<T> : AssetReferenceT<T> where T : MonoBehaviour
    {
        public AssetReferenceComponent(string guid) : base(guid)
        {
        }

        public override bool ValidateAsset(Object obj)
        {
            if (obj is MonoBehaviour go)
            {
                return go.GetComponent<T>() != null;
            }

            return false;
        }

#if UNITY_EDITOR
        public override bool ValidateAsset(string path)
        {
            var obj = AssetDatabase.LoadAssetAtPath<T>(path);
            return obj != null;
        }
#endif
    }
}