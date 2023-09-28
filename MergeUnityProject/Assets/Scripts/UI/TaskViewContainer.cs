using System;
using Card;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TaskViewContainer : MonoBehaviour
    {
        [SerializeField] 
        private Image[] taskImage;
        
        private CardConfig[] _configs;
        private int _indexTask;
        private float _animateDuration;
        public event Action AllTaskEnds;
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="configs"></param>
        /// <param name="animationDuration"></param>
        public void Initialize(CardConfig[] configs, float animationDuration)
        {
            _configs = configs;
            _animateDuration = animationDuration;

            for (int i = 0; i < taskImage.Length; i++)
            {
                taskImage[i].sprite = _configs[i].Icon[_configs.Length-1];
            }
        }
        /// <summary>
        /// On Collect Card, compare with configs list
        /// To find index, wich taskImage must be deactivate
        /// </summary>
        /// <param name="type"></param>
        public void CollectCard(CardType type)
        {
            for (int i = 0; i < _configs.Length; i++)
            {
                if (_configs[i].CardType == type)
                {
                    taskImage[i].gameObject.transform.DOScale(Vector3.zero, _animateDuration).SetEase(Ease.InBounce)
                                .OnComplete(() => 
                                {
                                    taskImage[i].gameObject.SetActive(false);
                                });
                    _indexTask++; 
                    
                    if (_indexTask == taskImage.Length)
                    {
                       AllTaskEnds?.Invoke();
                    }
                }
            }
        }
    }
}