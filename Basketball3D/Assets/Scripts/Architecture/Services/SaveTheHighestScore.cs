using System;
using System.IO;
using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Architecture.Services
{
    public class SaveTheHighestScore : ISaveTheHighestScore
    {
        private readonly IScoreService _scoreService;

        private string _savePath = "Data/Score.json";
        private string _fileName = "TheHighestScore.json";

        public int TheHighestScore { get; private set; }

        public SaveTheHighestScore(IScoreService scoreService)
        {
            _scoreService = scoreService;

#if UNITY_ANDROID && !UNITY_EDITOR
            _savePath = Path.Combine(Application.persistentDataPath, _fileName);
#else
            _savePath = Path.Combine(Application.dataPath, _fileName);
#endif
        }

        public void Save()
        {
            if (_scoreService.Score < TheHighestScore)
                return;

            TheHighestScore highestScore = new TheHighestScore()
            {
                Score = _scoreService.Score
            };

            try
            {
                string json = JsonUtility.ToJson(highestScore, true);
                File.WriteAllText(_savePath, json);
            }
            catch (Exception)
            {
                Debug.Log("Load interrupted");
            }
        }

        public void Load()
        {
            if (!File.Exists(_savePath))
                return;

            try
            {
                string json = File.ReadAllText(_savePath);

                TheHighestScore highestScore = JsonUtility.FromJson<TheHighestScore>(json);
                TheHighestScore = highestScore.Score;
            }
            catch (Exception)
            {
                Debug.Log("Load interrupted");
            }
        }
    }
}