using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    /// <summary>
    /// Handles applications' musics and sound effects.
    /// TODO: Use dependency injection to avoid singletons;
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AudioManager>();
                }

                return _instance;
            }
        }

        [SerializeField]
        private AudioSource _soundsEffectsSource;

        private static AudioManager _instance;

        private void Awake() => DontDestroyOnLoad(this);

        public void Play(AudioClip soundEffect, bool priority = false)
        {
            if (priority)
            {
                _soundsEffectsSource.Stop();
            }

            _soundsEffectsSource.clip = soundEffect;
            _soundsEffectsSource.Play();
        }
    }
}
