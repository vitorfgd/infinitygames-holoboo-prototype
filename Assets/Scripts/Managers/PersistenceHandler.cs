using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Handles applications' save/load methods.
    /// Uses PlayerPrefs as the main persistence class.
    /// TODO: Use dependency injection to avoid singletons;
    /// </summary>
    public class PersistenceHandler : MonoBehaviour
    {
        public static PersistenceHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PersistenceHandler>();
                }

                return _instance;
            }
        }

        public string CurrentStagePersistenceKey => _currentStagePersistenceKey;
        public string UnlockedStagesPersistenceKey => _unlockedStagesPersistenceKey;

        /// <summary>
        /// Current map persistence key. Needed for PlayerPrefs get/set;
        /// </summary>
        /// <returns></returns>
        [SerializeField]
        private string _currentStagePersistenceKey;

        /// <summary>
        /// Current map persistence key. Needed for PlayerPrefs get/set;
        /// </summary>
        /// <returns></returns>
        [SerializeField]
        private string _unlockedStagesPersistenceKey;

        private static PersistenceHandler _instance;

        private void Awake() => DontDestroyOnLoad(this);

        /// <summary>
        /// Receives "int" because that's all its needed for this prototype.
        /// TODO: Use Generic to return anything.
        /// </summary>
        public void Save(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        /// <summary>
        /// Returns "int" because that's all its needed for this prototype.
        /// TODO: Use Generic to return anything.
        /// </summary>
        public int Load(string key)
        {
            return PlayerPrefs.GetInt(key, 0);
        }
    }
}
