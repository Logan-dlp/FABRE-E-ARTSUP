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
                Pseudo = $"Player {SaveScore.LoadAll().Count}";
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