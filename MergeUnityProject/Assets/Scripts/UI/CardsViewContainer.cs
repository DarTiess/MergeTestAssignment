using DefaultNamespace;
using UnityEngine;

namespace UI
{
    public class CardsViewContainer: MonoBehaviour
    {
        [SerializeField] private CardConfig[] cardConfigs;
        [SerializeField] private Transform container;
        [SerializeField] private CardView cardViewPrefab;
        [SerializeField] private Card cardprefab;
        [SerializeField] public CardSpawner cardSpawner;

        private void Start()
        {
            for (int i = 0; i < cardConfigs.Length; i++)
            {
                AddItem(cardConfigs[i]);
            }
        }

        private void AddItem(CardConfig config)
        {
            CardView cardView= Instantiate(cardViewPrefab, container);
            cardView.Initialize(config);
            cardView.SelectedCard += OnItemSelected;
        }
        
        private void OnItemSelected(CardConfig config)
        {
           cardSpawner.SpawnRandomCard(config);
        }
    }
}