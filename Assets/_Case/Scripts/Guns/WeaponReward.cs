using DG.Tweening;
using UnityEngine;

namespace _Case.Scripts.Guns
{
    public class WeaponReward : MonoBehaviour
    {
        [SerializeField] private GameObject dummy;
        
        [SerializeField] private Weapon rewardPrefab;
        public Weapon RewardPrefab => rewardPrefab;

        private void Start()
        {
            dummy.transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}