using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FABRE.Painting
{
    public class PaintingDescription : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _description;
        
        public void Display(PaintingItem paintingItem)
        {
            _name.text = paintingItem.name;
            _image.sprite = paintingItem.PaintingSprite;
            _description.text = paintingItem.PaintingDescription;
        }

        public void GenerateNewPainting()
        {
            GeneratePainting.Generate();
        }
    }
}