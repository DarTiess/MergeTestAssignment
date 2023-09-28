using System;
using Card.States;
using DG.Tweening;
using UnityEngine;
using State = Card.States.State;

namespace Card
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private CardConfig _config;
        private Vector2 _mousePosition;
        private float _offsetX;
        private float _offsetY;
        private bool _isSelected;
        private Camera _camera;
        private Sprite _currenIcon;
        private CardStateMachine _stateMachine;
        private State _currentState;
        private bool _onComparing;
        private Vector3 _startPosition;
        private float _animateDuration;
        private ICoinsAnimationView _coinsAnimation;

        public Sprite CurrentIcon => _currenIcon;
        public event Action CardUpgrade;
        public event Action<int, CardType> CollectCard;

        /// <summary>
        /// Create State Machine, add states, get current state
        /// </summary>
        private void CreateStateMachine()
        {
            _stateMachine = new CardStateMachine();
            _stateMachine.AddState(new BeginState(_stateMachine, _spriteRenderer, _config.Icon[0]));
            _stateMachine.AddState(new FirstUpdateState(_stateMachine, _spriteRenderer, _config.Icon[1]));
            _stateMachine.AddState(new SecondUpdateState(_stateMachine, _spriteRenderer, _config.Icon[2]));
            _stateMachine.AddState(new FinishedState(_stateMachine, _spriteRenderer, _config.Icon[3]));
            _stateMachine.AddState(new EndState(this));

            _currenIcon = _stateMachine.SetState<BeginState>();
            _currentState = _stateMachine.GetCurrentState();
        }
        /// <summary>
        /// Action OnMouseDown on object
        /// </summary>
        private void OnMouseDown()
        {
            _isSelected = false;
            
            _offsetX = _camera.ScreenToWorldPoint(Input.mousePosition).x-transform.position.x;
            _offsetY = _camera.ScreenToWorldPoint(Input.mousePosition).y-transform.position.y;
        }
        /// <summary>
        /// Action OnMouseDrag on object
        /// </summary>
        private void OnMouseDrag()
        {
            _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(_mousePosition.x - _offsetX, _mousePosition.y - _offsetY);
        }
        /// <summary>
        /// Action OnMouseUp on object
        /// </summary>
        private void OnMouseUp()
        {
            _isSelected = true;
        }
        /// <summary>
        /// If Trigger with other card
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Card card) && _isSelected && !_onComparing)
            {
                TryMergeCard(card);
            }
        }
        /// <summary>
        /// Check if card same icon
        /// if not => return to start place
        /// </summary>
        /// <param name="card"></param>
        private void TryMergeCard(Card card)
        {
            if (HasSameConfig(card.CurrentIcon))
            {
                MergeCard(card);
            }
            else
            {
                transform.position = _startPosition;
            }
        }
        /// <summary>
        /// Change icon at another card
        /// deactivate this card
        /// </summary>
        /// <param name="card"></param>
        private void MergeCard(Card card)
        {
            _onComparing = true;
            card.ChangeIconLevel();
            gameObject.SetActive(false);
        }
        /// <summary>
        /// Change icon
        /// play particle effect
        /// make action
        /// </summary>
        private void ChangeIconLevel()
        {
           _coinsAnimation.ShowUpgradeEffect(transform);
            _currenIcon =_currentState.Exit();
            _currentState = _stateMachine.GetCurrentState();
            CardUpgrade?.Invoke();
        }
        /// <summary>
        /// Check if have same icon
        /// </summary>
        /// <param name="icon"></param>
        /// <returns>true/false</returns>
        private bool HasSameConfig(Sprite icon)
        {
            if (gameObject.activeInHierarchy)
            {
                return _currenIcon.Equals(icon);
            }
            return false;
        }
        /// <summary>
        /// Animate card
        /// Deactivate card
        /// Make action
        /// </summary>
        public void HideCard()
        {
            transform.DOScale(Vector3.zero, _animateDuration).SetEase(Ease.InBounce)
                     .OnComplete(() =>
                     {
                         _coinsAnimation.GetCoinsAnimation(transform);
                         CollectCard?.Invoke(_config.Price, _config.CardType);
                         gameObject.SetActive(false);
                     });
        }

        /// <summary>
        /// Initialize Card, after create
        /// </summary>
        /// <param name="config"></param>
        /// <param name="effect"></param>
        public void Initialize(CardConfig config, ICoinsAnimationView coinsAnimation)
        {
            _config = config;
            _animateDuration = config.AnimateDuration;
            _coinsAnimation = coinsAnimation;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _camera=Camera.main;
            CreateStateMachine();
            _startPosition = transform.position;
            _onComparing = false;
        }
    }
}