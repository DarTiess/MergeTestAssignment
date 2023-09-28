using DG.Tweening;
using UnityEngine;

namespace Card.States
{
    public class EndState: State
    {
        private Card _card;
        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="cardStateMachine"></param>
        /// <param name="spriteRenderer"></param>
        /// <param name="icon"></param>
        public EndState(Card card)
        {
            _card = card;
        }
        /// <summary>
        /// On Enter deactivate card
        /// </summary>
        /// <returns>null</returns>
        public override Sprite Enter()
        {
            _card.HideCard();
            return null;
        }
    }
}