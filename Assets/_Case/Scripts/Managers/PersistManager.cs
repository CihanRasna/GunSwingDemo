using UnityEngine;

namespace _Case.Scripts.Managers
{
    public class PersistManager : Singleton<PersistManager>
    {
        private int _playedLevelCount;
        private static string playerLevelCountKey = "PlayerLevelCount";
        public int playedLevelCount
        {
            get => _playedLevelCount;
            set
            {
                _playedLevelCount = value;
                PlayerPrefs.SetInt(playerLevelCountKey, _playedLevelCount);
                PlayerPrefs.Save();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            playedLevelCount = PlayerPrefs.GetInt(playerLevelCountKey, 0);
        }
    }
}