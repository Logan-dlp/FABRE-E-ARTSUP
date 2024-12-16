using UnityEngine;

namespace FABRE.Life
{
    public class LifeController
    {
        private static int _life = 5;
        public static int Life => _life;

        public static void Remove(int amount)
        {
            _life -= amount;
            DisplayLife.RefreshDisplay();
        }
    }
}