using System.Collections;
using UnityEngine;

namespace FABRE.UI
{
    public class ListMovement : MonoBehaviour
    {
        [SerializeField] private Transform _openTransform;
        [SerializeField] private Transform _closedTransform;
        [SerializeField] private float _speed;

        private float delta = -.01f;
        private bool _isOpen = false;

        private void Awake()
        {
            transform.position = _closedTransform.position;
        }

        public void ToggleListMovement()
        {
            StartCoroutine(TranslationCoroutine());
        }

        private IEnumerator TranslationCoroutine()
        {
            if (!_isOpen)
            {
                while (delta < 1)
                {
                    delta += UnityEngine.Time.deltaTime * _speed;
                    yield return null;
                    transform.position = Vector2.Lerp(_closedTransform.position, _openTransform.position, delta);
                }
            }
            else
            {
                while (delta > 0)
                {
                    delta -= UnityEngine.Time.deltaTime * _speed;
                    yield return null;
                    transform.position = Vector2.Lerp(_closedTransform.position, _openTransform.position, delta);
                }
            }
            _isOpen = !_isOpen;
        }
    }
}