﻿using UnityEngine;

namespace Card.States
{
    public class BeginState: State
    {
        public BeginState(CardStateMachine cardStateMachine, SpriteRenderer spriteRenderer, Sprite icon) : base(cardStateMachine, spriteRenderer, icon)
        {
            
        }

        public override Sprite Exit()
        {
           return _cardStateMachine.ChangeState<FirstUpdateState>();
        }
    }
}