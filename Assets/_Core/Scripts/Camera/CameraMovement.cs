using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FABRE.Camera
{
    public class CameraMovement
    {
        public static void SetRandomPositionInSprite(SpriteRenderer spriteRenderer, float depthMin = -5f, float depthMax = -1f)
        {
            Vector2 minPosition = spriteRenderer.bounds.min;
            Vector2 maxPosition = spriteRenderer.bounds.max;
            
            Vector3 randomPosition = new Vector3(
                Random.Range(minPosition.x, maxPosition.x), 
                Random.Range(minPosition.y, maxPosition.y),
                Random.Range(depthMin, depthMax)
                );
            
            UnityEngine.Camera.main.transform.position = randomPosition;
        }

        public static void SetRandomAroundPointPositionInSpriteWithList(SpriteRenderer spriteRenderer, List<Vector2> positionList, float aroundRange, float depthMin = -5f, float depthMax = -1f)
        {
            UnityEngine.Debug.Log(positionList.Count);
            Vector2 random2DPosition = positionList[Random.Range(0, positionList.Count - 1)];
            
            Vector3 random3DPosition = new Vector3(
                Random.Range(random2DPosition.x - aroundRange, random2DPosition.x + aroundRange),
                Random.Range(random2DPosition.y - aroundRange, random2DPosition.y + aroundRange),
                Random.Range(depthMin, depthMax)
                );
            
            UnityEngine.Camera.main.transform.position = random3DPosition;
        }

        public static void SetRandomPositionInSpriteWithList(SpriteRenderer spriteRenderer, List<Vector2> positionList, float depthMin = -5f, float depthMax = -1f)
        {
            Vector2 random2DPosition = positionList[Random.Range(0, positionList.Count - 1)];
            
            Vector3 random3DPosition = new Vector3(
                random2DPosition.x,
                random2DPosition.y,
                Random.Range(depthMin, depthMax)
            );
            
            UnityEngine.Camera.main.transform.position = random3DPosition;
        }
    }
}