using FABRE.Painting;
using UnityEngine;
using UnityEngine.Events;

namespace FABRE.Events.Listener
{
    public class PaintingEventListener : MonoBehaviour
    {
        [SerializeField] private PaintingEvent _paintingEvent;
        [SerializeField] private UnityEvent<PaintingItem> _callbacks;

        private void OnEnable()
        {
            _paintingEvent.PaintingAction += InvokeEvent;
        }

        private void OnDisable()
        {
            _paintingEvent.PaintingAction -= InvokeEvent;
        }

        private void InvokeEvent(PaintingItem paintingItem)
        {
            _callbacks?.Invoke(paintingItem);
        }
    }
}