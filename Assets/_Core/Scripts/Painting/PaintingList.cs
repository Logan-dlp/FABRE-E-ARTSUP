using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FABRE.Painting
{
    [CreateAssetMenu(fileName = "new_" + nameof(PaintingList), menuName = "Painting/List")]
    public class PaintingList : ScriptableObject
    {
        [SerializeField] private List<PaintingItem> _paintingItemList;
        public List<PaintingItem> paintingItemList => _paintingItemList;

        public void AddPaintingItem(PaintingItem paintingItem)
        {
            _paintingItemList.Add(paintingItem);
            SaveChanges();
        }

        public void RemovePaintingItem(PaintingItem paintingItem)
        {
            _paintingItemList.Remove(paintingItem);
            SaveChanges();
        }

        private void SaveChanges()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}