using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class SoundsController : MonoBehaviour
    {
        public static SoundsController Instance = null;

        [Header("Master")]
        [SerializeField]
        private AudioMixerGroup _masterGroup;

        [Header("Musics")]
        [SerializeField]
        private AudioSource _musicSource;

        [SerializeField]
        private AudioMixerGroup _musicGroup;

        [Header("Sounds")]
        [SerializeField]
        private AudioSource _soundsSource;

        [SerializeField]
        private AudioMixerGroup _soundsGroup;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void ChangeVolume()
        {
            // TODO: Use master AudioMixer group to control volume via slider/button;
        }

        public void Play(AudioClip soundEffect)
        {
            _soundsSource.PlayOneShot(soundEffect);
        }
    }
}
