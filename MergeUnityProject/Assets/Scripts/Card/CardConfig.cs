using UnityEngine;

namespace Card
{
    [CreateAssetMenu(menuName = "Configs/Cards", fileName = "CardsConfig", order = 51)]
    public class CardConfig : ScriptableObject
    {
        [SerializeField] 
        private Sprite[] icon;
        [SerializeField] 
        private int price;
        [SerializeField] 
        private float animateDuration;
        [SerializeField] 
        private CardType cardType;

        public Sprite[] Icon=>icon;
        public int Price=>price;
        public float AnimateDuration => animateDuration;
        public CardType CardType=>cardType;
    }
    
    
}