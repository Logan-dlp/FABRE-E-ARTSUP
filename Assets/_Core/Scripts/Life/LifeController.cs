using FABRE.Time;
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

        private static void GameOver()
        {
            Timer.Stoping();
            DisplayLife.DisplayGameOver();
            // save data...
        }
    }
}