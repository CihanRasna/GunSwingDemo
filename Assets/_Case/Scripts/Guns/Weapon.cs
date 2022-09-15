using System.Collections.Generic;
using UnityEngine;

namespace _Case.Scripts.Guns
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected int weaponLevel;
        [SerializeField] protected Bullet bullet;
        [SerializeField] protected int pooledBulletCount;
        [SerializeField] protected float shootPerSec;
        [SerializeField] protected float bulletSpeed = 1f;
        [SerializeField] protected Transform shootPoint;
        [SerializeField] protected int damage;
        
        public Transform ShootPoint => shootPoint;
        public int WeaponLevel => weaponLevel;
        public int Damage => damage;

        private float passedTime = 0f;
        protected float firerate = 0f;

        public Queue<Bullet> SimpleBulletPool;

        protected virtual void Start()
        {
            firerate = 1f / shootPerSec;
            SimpleBulletPool = new Queue<Bullet>(pooledBulletCount);
            for (var i = 0; i < pooledBulletCount; i++)
            {
                var poolObject = Instantiate(bullet, shootPoint.position,Quaternion.identity,shootPoint.transform);
                poolObject.poolParent = this;
                poolObject.bulletSpeed = bulletSpeed;
                poolObject.ReturnToPool();
            }
        }

        protected virtual void Update()
        {
            passedTime += Time.deltaTime;
            if (passedTime >= firerate)
            {
                passedTime = 0f;
                SimpleBulletPool.Peek().SpawnBullet();
            }
        }
    }
}