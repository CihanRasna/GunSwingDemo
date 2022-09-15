using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _Case.Scripts.Guns
{
    public class LaserBullet : Bullet
    {
        public override void SpawnBullet()
        {
            base.SpawnBullet();
            StartCoroutine(PoolReturnerForLaser());
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(int.MaxValue);
            }
        }


        private IEnumerator PoolReturnerForLaser()
        {
            transform.DOScaleZ(75f, 1f);
            yield return new WaitForSeconds(3f);
            transform.DOScaleZ(1, 0);
            ReturnToPool();
        }
    }
}
