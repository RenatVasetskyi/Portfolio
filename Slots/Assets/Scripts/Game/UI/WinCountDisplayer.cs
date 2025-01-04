using Game.Interfaces;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class WinCountDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private IWinCountReporter _winCountReporter;

        public void Initialize(IWinCountReporter winCountReporter)
        {
            _winCountReporter = winCountReporter;

            Subscribe();
        }

        private void Awake()
        {
            Display(0);
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }
        
        private void Display(int count)
        {
            _text.text = count.ToString();
        }

        private void Subscribe()
        {
            _winCountReporter.OnWin += Display;
        }

        private void UnSubscribe()
        {
            _winCountReporter.OnWin -= Display;
        }
    }
}