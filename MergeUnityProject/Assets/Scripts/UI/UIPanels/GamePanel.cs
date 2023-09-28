using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanels
{
    public class GamePanel: PanelBase
    {
        [SerializeField] 
        private Text _coinsText;
        
        private float _maxScaleSize;
        private float _animatedDuration;
        public virtual event Action ClickedPanel;
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="maxScaleSize"></param>
        /// <param name="animatedDuration"></param>
        public void Initialize(float maxScaleSize, float animatedDuration)
        {
            _maxScaleSize = maxScaleSize;
            _animatedDuration = animatedDuration;
            _button.gameObject.SetActive(false);
        }
        /// <summary>
        /// If Clicked button, invoke event
        /// </summary>
        protected override void OnClickedPanel()
        {
            ClickedPanel?.Invoke();
        }
        /// <summary>
        /// Update coins text on UI
        /// </summary>
        /// <param name="price"></param>
        public void UpdateCoinsTextView(int price)
        {
            if (price == 0)
            {
             _button.gameObject.SetActive(true);   
             _button.gameObject.transform.DOScale(_maxScaleSize,_animatedDuration*2).SetLoops(-1, LoopType.Yoyo);
            }
            else if (price > 0 && _button.gameObject.activeInHierarchy)
            {
                _button.gameObject.SetActive(false);
            }
            _coinsText.text = price.ToString();
            _coinsText.transform.DOScale(_maxScaleSize,_animatedDuration).SetLoops(2, LoopType.Yoyo);
        }
    }
}