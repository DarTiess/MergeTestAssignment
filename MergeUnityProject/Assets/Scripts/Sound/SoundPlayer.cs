using System.Linq;
using Card;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class SoundPlayer
    {
        private AudioSource _soundsSource;
        private SoundsData[] _soundsDatas;
        private IUIClicked _uiClicked;
        private ICardUpgrade _cardUpgrade;
        private ICoinsUpdate _coinsUpdate;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="soundsSource"></param>
        /// <param name="soundsData"></param>
        /// <param name="uiClicked"></param>
        /// <param name="cardUpgrade"></param>
        public SoundPlayer(AudioSource soundsSource, SoundsData[] soundsData, IUIClicked uiClicked, ICardUpgrade cardUpgrade, ICoinsUpdate coinsUpdate)
        {
            _soundsSource = soundsSource;
            _soundsDatas = soundsData;
            _uiClicked = uiClicked;
            _uiClicked.CardClicked += OnCardClicked;
            _cardUpgrade = cardUpgrade;
            _cardUpgrade.CardUpgrade += OnCardUpgraded;
            _coinsUpdate = coinsUpdate;
            _coinsUpdate.UpdateCoinsAmount += OnCoinsUpdate;
        }

        /// <summary>
        /// if coins was update
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void OnCoinsUpdate(int arg1, CardType arg2)
        {
            ChangeAudioClip(SoundType.TakeCoinSound);
        }

        /// <summary>
        /// If card view was clicked play sound clicked
        /// </summary>
        private void OnCardClicked()
        {
          ChangeAudioClip(SoundType.ClickSound);
        }
        /// <summary>
        /// if card upgraded, play sound upgrade
        /// </summary>
        private void OnCardUpgraded()
        {
            ChangeAudioClip(SoundType.ExplosionSound);
        }
        /// <summary>
        /// Change audio clip, and play
        /// </summary>
        /// <param name="type"></param>
        private void ChangeAudioClip(SoundType type)
        {
            AudioClip clip = _soundsDatas.First(x => x.SoundType == type).AudioClip;
            _soundsSource.clip = clip;
            _soundsSource.Play();
        }
        //TakeCoins
        

       
    }
}