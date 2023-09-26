using DG.Tweening;
using UnityEngine;

namespace Card.States
{
    public class EndState: State
    {
        public EndState(CardStateMachine cardStateMachine, SpriteRenderer spriteRenderer, Sprite icon) : base(cardStateMachine, spriteRenderer, icon)
        {
            
        }
        public override Sprite Enter()
        {
            _spriteRenderer.gameObject.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBounce)
                           .OnComplete(() =>
                           {
                               _spriteRenderer.gameObject.SetActive(false);
                           });
            return null;
        }
    }
}