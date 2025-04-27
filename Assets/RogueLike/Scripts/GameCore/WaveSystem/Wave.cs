using System;
using System.Collections.Generic;
using RogueLike.Scripts.Enemy;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.WaveSystem
{
    [Serializable]
    public class Wave: IActivate
    {
        [SerializeField] private List<EnemySpawner> enemySpawners = new();
        
        public void Activate()
        {
            for (var i = 0; i < enemySpawners.Count; i++)
            {
                enemySpawners[i].Activate();
            }
        }

        public void Deactivate()
        {
            for (var i = 0; i < enemySpawners.Count; i++)
            {
                enemySpawners[i].Deactivate();
            }
        }
    }
}