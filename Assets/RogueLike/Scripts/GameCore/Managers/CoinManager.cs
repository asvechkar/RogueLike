using System;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using RogueLike.Scripts.Events.Player;
using RogueLike.Scripts.GameCore.SaveSystem;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class CoinManager : MonoBehaviour
    {
        public int Coins { get; private set; }
        
        [Inject] private readonly GameData _gameData;

        private void Start()
        {
            Coins = _gameData.coins;
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