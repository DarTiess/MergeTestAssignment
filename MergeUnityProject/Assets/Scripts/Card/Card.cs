using System;
using Card.States;
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
        private ParticleSystem _effect;
        private Vector3 _startPosition;
        
        public Sprite CurrentIcon => _currenIcon;
        public event Action CardUpgrade;

        public void Initialize(CardConfig config, ParticleSystem effect)
        {
            _config = config;
            _effect = effect;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _camera=Camera.main;
            
            _stateMachine = new CardStateMachine();
           _stateMachine.AddState(new BeginState(_stateMachine, _spriteRenderer, _config.Icon[0]));
            _stateMachine.AddState(new FirstUpdateState(_stateMachine, _spriteRenderer, _config.Icon[1]));
            _stateMachine.AddState(new SecondUpdateState(_stateMachine, _spriteRenderer, _config.Icon[2]));
            _stateMachine.AddState(new FinishedState(_stateMachine, _spriteRenderer, _config.Icon[3]));
            _stateMachine.AddState(new EndState(_stateMachine, _spriteRenderer, null));

           _currenIcon= _stateMachine.SetState<BeginState>();
           _currentState = _stateMachine.GetCurrentState();
           _startPosition = transform.position;

        }

        private void OnMouseDown()
        {
            _isSelected = false;
            _startPosition = transform.position;
            _offsetX = _camera.ScreenToWorldPoint(Input.mousePosition).x-transform.position.x;
            _offsetY = _camera.ScreenToWorldPoint(Input.mousePosition).y-transform.position.y;
        }

        private void OnMouseDrag()
        {
            _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(_mousePosition.x - _offsetX, _mousePosition.y - _offsetY);
        }

        private void OnMouseUp()
        {
            _isSelected = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Card card) && _isSelected && !_onComparing)
            {
                TryMergeCard(card);
            }
           
        }

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

        private void MergeCard(Card card)
        {
            _onComparing = true;
            card.ChangeIconLevel();
            _effect.transform.position = transform.position;
            _effect.Play();
            gameObject.SetActive(false);
        }

        private void ChangeIconLevel()
        {
            _currenIcon =_currentState.Exit();
            _currentState = _stateMachine.GetCurrentState();
            CardUpgrade?.Invoke();
        }

        private bool HasSameConfig(Sprite icon)
        {
            return _currenIcon.Equals(icon);
        }
    }
}