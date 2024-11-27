using UnityEngine;

[CreateAssetMenu(fileName = "new_" + nameof(PaintingItem), menuName = "Painting/Item")]
public class PaintingItem : ScriptableObject
{
    [SerializeField] private Sprite _paintingSprite;
    public Sprite PaintingSprite
    {
        get => _paintingSprite;
        set => _paintingSprite = value;
    }
}
