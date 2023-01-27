using System;
using ConveyorGame.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ConveyorGame.GameCore.ProductLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Product : MonoBehaviour
    {
        public ProductModel Model { get; private set; }
        public ProductState ProductState { get; private set; }
        private ProductView _view;
        private Rigidbody _rigidbody;

        public event Action<Product> Clicked;

        public async UniTask SetModel(ProductModel model)
        {
            Model = model;
            if (_view != null)
                Destroy(_view.gameObject);

            _view = (await model.PrefabView.InstantiateAsync()).GetComponent<ProductView>();
            _view.transform.SetParent(transform);
            _view.transform.localPosition = new Vector3(0, 0, 0);
        }

        public void SetState(ProductState productState)
        {
            //TODO Refactor
            ProductState = productState;

            switch (productState)
            {
                case ProductState.OnConveyor:
                    SetKinematic(false);
                    break;
                case ProductState.Grabbing:
                    SetKinematic(true);
                    break;
                case ProductState.InBasket:
                    SetKinematic(false);
                    break;
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void SetKinematic(bool state)
        {
            _rigidbody.isKinematic = state;
        }

        private void OnMouseUpAsButton()
        {
            Clicked?.Invoke(this);
        }
    }
}