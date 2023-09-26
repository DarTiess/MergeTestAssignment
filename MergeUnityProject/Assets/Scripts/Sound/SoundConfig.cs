using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Configs/Audio", fileName = "SoundsConfig", order = 51)]
    public class SoundConfig : ScriptableObject
    {
        [SerializeField] private AudioClip musicInGame;
        [SerializeField] private SoundsData[] soundsDatas;

        public AudioClip MusicInGame => musicInGame;
        public SoundsData[] SoundsData => soundsDatas;
    }
}