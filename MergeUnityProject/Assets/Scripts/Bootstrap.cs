using Card;
using Infrastructure.Level;
using UI;
using UnityEngine;
using UnityEngine.Serialization;


namespace DefaultNamespace
{
    public class Bootstrap: MonoBehaviour
    {
        [Space]
        [Header("Level Settings")]
        [SerializeField] 
        private LevelLoader levelLoader;
        [SerializeField] 
        private float timeWin;
        [SerializeField] 
        private float timeLose;
        [Space]
        [Header("UI Settings")]
        [SerializeField] 
        private UIControl canvasPrefab;
        [SerializeField] 
        private float coinAnimateSize;
        [SerializeField] 
        private float coinAnimatedDuration;
        [Space]
        [Header("CardsSettings")]
        [SerializeField] 
        private Card.Card cardPrefab;
        [SerializeField] 
        private CardConfig[] cardConfigs;
        [Space]
        [Header("Effects Settings")]
        [SerializeField] 
        private ParticleSystem exposionEffect;
        [SerializeField]
        private CoinsAnimationView coinsAnimationPrefab;
        [SerializeField]
        private Transform coinsUiPosition;
        [SerializeField]
        private Transform spendCoinsUiPosition;
        [SerializeField]
        private float animationDuration;
        [Space]
        [Header("Grid Settings")]
        [SerializeField] 
        private GridBuilder _cardGridBuilder;
        [SerializeField] 
        private Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField] 
        private float cellSize = 1f;
        [SerializeField] 
        private float cellSpacing = 0.1f;
        [Space]
        [Header("Sounds Settings")]
        [SerializeField] 
        private SoundConfig soundsConfig;


        private LevelManager _levelManager;
        private UIControl _canvas;
        private CardSpawner _cardRandomSpawner;
        private ParticleSystem _effect;
        private AudioPlayer _audioPlayer;
        private SoundPlayer _soundPlayer;
        private AudioSource _musicSource;
        private AudioSource _soundsSource;
        private Coins _coins;
        private CoinsAnimationView _coinsAnimation;

        private void Awake()
        {
            CreateLevelManager();
            CreateExplosionEffect();
            InitializeCardGridGenerator();
            CreateCoinsData();
            CreateCardSpawner();
            CreateAndInitUI();
            CreateAudioPlayer();
            CreateSoundsPlayer();
        }
        /// <summary>
        /// Create Coins Container
        /// </summary>
        private void CreateCoinsData()
        {
            _coins = new Coins(_cardGridBuilder);
        }
        /// <summary>
        /// Create level manager
        /// </summary>
        private void CreateLevelManager()
        {
            _levelManager = new LevelManager(timeLose, timeWin);
        }
        /// <summary>
        /// Create one particle effect
        /// </summary>
        private void CreateExplosionEffect()
        {
            _effect = Instantiate(exposionEffect);
            _coinsAnimation = Instantiate(coinsAnimationPrefab);
            _coinsAnimation.Initialize(coinsUiPosition, spendCoinsUiPosition, _effect, animationDuration);
        }
        /// <summary>
        /// Initialize card grid builder
        /// </summary>
        private void InitializeCardGridGenerator()
        {
            _cardGridBuilder.Initialize(gridSize, cellSize, cellSpacing, cardPrefab, cardConfigs, _coinsAnimation);
        }
        /// <summary>
        /// Create card spawner
        /// </summary>
        private void CreateCardSpawner()
        {
            _cardRandomSpawner = new CardSpawner(_cardGridBuilder,_coins, _coinsAnimation);
        }
        /// <summary>
        /// Create UI
        /// </summary>
        private void CreateAndInitUI()
        {
            _canvas = Instantiate(canvasPrefab);
            _canvas.Init(_levelManager, _levelManager, levelLoader, cardConfigs, _cardRandomSpawner, _coins, coinAnimateSize, coinAnimatedDuration);
        }
        /// <summary>
        /// Create AudioPlayer
        /// </summary>
        private void CreateAudioPlayer()
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
            _audioPlayer = new AudioPlayer(_musicSource, soundsConfig.MusicInGame);
        }
        /// <summary>
        /// Create Sound Player
        /// </summary>
        private void CreateSoundsPlayer()
        {
            _soundsSource = gameObject.AddComponent<AudioSource>();
            _soundPlayer = new SoundPlayer(_soundsSource, soundsConfig.SoundsData, _canvas, _cardGridBuilder, _coins);
        }
    }
}