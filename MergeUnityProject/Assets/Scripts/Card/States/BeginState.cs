using UnityEngine;

namespace Card.States
{
    public class BeginState: State
    {
        public BeginState(CardStateMachine cardStateMachine, SpriteRenderer spriteRenderer, Sprite icon) : base(cardStateMachine, spriteRenderer, icon)
        {
            
        }

        public override Sprite Enter()
        {
            _spriteRenderer.sprite = _icon;
            return _icon;
        }

        public override Sprite Exit()
        {
           return _cardStateMachine.ChangeState<FirstUpdateState>();
        }
    }
}