using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace FABRE.VisualEffect
{
    public class DamageEffect : MonoBehaviour
    {
        public static DamageEffect Instance { get; private set; }
        
        [SerializeField] private Volume _volumeComponent;
        [SerializeField] private float _StartValue;
        [SerializeField] private float _EndValue;
        
        [SerializeField] private float _duration = 2;
        [SerializeField] private int _cycles = 3;
        
        private float _deltaOverTime;

        public void StartEffect()
        {
            StartCoroutine(LerpLoopEffect(_cycles));
        }
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            
            _deltaOverTime = _StartValue;
        }

        private void Update()
        {
            _volumeComponent.weight = _deltaOverTime;
        }

        private IEnumerator LerpLoopEffect(int cycles)
        {
            for (int i = 0; i < cycles; i++)
            {
                yield return StartCoroutine(LerpOverTimeEffect(_StartValue, _EndValue, _duration));
                
                yield return StartCoroutine(LerpOverTimeEffect(_EndValue, _StartValue, _duration));
            }
        }

        private IEnumerator LerpOverTimeEffect(float startValue, float endValue, float duration)
        {
            float elapsedTime = 0;
            
            while (elapsedTime < duration)
            {
                elapsedTime += UnityEngine.Time.deltaTime;
                _deltaOverTime = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
                yield return null;
            }

            _deltaOverTime = endValue;
        }
    }
}