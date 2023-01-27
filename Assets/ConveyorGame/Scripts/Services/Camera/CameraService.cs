using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ConveyorGame.Services.Camera
{
    public class CameraService : ServiceBase
    {
        private UnityEngine.Camera _camera;
        
        public override UniTask StartAsync()
        {
            _camera = UnityEngine.Camera.main;
            return base.StartAsync();
        }

        public void MoveCamera(CameraViewSettings cameraViewSettings, float time = 0, Action callabck = null)
        {
            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(_camera.transform.DOMove(cameraViewSettings.Position, time))
                .Insert(0, _camera.transform.DORotateQuaternion(Quaternion.Euler(cameraViewSettings.Angle), time))
                .OnComplete(() => callabck?.Invoke());
        }
    }
}