using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConveyorGame.GameCore.ProductLogic;
using ConveyorGame.ScriptableObjects;
using ConveyorGame.Services;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace ConveyorGame.GameCore
{
    public class Сonveyor : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _spawnFrequence;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Product _productPrefab;

        private bool _isEnable;
        private ObjectPool<Product> _objectsPool;
        private List<Product> _productsOnBelt;
        private ProductModel[] _availableProductModel;

        public event Action<Product> ProductClicked;

        public void Initialize()
        {
            _productsOnBelt = new List<Product>();
            _isEnable = true;
            _objectsPool = new ObjectPool<Product>(
                createFunc: CreateProduct,
                actionOnGet: OnGetProduct,
                actionOnRelease: OnReleaseProduct);

            _availableProductModel = ServiceProvider.GameData.ProductModels.ToArray();

            StartCoroutine(SpawnRoutine());

            //Implement Start\Stop logic
            IEnumerator SpawnRoutine()
            {
                while (true)
                {
                    var product = _objectsPool.Get();
                    yield return product.SetModel(_availableProductModel[Random.Range(0, _availableProductModel.Length)]);
                    yield return new WaitForSeconds(_spawnFrequence);
                }
            }
            
        }

        public void Disable()
        {
            _isEnable = false;
            for (int i = 0; i < _productsOnBelt.Count; i++)
                _productsOnBelt[i].gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            TryMoveProducts();
        }

        private void TryMoveProducts()
        {
            if (_productsOnBelt != null && _isEnable)
            {
                foreach (var productController in _productsOnBelt)
                {
                    productController.transform.Translate(transform.forward * (_moveSpeed * Time.deltaTime));
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Product product))
            {
                if (product.ProductState == ProductState.OnConveyor)
                {
                    _objectsPool.Release(product);
                }
            }
        }

        private void OnProductClicked(Product product)
        {
            if (!ServiceProvider.LevelService.GameIsEnd)
            {
                ProductClicked?.Invoke(product);
                _productsOnBelt.Remove(product);
            }
        }

        private Product CreateProduct()
        {
            var obj = Instantiate(_productPrefab);
            return obj;
        }

        private void OnGetProduct(Product product)
        {
            product.transform.position = _startPoint.position;
            product.gameObject.SetActive(true);
            product.Clicked += OnProductClicked;
            _productsOnBelt.Add(product);
        }

        private void OnReleaseProduct(Product product)
        {
            product.Clicked -= OnProductClicked;
            product.gameObject.SetActive(false);
            _productsOnBelt.Remove(product);
        }
    }
}