using UnityEngine;
using FABRE.Camera;
using FABRE.Time;

namespace FABRE.Painting
{
    public class GeneratePainting : MonoBehaviour
    {
        private static PaintingList _staticPaintingList;
        private static SpriteRenderer _staticSpriteRenderer;
        private static PaintingItem _currentPainting;
        
        public static void Generate()
        {
            if (_staticPaintingList == null)
            {
                Debug.LogError("Painting List as null ref !");
                return;
            }
            
            int randomIndex = Random.Range(0, _staticPaintingList.paintingItemList.Count);
            PaintingItem newPainting = _staticPaintingList.paintingItemList[randomIndex];
            
            if (_currentPainting == null || _currentPainting != newPainting)
            {
                _currentPainting = newPainting;
                _staticSpriteRenderer.sprite = _currentPainting.PaintingSprite;
                CameraMovement.SetRandomAroundPointPositionInSpriteWithList(_staticSpriteRenderer, _currentPainting.PaintingKeyPointsList, .5f);
                    
                Timer.Starting();
            }
            else
            {
                Generate();
            }
        }

        public static PaintingItem GetCurrentPainting()
        {
            return _currentPainting;
        }
        
        [SerializeField] private PaintingList _paintingList;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                _spriteRenderer = spriteRenderer;
            }
            
            _staticPaintingList = _paintingList;
            _staticSpriteRenderer = _spriteRenderer;
        }

        private void Start()
        {
            Timer.Reaload();
            Timer.Starting();
            Generate();
        }
    }
}