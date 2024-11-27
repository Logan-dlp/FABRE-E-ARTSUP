using UnityEngine;
using FABRE.Camera;

namespace FABRE.Painting
{
    public class GeneratePainting : MonoBehaviour
    {
        [SerializeField] private PaintingList _paintingList;
        [SerializeField] private SpriteRenderer _spriteRenderer;
    
        [ContextMenu("Generate Painting")]
        public void Generate()
        {
            if (_paintingList != null)
            {
                int randomIndex = Random.Range(0, _paintingList.paintingItemList.Count);
                _spriteRenderer.sprite = _paintingList.paintingItemList[randomIndex].PaintingSprite;
                CameraMovement.SetRandomPositionInSprite(_spriteRenderer);
            }
        }
    }
}