using DG.Tweening;
using UnityEngine;

namespace _Case.Scripts.Guns
{
    public class Laser : Weapon
    {
        [SerializeField] private Transform particleEffect;

        protected override void Start()
        {
            base.Start();
            particleEffect.DOScale(1f, firerate).OnComplete(() => { particleEffect.DOScale(0f, 0f); }).SetLoops(-1);
        }
    }
}