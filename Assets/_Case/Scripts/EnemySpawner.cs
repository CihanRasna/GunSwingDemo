using System;
using System.Collections;
using System.Collections.Generic;
using _Case.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Case.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private int spawnCount;
        [SerializeField] private Vector2 minMaxSpawnOffset;
        [SerializeField] private float spawnDelay;
        [SerializeField] private bool changeEnemySpeed;
        [SerializeField] private float changedEnemySpeed = 10f;

        private IEnumerator spawnerRoutine;
        public int SpawnCount => spawnCount;

        private WaitForSeconds waitForSeconds;
        public void StartSpawning()
        {
            waitForSeconds = new WaitForSeconds(spawnDelay);
            StartCoroutine(spawnerRoutine = Spawner());
        }

        public void StopSpawner()
        {
            StopCoroutine(spawnerRoutine);
            spawnerRoutine = null;
        }
        private IEnumerator Spawner()
        {
            var level = LevelManager.Instance.currentLevel as Level;
            for (var i = 0; i < spawnCount; i++)
            {
                var posX = Random.Range(minMaxSpawnOffset.x, minMaxSpawnOffset.y);
                var myTransform = transform;
                var newPos = myTransform.position + (posX * Vector3.right);
                var enemy = Instantiate(enemyPrefab, newPos, myTransform.rotation,level.transform);
                if (changeEnemySpeed)
                    enemy.enemySpeed = changedEnemySpeed;
                level.currentEnemies.Add(enemy);
                enemy.enemyDie += level.OnEnemyDie;
                yield return waitForSeconds;
            }
        }
    }
}
