using UnityEngine;

namespace DefaultNamespace
{
    public class AudioPlayer
    {
        private AudioSource _musicSource;
        private AudioClip _musicInGame;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="musicSource"></param>
        /// <param name="musicInGame"></param>
        public AudioPlayer(AudioSource musicSource, AudioClip musicInGame)
        {
            _musicSource = musicSource;
            _musicInGame = musicInGame;

            InitMusicInGame();
        }
        /// <summary>
        /// Initialize music in game, and play
        /// </summary>
        private void InitMusicInGame()
        {
            _musicSource.clip = _musicInGame;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }
}