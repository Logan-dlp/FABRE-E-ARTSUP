using System.Collections.Generic;
using FABRE.Score;
using TMPro;
using UnityEngine;

namespace FABRE.UI
{
    public class DisplayScores : MonoBehaviour
    {
        [SerializeField] private GameObject _scoreTemplate;
        [SerializeField] private Color _colorA;
        [SerializeField] private Color _colorB;
        
        public void RefreshScore()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            List<ScoreDTO> scores = SaveScore.LoadAllDecreasing();
            if (scores == null) return;

            RectTransform content = transform as RectTransform;
            content.sizeDelta = new Vector2(content.sizeDelta.x, scores.Count * 45);

            for (int i = 0; i < scores.Count; i++)
            {
                GameObject scoreTemplate = Instantiate(_scoreTemplate, transform);
                TextMeshProUGUI scoreText = scoreTemplate.GetComponent<TextMeshProUGUI>();
                scoreText.text = $"{i+1}. {scores[i].Name} : {scores[i].Score}";
                if (i % 2 == 0)
                {
                    scoreText.color = _colorA;
                }
                else
                {
                    scoreText.color = _colorB;
                }
            }
        }
    }
}