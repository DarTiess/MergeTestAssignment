using Card;

namespace DefaultNamespace
{
    public interface ICardSpawner
    {
        /// <summary>
        /// Spawn card on random position
        /// </summary>
        /// <param name="config">Cards configuration</param>
        void SpawnCardOnRandomPosition(CardConfig config);
    }
}