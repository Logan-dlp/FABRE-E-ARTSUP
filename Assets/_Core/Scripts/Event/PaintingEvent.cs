using System;
using FABRE.Painting;
using UnityEngine;

namespace FABRE.Events
{
    [CreateAssetMenu(fileName = "new_" + nameof(PaintingEvent), menuName = "Events/Painting")]
    public class PaintingEvent : ScriptableObject
    {
        public Action<PaintingItem> PaintingAction;

        public void InvokeEvent(PaintingItem paintingItem)
        {
            PaintingAction?.Invoke(paintingItem);
        }
    }
}