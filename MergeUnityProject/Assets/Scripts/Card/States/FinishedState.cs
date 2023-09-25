using UnityEngine;

namespace Card.States
{
    public class FinishedState: State
    {
        public FinishedState(CardStateMachine cardStateMachine, SpriteRenderer spriteRenderer, Sprite icon) : base(cardStateMachine, spriteRenderer, icon)
        {
            
        }
        
    }
}