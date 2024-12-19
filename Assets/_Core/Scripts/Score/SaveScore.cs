using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace FABRE.Score
{
    public class SaveScore
    {
        private static string _filePath = $"{Application.persistentDataPath}/SaveScore.json";

        public static List<ScoreDTO> ScoreList { get; private set; } = new();

        public static void Save(string name, int score)
        {
            ScoreDTO scoreDTO = new(name, score);
            if (File.Exists(_filePath))
            {
                LoadAll();
            }
            
            ScoreList.Add(scoreDTO);
            
            try
            {
                string json = JsonConvert.SerializeObject(ScoreList, Formatting.Indented);
                using FileStream fileStream = new FileStream(_filePath, FileMode.Create);
                using StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Save score failed !\n{ex}");
                throw;
            }
        }

        public static List<ScoreDTO> LoadAll()
        {
            try
            {
                using StreamReader reader = new(_filePath);
                string json = reader.ReadToEnd();
                ScoreList = JsonConvert.DeserializeObject<List<ScoreDTO>>(json);

                if (ScoreList != null)
                {
                    return ScoreList;
                }
                else
                {
                    ScoreList = new List<ScoreDTO>();
                    return ScoreList;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Load Score failed !\n{ex}");
                return null;
                throw;
            }
        }
    }
}