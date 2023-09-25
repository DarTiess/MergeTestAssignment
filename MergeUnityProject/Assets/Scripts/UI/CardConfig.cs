using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Configs/Cards", fileName = "CorsConfig", order = 51)]
    public class CardConfig : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private int price;
        [SerializeField] private CardType cardType;
        [SerializeField] private CardLevel cardLevel;
        
        public Sprite Icon=>icon;
        public int Price=>price;
        public CardType CardType=>cardType;
        public CardLevel CardLevel=>cardLevel;
    }
}