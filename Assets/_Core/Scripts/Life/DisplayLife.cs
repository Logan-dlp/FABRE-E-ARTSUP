using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FABRE.Life
{
    public class DisplayLife : MonoBehaviour
    {
        private static GameObject _staticGameOverParent;
        private static Sprite _staticHeartBorkenSprite;
        private static List<GameObject> _heartList = new();
        
        public static void RefreshHeart()
        {
            for (int i = 0; i < _heartList.Count; i++)
            {
                if (LifeController.Life <= i)
                {
                    Image imageComponent = _heartList[i].GetComponent<Image>();
                    imageComponent.sprite = _staticHeartBorkenSprite;
                    imageComponent.color = Color.gray;
                }
            }
        }

        public static void DisplayGameOver()
        {
            _staticGameOverParent.SetActive(true);
        }

        [SerializeField] private GameObject _HeartParent;
        [SerializeField] private GameObject _gameOverParent;
        [SerializeField] private Vector2 _offset = new(100, 100);
        [SerializeField] private Sprite _heartSprite;
        [SerializeField] private Sprite _heartBorkenSprite;
        
        private void Awake()
        {
            _staticHeartBorkenSprite = _heartBorkenSprite;
            _staticGameOverParent = _gameOverParent;
        }

        private void Start()
        {
            ResetHeart();
        }

        private void ResetHeart()
        {
            _heartList.Clear();
            
            foreach (Transform child in _HeartParent.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < LifeController.Life; i++)
            {
                GameObject heartInstance = new GameObject(string.Format("Heart_{0:D3}", i));
                heartInstance.transform.parent = _HeartParent.transform;

                heartInstance.transform.position = new Vector3(_offset.x * (i + 1), _offset.y, 0);
                
                Image imageComponent = heartInstance.AddComponent<Image>();
                imageComponent.sprite = _heartSprite;
                imageComponent.color = Color.red;
                
                _heartList.Add(heartInstance);
            }
        }
    }
}