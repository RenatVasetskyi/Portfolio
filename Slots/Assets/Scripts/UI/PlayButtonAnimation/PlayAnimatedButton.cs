using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.PlayButtonAnimation
{
    public class PlayAnimatedButton : MonoBehaviour
    {
        [SerializeField] private List<Line> _lines;

        private void Awake()
        {
            StartCoroutine(PlayAnimation());
        }

        private IEnumerator PlayAnimation()
        {
            while (true)
            {
                foreach (Line line in _lines)
                    StartCoroutine(line.PlayAnimation());
                
                yield return new WaitUntil(() => _lines.TrueForAll(line => line.IsStopped));
            }   
        }
    }
}