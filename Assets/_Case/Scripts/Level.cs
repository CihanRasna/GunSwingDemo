using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Case.Scripts
{
    public class Level : BaseLevel
    {

        [SerializeField] private List<RopeHandler> roperHandlers;
        public Transform pool;

        private void Awake()
        {
            player.ropeHandlers = roperHandlers;
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
        }
    }
}
