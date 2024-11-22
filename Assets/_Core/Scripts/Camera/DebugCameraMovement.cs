using UnityEngine;

namespace FABRE.Camera.Debug
{
    public class DebugCameraMovement : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [ContextMenu("Test Camera Movement")]
        public void TestCameraMovement()
        {
            CameraMovement.SetRandomPositionInSprite(_spriteRenderer, -3, -.5f);
        }
    }
}