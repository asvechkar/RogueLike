using System;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.Events.InputEvents;
using RogueLike.Scripts.Events.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class InputManager : MonoBehaviour
    {
        public void OnMove(InputAction.CallbackContext context)
        {
            EventBus.Invoke(new OnMoved(context.ReadValue<Vector2>()));
        }

        public void OnWeaponClicked(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var control = (KeyControl)context.control;
                var button = control.keyCode switch
                {
                    Key.Digit1 => 1,
                    Key.Digit2 => 2,
                    Key.Digit3 => 3,
                    Key.Digit4 => 4,
                    Key.Digit5 => 5,
                    Key.Digit6 => 6,
                    _ => throw new ArgumentOutOfRangeException()
                };
            
                EventBus.Invoke(new OnWeaponActivate(button));
            }
        }

        private void Start()
        {
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        }

        private void PauseGame(OnGamePaused evt)
        {
            GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        }
        
        private void ResumeGame(OnGameResumed evt)
        {
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnGamePaused>(PauseGame);
            EventBus.Subscribe<OnGameResumed>(ResumeGame);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnGamePaused>(PauseGame);
            EventBus.Unsubscribe<OnGameResumed>(ResumeGame);
        }
    }
}