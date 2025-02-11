using System;
using UnityEngine;

namespace FABRE.Sounds
{
    public class SoundEffect : MonoBehaviour
    {
        private static AudioClip _interactionSoundStatic;
        private static AudioClip _wrongSoundStatic;
        private static AudioClip _timerStressSoundStatic;
        private static AudioClip _gameOverSoundStatic;
        
        private static AudioSource _audioSource;

        public static void PlayInteractionSound()
        {
            if (_audioSource != null)
            {
                _audioSource.PlayOneShot(_interactionSoundStatic);
            }
        }
        
        public static void PlayWrongSound()
        {
            if (_audioSource != null)
            {
                _audioSource.PlayOneShot(_wrongSoundStatic);
            }
        }
        
        public static void PlayTimerStressSound()
        {
            if (_audioSource != null)
            {
                _audioSource.PlayOneShot(_timerStressSoundStatic);
            }
        }
        
        public static void PlayGameOverSound()
        {
            if (_audioSource != null)
            {
                _audioSource.PlayOneShot(_gameOverSoundStatic);
            }
        }
        
        [SerializeField] private AudioClip _interactionSound;
        [SerializeField] private AudioClip _wrongSound;
        [SerializeField] private AudioClip _timerStressSound;
        [SerializeField] private AudioClip _gameOverSound;

        private void OnEnable()
        {
            _audioSource = FindAnyObjectByType<AudioSource>();
            
            _interactionSoundStatic = _interactionSound;
            _wrongSoundStatic = _wrongSound;
            _timerStressSoundStatic = _timerStressSound;
            _gameOverSoundStatic = _gameOverSound;
        }
    }
}