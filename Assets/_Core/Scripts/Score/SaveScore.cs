using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace FABRE.Score
{
    public class SaveScore
    {
        private static string _filePath = $"{Application.persistentDataPath}/SaveScore.json";

        private static List<ScoreDTO> _scoreList = new();

        public static void Save(string name, int score)
        {
            ScoreDTO scoreDTO = new(name, score);
            if (File.Exists(_filePath))
            {
                Debug.LogError(File.Exists(_filePath));
                Debug.Log(_filePath);
                LoadAll();
            }

            if (_scoreList == null || _scoreList.Count == 0)
            {
                _scoreList = new List<ScoreDTO>();
            }
            
            _scoreList.Add(scoreDTO);
            
            string json = JsonConvert.SerializeObject(_scoreList, Formatting.Indented);
            using FileStream fileStream = new FileStream(_filePath, FileMode.Create);
            using StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(json);
        }

        public static List<ScoreDTO> LoadAll()
        {
            try
            {
                using StreamReader reader = new(_filePath);
                string json = reader.ReadToEnd();
                _scoreList = JsonConvert.DeserializeObject<List<ScoreDTO>>(json);

                if (_scoreList != null)
                {
                    return _scoreList;
                }
                else
                {
                    _scoreList = new List<ScoreDTO>();
                    return _scoreList;
                }
            }
            catch (Exception e)
            {
                _scoreList = new List<ScoreDTO>();
                return _scoreList;
            }
        }

        public static List<ScoreDTO> LoadAllDecreasing()
        {
            LoadAll();
            return _scoreList.OrderByDescending(score => score.Score).ToList();
        }
    }
}