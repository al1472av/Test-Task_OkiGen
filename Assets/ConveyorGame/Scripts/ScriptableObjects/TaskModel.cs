using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ConveyorGame.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game Resources/Task Model")]
    public class TaskModel : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<ProductModel, int> ProductsToCollect { get; private set; }

        public bool CheckIfTaskIsDone(List<ProductModel> collectedProducts)
        {
           var collectedProductsDictionary = collectedProducts.GroupBy(v => v).ToDictionary(t => t.Key, t => t.Count());

           return ProductsToCollect.All(product => collectedProductsDictionary.ContainsKey(product.Key) && collectedProductsDictionary[product.Key] >= product.Value);
        }
    }
}