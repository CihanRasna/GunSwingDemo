using System;
using _Case.Scripts.Managers;
using UnityEngine;

namespace _Case.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> enemyDie;
        [SerializeField] private int health = 100;
        public float enemySpeed;

        [SerializeField] private Animator animator;
        private Player _player;
        [HideInInspector] public bool canMove = true;

        private void Start()
        {
            canMove = true;
            _player = LevelManager.Instance.currentLevel.player;
            if (enemySpeed <= _player.PlayerSpeed)
            {
                enemySpeed *= 1.5f;
                Debug.LogWarning($"{gameObject.name} has same speed with {_player.gameObject.name} Fixed for this session", _player);
            }
        }

        private void Update()
        {
            if (!canMove) return;
            var myTransform = transform;
            var position = myTransform.position;
            position += (Time.deltaTime * enemySpeed) * myTransform.forward;
            myTransform.position = position;
        }

        public void TakeDamage(int damage)
        {
            if (health <= 0) return;

            health -= damage;

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            enemyDie?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}