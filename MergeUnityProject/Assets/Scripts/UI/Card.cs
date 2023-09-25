using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        public void Initialize(CardConfig config)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = config.Icon;
        }
    }
}