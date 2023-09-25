using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        private int _price;
        private CardConfig _config;
        private Button _selectedButton;

        public event Action<CardConfig> SelectedCard;

        private void OnEnable()
        {
            _selectedButton = GetComponent<Button>();
            _selectedButton.onClick.AddListener(OnCardSelected);
        }

        private void OnDisable()
        {
            _selectedButton.onClick.RemoveListener(OnCardSelected);
        }

        public void Initialize(CardConfig config)
        {
            _config = config;
            iconImage.sprite = _config.Icon;
        }

        private void OnCardSelected()
        {
            SelectedCard?.Invoke(_config);
        }
    }
}