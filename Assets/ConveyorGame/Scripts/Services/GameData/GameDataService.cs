using System.Collections.Generic;
using ConveyorGame.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ConveyorGame.Services.GameData
{
    public class GameDataService : ServiceBase
    {
        public IReadOnlyList<ProductModel> ProductModels { get; private set; }
        public IReadOnlyList<TaskModel> TaskModels { get; private set; }
        public TaskModel CurrentTask => TaskModels[ServiceProvider.PlayerData.Data.Level];

        public override UniTask InitializeAsync()
        {
            Application.targetFrameRate = 120;
            ProductModels = Resources.LoadAll<ProductModel>("Products");
            TaskModels = Resources.LoadAll<TaskModel>("Tasks");
            
            return base.InitializeAsync();
        }
    }
}