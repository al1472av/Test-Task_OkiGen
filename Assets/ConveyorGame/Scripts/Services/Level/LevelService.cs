using System.Collections.Generic;
using ConveyorGame.GameCore;
using ConveyorGame.GameCore.ProductLogic;
using ConveyorGame.ScriptableObjects;
using ConveyorGame.Services.Addressables;
using ConveyorGame.Services.Camera;
using ConveyorGame.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ConveyorGame.Services.Level
{
    public class LevelService : ServiceBase
    {
        [SerializeField] private TaskView _taskView;
        [SerializeField] private CameraViewSettings _winView;
        [SerializeField] private AssetReferenceComponent<Player> _playerPrefab;
        [SerializeField] private AssetReferenceComponent<Сonveyor> _conveyorPrefab;
        private Player _player;
        private Сonveyor _conveyor;
        private List<ProductModel> _collectedProducts;

        public bool GameIsEnd { get; private set; }

        public override async UniTask StartAsync()
        {
            _collectedProducts = new List<ProductModel>();
            _taskView.Initialize(ServiceProvider.GameData.CurrentTask);

            await InstantiateLevelElements();

            _conveyor.ProductClicked += OnProductClicked;
            _taskView.NextLevelButton.onClick.AddListener(ReloadLevel);
        }

        private async UniTask InstantiateLevelElements()
        {
            //TODO Need to be in fabric
            _player = (await _playerPrefab.InstantiateAsync()).GetComponent<Player>();
            _conveyor = (await _conveyorPrefab.InstantiateAsync()).GetComponent<Сonveyor>();
            _conveyor.Initialize();
        }

        private void OnProductClicked(Product product)
        {
            _player.GrabProduct(product);
            _collectedProducts.Add(product.Model);
            _taskView.Refresh(_collectedProducts);

            if (ServiceProvider.GameData.CurrentTask.CheckIfTaskIsDone(_collectedProducts))
                EndGame();
        }

        private void ReloadLevel()
        {
            ServiceLocator.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void EndGame()
        {
            void OnCameraMoveEnd()
            {
                _taskView.ShowWinLayout();
                _player.Dance();
                _conveyor.Disable();
            }

            GameIsEnd = true;
            ServiceProvider.CameraService.MoveCamera(_winView, 1.4f, OnCameraMoveEnd);
        }
    }
}