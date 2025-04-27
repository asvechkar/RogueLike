using System;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using RogueLike.Scripts.Events.Player;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class CoinManager : MonoBehaviour
    {
        private int _coins;

        private void Start()
        {
            _coins = 0;
        }

        private void AddCoins(OnPickupCoins evt)
        {
            var oldAmount = _coins;
            _coins += evt.Coins;
            EventBus.Invoke(new OnCoinsChanged(oldAmount, _coins));
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnPickupCoins>(AddCoins);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPickupCoins>(AddCoins);
        }
    }
}