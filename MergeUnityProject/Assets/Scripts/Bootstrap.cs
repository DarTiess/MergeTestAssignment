using Card;
using Infrastructure.Level;
using UI;
using UnityEngine;


namespace DefaultNamespace
{
    public class Bootstrap: MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] private float timeWin;
        [SerializeField] private float timeLose;
        [Header("UI Settings")]
        [SerializeField] private UIControl canvasPrefab;
        [Header("CardsConfigs")]
        [SerializeField] private Card.Card cardPrefab;
        [SerializeField] private CardConfig[] cardConfigs;
        [SerializeField] private ParticleSystem exposionEffect;
        [Header("Grid Settings")]
        [SerializeField] private GridSpawner gridSpawner;
        [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private float cellSpacing = 0.1f;
        [Header("Sounds Settings")]
        [SerializeField] private SoundConfig soundsConfig;
        

        private LevelManager _levelManager;
        private UIControl _canvas;
        private CardSpawner _cardSpawner;
        private ParticleSystem _effect;
        private AudioPlayer _audioPlayer;
        private SoundPlayer _soundPlayer;
        private AudioSource _musicSource;
        private AudioSource _soundsSource;

        private void Awake()
        {
            CreateLevelManager();
            InitializeGridSpawner();
            CreateExplosionEffect();
            CreateCardSpawner();
            CreateAndInitUI();
            CreateAudioPlayer();
            CreateSoundsPlayer();
        }

        private void CreateLevelManager()
        {
            _levelManager = new LevelManager(timeLose, timeWin);
        }

        private void InitializeGridSpawner()
        {
            gridSpawner.Init(gridSize, cellSize, cellSpacing);
        }

        private void CreateExplosionEffect()
        {
            _effect = Instantiate(exposionEffect);
        }

        private void CreateCardSpawner()
        {
            _cardSpawner = new CardSpawner(gridSpawner, cardPrefab, _effect);
        }

        private void CreateAndInitUI()
        {
            _canvas = Instantiate(canvasPrefab);
            _canvas.Init(_levelManager, _levelManager, levelLoader, cardConfigs, _cardSpawner);
        }

        private void CreateAudioPlayer()
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
            _audioPlayer = new AudioPlayer(_musicSource, _soundsSource, soundsConfig.MusicInGame);
        }

        private void CreateSoundsPlayer()
        {
            _soundsSource = gameObject.AddComponent<AudioSource>();
            _soundPlayer = new SoundPlayer(_soundsSource, soundsConfig.SoundsData, _canvas, _cardSpawner);
        }
    }
}