using System;
using System.Collections.Generic;
using _Case.Scripts.Guns;
using Obi;
using UnityEngine;

namespace _Case.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerModel modelPrefab;
        [SerializeField] private Animator animator;
        [SerializeField] private int health;
        [SerializeField] private float playerSpeed;
        [SerializeField] private List<Transform> gunPoints;
        [SerializeField] private int activeGunCount = 0;
        [HideInInspector] public bool canMove;

        public float PlayerSpeed => playerSpeed;
        public List<RopeHandler> ropeHandlers;
        private static readonly int GameStarted = Animator.StringToHash("GameStarted");

        private void Start()
        {
            var playerModel = Instantiate(modelPrefab, transform);
            var valueTuple = playerModel.SendValuesToPlayer();
            animator = valueTuple.animator;
            health = valueTuple.health;
            playerSpeed = valueTuple.backwardSpeed;
            gunPoints = valueTuple.gunPoints;
        }

        private void ActivateNewGun(Weapon weapon)
        {
            if (activeGunCount < ropeHandlers.Count)
            {
                var desiredGunCount = activeGunCount + 1;
                for (var i = activeGunCount; i < desiredGunCount; i++)
                {
                    var handler = ropeHandlers[i];
                    handler.gameObject.SetActive(true);
                    handler.PlayerGotNewWeapon(weapon);
                }

                activeGunCount = desiredGunCount;
            }
            else
            {
                for (var i = 0; i < ropeHandlers.Count; i++)
                {
                    var handler = ropeHandlers[i];
                    if (handler.CurrentWeapon.WeaponLevel < weapon.WeaponLevel)
                    {
                        handler.PlayerGotNewWeapon(weapon);
                        return;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<WeaponReward>(out var weaponReward))
            {
                ActivateNewGun(weaponReward.RewardPrefab);
                weaponReward.gameObject.SetActive(false);
            }
        }

        public void PlayerStarted()
        {
            var layer = animator.GetLayerIndex("UpperBody");
            animator.SetTrigger(GameStarted);
            animator.SetLayerWeight(layer,1f);
        }

        private void Update()
        {
            if (canMove)
            {
                var myTransform = transform;
                var pos = myTransform.position;
                pos.z -= playerSpeed * Time.deltaTime;
                myTransform.position = pos;
            }
        }

        private void LateUpdate()
        {
            for (var i = 0; i < ropeHandlers.Count; i++)
            {
                var rope = ropeHandlers[i];
                var gunpoint = gunPoints[i];
                rope.transform.position = gunpoint.TransformPoint(gunpoint.localPosition);
            }
        }
    }
}