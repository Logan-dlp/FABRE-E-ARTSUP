using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FABRE.Score
{
    public class SavePlayer : MonoBehaviour
    {
        public static string Pseudo { get; private set; }
        public static int Score { get; private set; }

        public static void Save(TMP_InputField text)
        {
            if (!string.IsNullOrEmpty(text.text))
            {
                Pseudo = text.text;
            }
            else
            {
                List<ScoreDTO> scores = SaveScore.LoadAll();
                int placement = scores != null || scores.Count != 0 ? scores.Count : 0;
                
                Pseudo = $"Player {placement}";
            }
        }

        public static void AddScore(int score)
        {
            Score += score;
        }

        public static void ResetScore()
        {
            Score = 0;
        }
    }
}