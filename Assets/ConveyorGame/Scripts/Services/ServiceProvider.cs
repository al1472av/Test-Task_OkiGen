using ConveyorGame.Services.Audio;
using ConveyorGame.Services.Camera;
using ConveyorGame.Services.GameData;
using ConveyorGame.Services.Level;
using ConveyorGame.Services.PlayerData;
using ConveyorGame.Services.WaypointMovement;

namespace ConveyorGame.Services
{
    public static class ServiceProvider
    {
        public static PlayerDataService PlayerData => ServiceLocator.GetService<PlayerDataService>();
        public static GameDataService GameData => ServiceLocator.GetService<GameDataService>();
        public static CameraService CameraService => ServiceLocator.GetService<CameraService>();
        public static WaypointMovementService WaypointMovementService => ServiceLocator.GetService<WaypointMovementService>();
        public static LevelService LevelService => ServiceLocator.GetService<LevelService>();
    }
}