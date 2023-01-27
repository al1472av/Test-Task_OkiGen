using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ExternalTools.Scripts.Utilities;

namespace ConveyorGame.Services.WaypointMovement
{
    public class WaypointMovementService : ServiceBase
    {
        private List<WaypointsMovement> _movements;
        private float _minDistance;
        
        public override UniTask StartAsync()
        {
            _movements = new List<WaypointsMovement>();
            return base.StartAsync();
        }

        public void AddMovement(WaypointsMovement movement)
        {
            _movements.Add(movement);
        }

        private void Update()
        {

            for (int i = 0; i < _movements.Count; i++)
            {
                if (!_movements[i].IsDone)
                {
                    _movements[i].Move();
                }
                else
                {
                    _movements.Remove(_movements[i]);
                }
            }
            
        }
    }
}