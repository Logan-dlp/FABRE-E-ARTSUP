using FABRE.Time;
using UnityEngine;

namespace FABRE.Life
{
    public class LifeController
    {
        private static int _life = 5;
        public static int Life => _life;

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