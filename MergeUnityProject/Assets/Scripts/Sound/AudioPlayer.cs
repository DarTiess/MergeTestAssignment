using UnityEngine;

namespace DefaultNamespace
{
    public class AudioPlayer
    {
        private AudioSource _musicSource;
        private AudioSource _soundsSource;
        private AudioClip _musicInGame;

        public AudioPlayer(AudioSource musicSource, AudioSource soundsSource, AudioClip musicInGame)
        {
            _musicSource = musicSource;
            _soundsSource = soundsSource;
            _musicInGame = musicInGame;

            InitMusicInGame();
        }

        private void InitMusicInGame()
        {
            _musicSource.clip = _musicInGame;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }
}