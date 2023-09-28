using DG.Tweening;
using UnityEngine;

namespace Card
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CoinsAnimationView : MonoBehaviour, ICoinsAnimationView
    {
        private Transform _addPosition;
        private Transform _spendPosition;
        private ParticleSystem _upgradeEffect;
        private SpriteRenderer _spriteRenderer;
        private float _animationDuration;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="coinsUiPosition"></param>
        /// <param name="spendPosition"></param>
        /// <param name="upgradeEffect"></param>
        /// <param name="animationDuration"></param>
        public void Initialize(Transform coinsUiPosition,Transform spendPosition, ParticleSystem upgradeEffect, float animationDuration)
        {
            _addPosition = coinsUiPosition;
            _upgradeEffect = upgradeEffect;
            _animationDuration = animationDuration;
            _spendPosition = spendPosition;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.enabled = false;
        }
        /// <summary>
        /// Activate spriteRendere and play GetCoin animation 
        /// </summary>
        /// <param name="startPosition"></param>
        public void GetCoinsAnimation(Transform startPosition)
        {
            _spriteRenderer.transform.position = startPosition.position;
            _spriteRenderer.enabled = true;
            _spriteRenderer.transform.DOMove(_addPosition.position, _animationDuration)
                           .OnComplete(() =>
                           {
                               _spriteRenderer.enabled = false;
                           });

        }
        /// <summary>
        /// Activate spriterendere and Play SpendCoin Animation
        /// </summary>
        public void SpendCoinsAnimation()
        {
            _spriteRenderer.transform.position =_addPosition.position;
            _spriteRenderer.enabled = true;
            _spriteRenderer.transform.DOMove(_spendPosition.position, _animationDuration)
                           .OnComplete(() =>
                           {
                               _spriteRenderer.enabled = false;
                           });

        }
        /// <summary>
        /// Play explosion effect
        /// </summary>
        /// <param name="setPosition"></param>
        public void ShowUpgradeEffect(Transform setPosition)
        {
            _upgradeEffect.transform.position = setPosition.position;
            _upgradeEffect.Play();
        }
    }
}