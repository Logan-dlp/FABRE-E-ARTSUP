using FABRE.Time;
using FABRE.Score;
using UnityEngine;

namespace FABRE.Life
{
    public class LifeController
    {
        private const int _basicLife = 5;
        
        private static int _life = _basicLife;
        public static int Life => _life;

        public static void ResetLife()
        {
            _life = _basicLife;
        }

        public static void RemoveLife(int amount)
        {
            _life -= amount;
            DisplayLife.RefreshHeart();
            
            if (_life <= 0) GameOver();
        }

        public static void GameOver()
        {
            Timer.Stoping();
            DisplayLife.DisplayGameOver();
            SaveScore.Save(SavePlayer.Pseudo, SavePlayer.Score);
        }
    }
}