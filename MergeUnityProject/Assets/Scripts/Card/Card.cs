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
      

        public Sprite CurrentIcon => _currenIcon;

        public void Initialize(CardConfig config)
        {
            _config = config;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _camera=Camera.main;
            
            _stateMachine = new CardStateMachine();
           _stateMachine.AddState(new BeginState(_stateMachine, _spriteRenderer, _config.Icon[0]));
            _stateMachine.AddState(new FirstUpdateState(_stateMachine, _spriteRenderer, _config.Icon[1]));
            _stateMachine.AddState(new SecondUpdateState(_stateMachine, _spriteRenderer, _config.Icon[2]));
            _stateMachine.AddState(new FinishedState(_stateMachine, _spriteRenderer, _config.Icon[3]));

           _currenIcon= _stateMachine.SetState<BeginState>();
           _currentState = _stateMachine.GetCurrentState();

        }

        private void OnMouseDown()
        {
            _isSelected = false;
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
            if (other.TryGetComponent(out Card card) && _isSelected)
            {
                if (HasSameConfig(card.CurrentIcon))
                {
                    Debug.Log("Find Same");
                    card.ChangeIconLevel();
                    gameObject.SetActive(false);
                }
            }
        }

        private void ChangeIconLevel()
        {
            _currenIcon =_currentState.Exit();
            _currentState = _stateMachine.GetCurrentState();
           Debug.Log(_currentState.GetType());
        }

        private bool HasSameConfig(Sprite icon)
        {
            return _currenIcon.Equals(icon);
        }
    }
}