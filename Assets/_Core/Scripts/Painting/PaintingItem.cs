using System.Collections.Generic;
using UnityEngine;

namespace FABRE.Painting
{
    [CreateAssetMenu(fileName = "new_" + nameof(PaintingItem), menuName = "Painting/Item")]
    public class PaintingItem : ScriptableObject
    {
        [SerializeField] private string _paintingName;
        public string PaintingName
        {
            get => _paintingName;
            set => _paintingName = value;
        }
        
        [SerializeField] private Sprite _paintingSprite;
        public Sprite PaintingSprite
        {
            get => _paintingSprite;
            set => _paintingSprite = value;
        }
        
        [SerializeField] private string _paintingDescription;
        public string PaintingDescription
        {
            get => _paintingDescription;
            set => _paintingDescription = value;
        }

        [SerializeField] private List<Vector2> _paintingKeyPointsList;
        public List<Vector2> PaintingKeyPointsList
        {
            get => _paintingKeyPointsList;
            set => _paintingKeyPointsList = value;
        }
    }
}