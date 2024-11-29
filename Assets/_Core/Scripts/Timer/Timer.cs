using TMPro;
using UnityEngine;

namespace FABRE.Time
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
    
        private static float _timer;
        private static bool _isActive = false;

        private void Update()
        {
            if (_isActive)
            {
                _timer += UnityEngine.Time.deltaTime;
            }

            if (_timerText != null)
            {
                DisplayInText(_timerText);
            }
        }

        private void DisplayInText(TextMeshProUGUI text)
        {
            if (_timer < 0)
            {
                _timer = 0;
            }
        
            float minutes = Mathf.FloorToInt(_timer / 60);
            float seconds = Mathf.FloorToInt(_timer % 60);
            float miliseconds = _timer % 1 * 1000;
        
            text.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
        }

        public static void Starting()
        {
            _isActive = true;
        }

        public static void Stoping()
        {
            _isActive = false;
        }
    
        public static void Reaload()
        {
            _timer = 0;
        }
    }
}