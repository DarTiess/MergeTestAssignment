using UnityEngine;

namespace Card.States
{
    public class FirstUpdateState: State
    {
        public FirstUpdateState(CardStateMachine cardStateMachine, SpriteRenderer spriteRenderer, Sprite icon) : base(cardStateMachine, spriteRenderer, icon)
        {
            
        }
        public override Sprite Exit()
        {
            return _cardStateMachine.ChangeState<SecondUpdateState>();
        }
    }
}