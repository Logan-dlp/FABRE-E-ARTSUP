using UnityEngine;

namespace FABRE.Camera
{
    public class CameraMovement
    {
        public static void SetRandomPositionInSprite(SpriteRenderer spriteRenderer, float depthMin = -5f, float depthMax = -1f)
        {
            Vector2 minPosition = spriteRenderer.bounds.min;
            Vector2 maxPosition = spriteRenderer.bounds.max;
            
            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(minPosition.x, maxPosition.x), 
                UnityEngine.Random.Range(minPosition.y, maxPosition.y),
                UnityEngine.Random.Range(depthMin, depthMax)
                );
            
            UnityEngine.Camera.main.transform.position = randomPosition;
        }
    }
}