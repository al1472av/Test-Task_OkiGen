using Cysharp.Threading.Tasks;

namespace ConveyorGame.Services
{
    public interface IService
    {
        UniTask InitializeAsync();

        UniTask StartAsync();

        UniTask StopAsync();
    }
}