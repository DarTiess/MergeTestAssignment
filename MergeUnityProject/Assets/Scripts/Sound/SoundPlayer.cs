using System.Linq;
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

        public SoundPlayer(AudioSource soundsSource, SoundsData[] soundsData, IUIClicked uiClicked, ICardUpgrade cardUpgrade)
        {
            _soundsSource = soundsSource;
            _soundsDatas = soundsData;
            _uiClicked = uiClicked;
            _uiClicked.CardClicked += OnCardClicked;
            _cardUpgrade = cardUpgrade;
            _cardUpgrade.CardUpgrade += OnCardUpgraded;
        }
        
        private void OnCardClicked()
        {
          ChangeAudioClip(SoundType.ClickSound);
        }

        private void OnCardUpgraded()
        {
            ChangeAudioClip(SoundType.ExplosionSound);
        }

        private void ChangeAudioClip(SoundType type)
        {
            AudioClip clip = _soundsDatas.First(x => x.SoundType == type).AudioClip;
            _soundsSource.clip = clip;
            _soundsSource.Play();
        }
        
        
        //TakeCoins
        

       
    }
}