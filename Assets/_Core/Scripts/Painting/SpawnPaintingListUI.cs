using UnityEngine;
using UnityEngine.UI;

namespace FABRE.Painting.UI
{
    public class SpawnPaintingListUI : MonoBehaviour
    {
        [SerializeField] PaintingList _paintingList;
        [SerializeField] private GameObject _paintingTemplate;
        
        [SerializeField] private Vector2 _cellSize;
        [SerializeField] private Vector2 _spacing;
        
        private RectTransform _rectTransform;

        private void Awake()
        {
            if (TryGetComponent<GridLayoutGroup>(out GridLayoutGroup component))
            {
                _cellSize = component.cellSize;
                _spacing = component.spacing;
            }
            
            _rectTransform = GetComponent<RectTransform>();
        }
        
        public void RefreshUI()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            foreach (PaintingItem paintingItem in _paintingList.paintingItemList)
            {
                if (_paintingTemplate.TryGetComponent<Image>(out Image image) && _paintingTemplate.TryGetComponent<Button>(out Button button))
                {

                    void OnClickPainting()
                    {
                        if (paintingItem == GeneratePainting.GetCurrentPainting())
                        {
                            Timer.Stoping();
                            GeneratePainting.Generate();
                        }
                        else
                        {
                            Debug.Log("Ta faux !");
                        }
                    }
                    
                    GameObject instance = Instantiate(_paintingTemplate, transform);
                    instance.GetComponent<Image>().sprite = paintingItem.PaintingSprite;
                    instance.GetComponent<Button>().onClick.AddListener(OnClickPainting);
                }
            }

            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x,
                _paintingList.paintingItemList.Count * _cellSize.y 
                + _paintingList.paintingItemList.Count * _spacing.y + _spacing.y);
            
        }
    }
}