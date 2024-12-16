using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FABRE.Life
{
    public class DisplayLife : MonoBehaviour
    {
        [SerializeField] private Vector2 _offset = new(100, 100);
        [SerializeField] private Sprite _heartSprite;
        [SerializeField] private Sprite _heartBorkenSprite;

        [SerializeField] private static Sprite _staticHeartBorkenSprite;
        private static List<GameObject> _heartList = new();

        private void Awake()
        {
            _staticHeartBorkenSprite = _heartBorkenSprite;
        }

        private void Start()
        {
            ResetDisplay();
        }

        private void ResetDisplay()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < LifeController.Life; i++)
            {
                GameObject heartInstance = new GameObject($"Heart_{i}");
                heartInstance.transform.parent = transform;

                heartInstance.transform.position = new Vector3(_offset.x * (i + 1), _offset.y, 0);
                
                Image imageComponent = heartInstance.AddComponent<Image>();
                imageComponent.sprite = _heartSprite;
                imageComponent.color = Color.red;
                
                _heartList.Add(heartInstance);
            }
        }

        public static void RefreshDisplay()
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
    }
}