using UnityEngine;

namespace Card.States
{
    public abstract class State 
    {
        protected readonly CardStateMachine _cardStateMachine;
        protected Sprite _icon;
        protected SpriteRenderer _spriteRenderer;

        public State(CardStateMachine cardStateMachine,SpriteRenderer spriteRenderer, Sprite icon)
        {
            _cardStateMachine = cardStateMachine;
            _spriteRenderer = spriteRenderer;
            _icon = icon;
        }

        public virtual Sprite Enter()
        {
            _spriteRenderer.sprite = _icon;
            return _icon;
        }

        public virtual Sprite Exit()
        {
            return null;
            
        }
        public virtual void Update(){}

    }
}