using _Case.Scripts.Managers;
using UnityEngine;

namespace _Case.Scripts.Guns
{
    public abstract class Bullet : MonoBehaviour
    {
        public Weapon poolParent;
        [HideInInspector] public float bulletSpeed;
        [SerializeField] protected Vector3 desiredWorldScale = Vector3.one;


        public virtual void SpawnBullet()
        {
            var pool = (LevelManager.Instance.currentLevel as Level)?.pool;
            gameObject.SetActive(true);
            transform.SetParent(pool, true);
            poolParent.SimpleBulletPool.Dequeue();
        }

        public virtual void ReturnToPool()
        {
            if (!poolParent)
            {
                gameObject.SetActive(false);
                return;
            }

            poolParent.SimpleBulletPool.Enqueue(this);
            Transform myTransform;
            (myTransform = transform).SetParent(poolParent.ShootPoint, true);
            myTransform.position = poolParent.ShootPoint.transform.position;
            var parentScale = poolParent.transform.lossyScale;
            var scale = desiredWorldScale;
            myTransform.localScale =
                new Vector3(scale.x / parentScale.x, scale.y / parentScale.y, scale.z / parentScale.z);
            gameObject.SetActive(false);
        }

        protected virtual void Update()
        {
            transform.position += (Time.deltaTime * bulletSpeed) * transform.forward;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(poolParent.Damage);
                ReturnToPool();
            }

            if (other.TryGetComponent(out PoolWall poolWall))
            {
                ReturnToPool();
            }
        }
    }
}