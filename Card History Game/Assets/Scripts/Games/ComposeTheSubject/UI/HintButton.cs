using System.Collections;
using Games.ComposeTheSubject.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Games.ComposeTheSubject.UI
{
    public class HintButton : MonoBehaviour
    {
        private const float UnlockDelay = 2f;
        
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;
        
        private IComposeTheSubjectGameController _gameController;
        
        private int _hintCount;

        public void Initialize(IComposeTheSubjectGameController gameController)
        {
            _gameController = gameController;
            Subscribe();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(MakeHint);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(MakeHint);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void MakeHint()
        {
            if (_gameController != null)
            {
                _button.interactable = false;
                _gameController.MakeHint();   
                StartCoroutine(UnlockWithDelay());
            }
        }

        private void Subscribe()
        {
            _gameController.OnHintUsed += UpdateHintText;
        }
        
        private void Unsubscribe()
        {
            if (_gameController != null)
                _gameController.OnHintUsed -= UpdateHintText;
        }

        private void UpdateHintText(int count)
        {
            _text.text = $"{count} att";
            _hintCount = count;
        }

        private IEnumerator UnlockWithDelay()
        {
            if (_hintCount <= 0)
                yield break;
            
            yield return new WaitForSeconds(UnlockDelay);

            _button.interactable = true;
        }
    }
}