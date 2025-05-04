using System;
using RogueLike.Scripts.GameCore.SaveSystem;
using UnityEngine;
using File = System.IO.File;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class LoadManager
    {
        private GameData _gameData;
        
        public GameData LoadGame()
        {
            try
            {
                if (!File.Exists(SaveManager.SaveFilePath)) return _gameData;

                var json = File.ReadAllText(SaveManager.SaveFilePath);
                _gameData = JsonUtility.FromJson<GameData>(json);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            
            return _gameData;
        }
    }
}