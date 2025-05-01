using System;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using RogueLike.Scripts.Events.Player;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class CoinManager : MonoBehaviour
    {
        public int Coins { get; private set; }

        private void Start()
        {
            Coins = 0;
        }

        private void AddCoins(OnPickupCoins evt)
        {
            Coins += evt.Coins;
            EventBus.Invoke(new OnCoinsChanged(Coins));
        }
        
        public void SpendCoins(int amount)
        {
            Coins -= amount;
            EventBus.Invoke(new OnCoinsChanged(Coins));
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