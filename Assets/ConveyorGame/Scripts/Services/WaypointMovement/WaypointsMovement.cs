using System;
using UnityEngine;

namespace ConveyorGame.Services.WaypointMovement
{
    public class WaypointsMovement
    {

        public event Action Update;
        private readonly Transform _transformToMove;
        public Vector3[] Waypoints;
        private readonly float _speed;
        private readonly float _minDistance;
        private readonly Action _onComplete;
        private int _currentWaypointIndex;
        public bool IsDone { get; private set; }

        public WaypointsMovement(Transform transformToMove, Vector3[] waypoints, float speed, float minDistance, Action onComplete)
        {
            _onComplete = onComplete;
            _minDistance = minDistance;
            _speed = speed;
            Waypoints = waypoints;
            _transformToMove = transformToMove;
            _currentWaypointIndex = 0;
        }

        public void Move()
        {
            if (IsDone)
            {
                return;
            }

            if (Vector3.Distance(Waypoints[_currentWaypointIndex], _transformToMove.transform.position) < _minDistance)
            {
                Update?.Invoke();

                _currentWaypointIndex++;
                if (_currentWaypointIndex >= Waypoints.Length)
                {
                    IsDone = true;
                    _onComplete?.Invoke();
                    return;
                }
            }

            _transformToMove.transform.position = Vector3.MoveTowards(_transformToMove.position, Waypoints[_currentWaypointIndex], Time.deltaTime * _speed);
        }
    }
}