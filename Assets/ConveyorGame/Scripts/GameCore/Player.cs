using System;
using System.Collections.Generic;
using ConveyorGame.GameCore.ProductLogic;
using ConveyorGame.Services;
using ConveyorGame.Services.WaypointMovement;
using DG.Tweening;
using ExternalTools.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ConveyorGame.GameCore
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Basket _basket;
        [SerializeField] private TwoBoneIKConstraint _rightHandIKConstraint;
        [SerializeField] private Transform _leftHandTarget;
        [SerializeField] private Transform _rightHandTarget;
        [SerializeField] private Transform _targetProductPosition;
        [SerializeField] private Transform _handGrabPosition;
        [SerializeField] private Transform _handReleasePosition;
        private bool _isGrabbing;
        private Queue<Product> _productsToGrab = new Queue<Product>(); 
        private Product _productToGrab;

        public void Dance()
        {
            _rightHandIKConstraint.weight = 0;
            _animator.SetTrigger("Dance");
        }

        public void GrabProduct(Product product)
        {
            if (!_productsToGrab.Contains(product))
                _productsToGrab.Enqueue(product);
            if (!_isGrabbing)
            {
                StartGrabbing();
            }
        }

        private void StartGrabbing()
        {
            _isGrabbing = true;
            _productToGrab = _productsToGrab.Dequeue();
            _productToGrab.SetState(ProductState.Grabbing);

            MoveHandTarget(_handGrabPosition, 0);
            ChangeHandRigWeight(1f, 0.2f);
            MoveProductToHand();
        }

        private void ReleaseProduct()
        {
            _productToGrab.transform.SetParent(_targetProductPosition);

            MoveHandTarget(_handReleasePosition, 0.3f, OnMoveEnd);

            void OnMoveEnd()
            {
                _productToGrab.SetState(ProductState.InBasket);
                _productToGrab.transform.SetParent(_basket.transform);

                if (_productsToGrab.Count > 0)
                {
                    MoveHandTarget(_handGrabPosition, 0.2f, StartGrabbing);
                }
                else
                {
                    _isGrabbing = false;
                    ChangeHandRigWeight(0f, 0.3f);
                }
            }
        }

        private void MoveHandTarget(Transform targetTransform, float time, Action onMoveEnd = null)
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_rightHandTarget.DOMove(targetTransform.position, time))
                .Append(_rightHandTarget.DORotate(targetTransform.eulerAngles, time))
                .OnComplete(() => onMoveEnd?.Invoke());
        }

        private void MoveProductToHand()
        {
            //TODO Refactor magic numbers
            var porductTransform = _productToGrab.transform;
            Vector3 middlePos = (porductTransform.position + _targetProductPosition.position) / 2;
            middlePos.y = _rightHandTarget.position.y + 0.2f;
            
            var waypointsMovement = new WaypointsMovement(porductTransform, new Vector3[] { middlePos, _targetProductPosition.position }, 2, 0.1f, () =>
            {
                _productToGrab.transform.SetParent(_targetProductPosition);
                ReleaseProduct();
            });

            waypointsMovement.Update += () =>
            {
                middlePos = (_productToGrab.transform.position + _targetProductPosition.position) / 2;
                middlePos.y = _rightHandTarget.position.y + 0.2f;
                waypointsMovement.Waypoints = new Vector3[] { middlePos, _targetProductPosition.position };
            };

            ServiceProvider.WaypointMovementService.AddMovement(waypointsMovement);
        }

        private void ChangeHandRigWeight(float endValue, float duration)
        {
            DOTween.To(() => _rightHandIKConstraint.weight, v => _rightHandIKConstraint.weight = v, endValue, duration);
        }

    }
}