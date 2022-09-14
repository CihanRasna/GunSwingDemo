using System.Collections.Generic;
using UnityEngine;

namespace _Case.Scripts
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private int health;
        [SerializeField] private float backwardSpeed;
        [SerializeField] private List<Transform> gunPoints;
    }
}
