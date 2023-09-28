using UnityEngine;

namespace Card.States
{
    public class FinishedState: State
    {
        private readonly CardStateMachine _cardStateMachine;
        private Sprite _icon;
        private SpriteRenderer _spriteRenderer;
        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="cardStateMachine"></param>
        /// <param name="spriteRenderer"></param>
        /// <param name="icon"></param>
        public FinishedState(CardStateMachine cardStateMachine, SpriteRenderer spriteRenderer, Sprite icon)
        {
            _cardStateMachine = cardStateMachine;
            _spriteRenderer = spriteRenderer;
            _icon = icon;
        }
        /// <summary>
        /// On Enter return actual sprite
        /// </summary>
        /// <returns>sprite</returns>
        public override Sprite Enter()
        {
            _spriteRenderer.sprite = _icon;
            return _icon;
        }
        /// <summary>
        /// On Exit enter to next state
        /// </summary>
        /// <returns>sprite from next state</returns>
        public override Sprite Exit()
        {
            return _cardStateMachine.ChangeState<EndState>();
        }
    }
}