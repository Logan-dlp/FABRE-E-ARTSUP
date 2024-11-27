using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_" + nameof(PaintingList), menuName = "Painting/List")]
public class PaintingList : ScriptableObject
{
    [SerializeField] private List<PaintingItem> _paintingItemList;
    public List<PaintingItem> paintingItemList => _paintingItemList;

    public void AddPaintingItem(PaintingItem paintingItem)
    {
        _paintingItemList.Add(paintingItem);
    }

    public void RemovePaintingItem(PaintingItem paintingItem)
    {
        _paintingItemList.Remove(paintingItem);
    }
}
