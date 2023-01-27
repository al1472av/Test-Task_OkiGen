using System;
using System.Linq;
using ConveyorGame.GameCore.ProductLogic;
using ConveyorGame.Services.Addressables;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ConveyorGame.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game Resources/Product Model")]
    public class ProductModel : SerializedScriptableObject
    {
        [SerializeField] private AssetReferenceComponent<ProductView> _prefabView;
        [OdinSerialize, ReadOnly]public string ID { get; private set; }
        [OdinSerialize] public string Name { get; private set; }
        public AssetReferenceComponent<ProductView> PrefabView => _prefabView;

        private void OnValidate()
        {
            var allProducts = Resources.LoadAll<ProductModel>("Products");
            if (string.IsNullOrEmpty(ID) || allProducts.Count(t => t.ID == ID) > 1)
            {
                ID = Guid.NewGuid().ToString();
            }
        }
    }
}