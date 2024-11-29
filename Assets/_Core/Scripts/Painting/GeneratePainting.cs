using UnityEngine;
using FABRE.Camera;
using FABRE.Time;

namespace FABRE.Painting
{
    public class GeneratePainting : MonoBehaviour
    {
        [SerializeField] private PaintingList _paintingList;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private static PaintingList _staticPaintingList;
        private static SpriteRenderer _staticSpriteRenderer;
        private static PaintingItem _currentPainting;

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

        public static void Generate()
        {
            Timer.Starting();
            
            if (_staticPaintingList != null)
            {
                int randomIndex = Random.Range(0, _staticPaintingList.paintingItemList.Count);
                _currentPainting = _staticPaintingList.paintingItemList[randomIndex];
                _staticSpriteRenderer.sprite = _currentPainting.PaintingSprite;
                CameraMovement.SetRandomPositionInSprite(_staticSpriteRenderer);
            }
        }

        public static PaintingItem GetCurrentPainting()
        {
            return _currentPainting;
        }
    }
}