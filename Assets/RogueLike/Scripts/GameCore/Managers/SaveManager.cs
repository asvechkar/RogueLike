using System;
using System.Collections.Generic;
using Reflex.Attributes;
using RogueLike.Scripts.GameCore.SaveSystem;
using UnityEngine;
using File = System.IO.File;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class SaveManager
    {
        [Inject] private PlayerManager playerManager;
        [Inject] private ExperienceManager experienceManager;
        [Inject] private SkillManager skillManager;
        [Inject] private GameManager gameManager;
        [Inject] private CoinManager coinManager;
        [Inject] private ScoreManager scoreManager;
        [Inject] private WeaponManager weaponManager;
        
        private GameData _gameData;
        
        public static string SaveFilePath => Application.persistentDataPath + "/save_file.json";

        public void SaveGame()
        {
            try
            {
                FillSaveData();
                File.WriteAllText(SaveFilePath, JsonUtility.ToJson(_gameData, true));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void FillSaveData()
        {
            _gameData.playerData.level = experienceManager.CurrentLevel;
            _gameData.playerData.maxHealth = playerManager.MaxHealth;
            _gameData.playerData.speed = playerManager.Speed;
            _gameData.playerData.healPoints = playerManager.HealPoints;
            
            _gameData.skillPoints = skillManager.SkillPoints;
            _gameData.difficulty = gameManager.Difficulty;
            _gameData.coins = coinManager.Coins;
            _gameData.score = scoreManager.Score;

            var weaponList = new List<SaveSystem.Weapon>();
            foreach (var weapon in weaponManager.Weapons)
            {
                var item = new SaveSystem.Weapon
                {
                    Type  = weapon.WeaponType,
                    Level = weapon.CurrentLevel
                };
                weaponList.Add(item);
            }

            if (weaponList.Count > 0)
            {
                var weaponData = new WeaponData
                {
                    Weapons = weaponList.ToArray()
                };
                _gameData.WeaponData = weaponData;
            }
        }
    }
}