using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.PlayButtonAnimation
{
    public class Line : MonoBehaviour
    {
        private const float MoveDuration = 0.08f;
        private const int Spins = 5;
        
        [SerializeField] private List<Transform> _movePositions;
        [SerializeField] private List<Element> _elements;
        [SerializeField] private List<Sprite> _elementSprites;
        [SerializeField] private float _spinDelay;

        public bool IsStopped { get; private set; } = true;

        public IEnumerator PlayAnimation()
        {
            IsStopped = false;
            
            yield return new WaitForSeconds(_spinDelay);
            
            int stoppedCount = 0;
            
            for (int i = 0; i < Spins; i++)
            {
                foreach (Element element in _elements)
                {
                    int currentPosition = element.PositionInLine;
                    int nextPosition = currentPosition + 1;
                
                    if (nextPosition < _movePositions.Count)
                    {
                        element.PositionInLine++;
                    
                        LeanTween.moveLocal(element.gameObject, _movePositions[element.PositionInLine]
                            .localPosition, MoveDuration).setEase(LeanTweenType.linear).setOnComplete(() => stoppedCount++);
                    }
                    else
                    {
                        element.PositionInLine = 0;
                        element.transform.localPosition = _movePositions[element.PositionInLine].localPosition;

                        element.SetSprite(_elementSprites[Random.Range(0, _elementSprites.Count)]);

                        element.PositionInLine++;

                        LeanTween.moveLocal(element.gameObject, _movePositions[element.PositionInLine]
                            .localPosition, MoveDuration).setEase(LeanTweenType.linear).setOnComplete(() => stoppedCount++);
                    }
                }
                
                yield return new WaitForSeconds(MoveDuration);
            }
            
            yield return new WaitUntil(() => stoppedCount >= _elements.Count * Spins);

            IsStopped = true;
        }

        private void Awake()
        {
            foreach (Element element in _elements)
                element.SetSprite(_elementSprites[Random.Range(0, _elementSprites.Count)]);
        }
    }
}