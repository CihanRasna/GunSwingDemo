using System;
using _Case.Scripts.Guns;
using _Case.Scripts.Managers;
using Obi;
using UnityEngine;

namespace _Case.Scripts
{
    public class RopeHandler : MonoBehaviour
    {
        [SerializeField] private ObiRope rope;
        [SerializeField] private Transform gunPlacement;
        [SerializeField] private Weapon currentWeapon;
        public Weapon CurrentWeapon => currentWeapon;

        public void PlayerGotNewWeapon(Weapon weapon)
        {
            if (currentWeapon && weapon.WeaponLevel > currentWeapon.WeaponLevel)
            {
                Destroy(currentWeapon.gameObject);
                currentWeapon = null;
            }

            if (!currentWeapon)
            {
                currentWeapon = Instantiate(weapon, gunPlacement.position,Quaternion.identity,gunPlacement);
            }
        }
    }
}