using System;
using UnityEngine;

namespace _Case.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerModel modelPrefab;

        private void Start()
        {
            Instantiate(modelPrefab, transform);
        }
    }
}