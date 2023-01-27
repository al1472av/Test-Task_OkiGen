using Cysharp.Threading.Tasks;

namespace ConveyorGame.Services.Addressables
{
    public class AddressablesService : ServiceBase
    {
        public override async UniTask InitializeAsync()
        {
            await UnityEngine.AddressableAssets.Addressables.InitializeAsync();
        }
    }
}
