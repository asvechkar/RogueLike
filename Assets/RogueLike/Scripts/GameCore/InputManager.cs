using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.InputEvents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RogueLike.Scripts.GameCore
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
    }
}