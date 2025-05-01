using System;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.Events.InputEvents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class InputManager : MonoBehaviour
    {
        public void OnMove(InputAction.CallbackContext context)
        {
            EventBus.Invoke(new OnMoved(context.ReadValue<Vector2>()));
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            EventBus.Invoke(new OnAttacked());
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