using System;
using System.Collections.Generic;
using Obi;
using UnityEngine;

namespace _Case.Scripts
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private int health;
        [SerializeField] private float backwardSpeed;
        [SerializeField] private List<Transform> armPoints;

        public (Animator animator, int health, float backwardSpeed, List<Transform> gunPoints) SendValuesToPlayer()
        {
            return (animator, health, backwardSpeed, armPoints);
        }
    }
}
