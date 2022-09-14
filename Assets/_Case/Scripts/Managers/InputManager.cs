﻿using System;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

namespace _Case.Scripts.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] private LeanTouch leanTouch;
        private bool _isFirstTouch;

        protected override void Awake()
        {
            base.Awake();
            if (!leanTouch)
            {
                leanTouch = gameObject.AddComponent<LeanTouch>();
            }
            
            LeanTouch.OnFingerDown += OnPointerDown;
            LeanTouch.OnFingerUp += OnPointerUp;
        }

        private void OnPointerDown(LeanFinger finger)
        {
            if (!_isFirstTouch)
            {
                _isFirstTouch = true;
                var currentLevel = LevelManager.Instance.currentLevel as Level;
                currentLevel.StartOnFirstTouch();
            }
        }

        private void OnPointerUp(LeanFinger finger)
        {
        }
    }
}