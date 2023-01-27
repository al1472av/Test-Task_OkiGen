using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ConveyorGame.Services
{
    public class ServiceBase : MonoBehaviour, IService
    {
        public virtual async UniTask InitializeAsync()
        {
            await UniTask.CompletedTask;
        }

        public virtual async UniTask StartAsync()
        {
            await UniTask.CompletedTask;
        }

        public virtual async UniTask StopAsync()
        {
            await UniTask.CompletedTask;
        }
    }
}