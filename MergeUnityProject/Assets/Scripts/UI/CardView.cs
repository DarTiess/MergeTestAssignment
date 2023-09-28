using System;
using Card;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class CardView : MonoBehaviour
    {
        [SerializeField] 
        private Image iconImage;
        
        private CardConfig _config;
        private Button _selectedButton;

        public event Action<CardConfig> SelectedCard;

        /// <summary>
        /// Get Button component.
        /// Subscribe to action OnClick
        /// </summary>
        private void OnEnable()
        {
            _selectedButton = GetComponent<Button>();
            _selectedButton.onClick.AddListener(OnCardSelected);
        }
        /// <summary>
        /// Unsubscribe of action if object disable
        /// </summary>
        private void OnDisable()
        {
            _selectedButton.onClick.RemoveListener(OnCardSelected);
        }

        /// <summary>
        /// Post action if card was selected
        /// </summary>
        private void OnCardSelected()
        {
            SelectedCard?.Invoke(_config);
        }

        /// <summary>
        /// Initialize cards parameters
        /// </summary>
        /// <param name="config"></param>
        public void Initialize(CardConfig config)
        {
            _config = config;
            iconImage.sprite = _config.Icon[0];
        }
    }
}