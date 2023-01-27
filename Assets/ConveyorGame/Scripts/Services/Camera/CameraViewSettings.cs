using System;
using Sirenix.Serialization;
using UnityEngine;

namespace ConveyorGame.Services.Camera
{
    [Serializable]
    public class CameraViewSettings
    {
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _angle;

        [OdinSerialize] public Vector3 Position => _position;
        [OdinSerialize] public Vector3 Angle => _angle;

        public CameraViewSettings(Vector3 position, Vector3 angle)
        {
            _position = position;
            _angle = angle;
        }
    }
}