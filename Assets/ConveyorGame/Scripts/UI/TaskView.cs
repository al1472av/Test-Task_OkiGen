using System.Collections.Generic;
using System.Linq;
using ConveyorGame.ScriptableObjects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ConveyorGame.UI
{
    public class TaskView : SerializedMonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI _taskText;
        [SerializeField] private TextMeshProUGUI _titleTask;
        [SerializeField] private TextMeshProUGUI _titleWin;
        private TaskModel _currentTaskModel;
        private Dictionary<ProductModel, int> _collectedProducts;

        [OdinSerialize] public Button NextLevelButton { get; private set; }

        public void Initialize(TaskModel taskModel)
        {
            _currentTaskModel = taskModel;
            SetLayoutState(true, false, false);
            Refresh(new List<ProductModel>());
        }

        public void Refresh(List<ProductModel> collectedProducts)
        {
            _collectedProducts = collectedProducts.GroupBy(v => v).ToDictionary(t => t.Key, t => t.Count());

            string str = "";
            
            foreach (var product in _currentTaskModel.ProductsToCollect)
            {
                int currentCount = _collectedProducts.ContainsKey(product.Key) ? _collectedProducts[product.Key] : 0;
                Color color = currentCount >= product.Value ? Color.green : Color.white;
                str += $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{product.Key.Name} {currentCount}/{product.Value}</color>\n";
            }

            _taskText.text = str;
        }
        
        public void ShowWinLayout()
        {
            SetLayoutState(false, true, true);
        }

        private void SetLayoutState(bool titleTaskState, bool titleWinState, bool buttonState)
        {
            _titleTask.gameObject.SetActive(titleTaskState);
            _titleWin.gameObject.SetActive(titleWinState);
            NextLevelButton.gameObject.SetActive(buttonState);
        }
    }
}