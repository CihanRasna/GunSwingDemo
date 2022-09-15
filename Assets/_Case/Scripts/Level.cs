using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Case.Scripts
{
    public class Level : BaseLevel
    {
        [SerializeField] private List<RopeHandler> roperHandlers;
        [SerializeField] private EnemySpawner enemySpawner;
        [HideInInspector] public List<Enemy> currentEnemies;

        private int _totalEnemyCount = 0;
        private int _killedEnemyCount = 0;

        public Transform pool;

        private void Awake()
        {
            player.ropeHandlers = roperHandlers;
            _totalEnemyCount = enemySpawner.SpawnCount;
        }

        protected override void LevelDidLoad()
        {
            base.LevelDidLoad();
        }

        protected override void LevelDidStart()
        {
            base.LevelDidStart();
            player.PlayerStarted();
            player.canMove = true;
            enemySpawner.StartSpawning();
        }

        private void Update()
        {
            if (state != State.Started) return;
            for (var i = 0; i < currentEnemies.Count; i++)
            {
                var enemy = currentEnemies[i];
                var enemyZ = enemy.transform.position.z;
                var playerZ = player.transform.position.z;
                var dist = enemyZ - playerZ;
                if (!(dist <= 3f)) continue;
                Fail();
                break;
            }
        }

        protected override void Fail()
        {
            base.Fail();
            foreach (var enemy in currentEnemies)
            {
                enemy.canMove = false;
            }
            Debug.Log("AAA");
            enemySpawner.StopSpawner();
            player.canMove = false;
        }

        protected override void Success(float score)
        {
            base.Success(score);
            player.canMove = false;
        }

        public void OnEnemyDie(Enemy enemy)
        {
            enemy.enemyDie -= OnEnemyDie;
            currentEnemies.Remove(enemy);
            _killedEnemyCount += 1;
            if (_killedEnemyCount >= _totalEnemyCount)
            {
                Success(1);
            }
        }
    }
}