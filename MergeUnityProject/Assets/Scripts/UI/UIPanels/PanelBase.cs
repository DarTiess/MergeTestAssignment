using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanels
{
    public abstract class PanelBase : MonoBehaviour
    {
        [SerializeField] 
        protected Button _button;
        /// <summary>
        /// Add action on button
        /// </summary>
        private void Start()
        {
            _button.onClick.AddListener(OnClickedPanel);
        }
        /// <summary>
        /// On disable remove action
        /// </summary>
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickedPanel);
        }
        /// <summary>
        /// Hide panel
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        /// <summary>
        /// Show panel
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }
        /// <summary>
        /// Send action if clicked button
        /// </summary>
        protected virtual void OnClickedPanel()
        {
          
        }
    }
}