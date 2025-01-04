using TMPro;
using UI.Base;
using UnityEngine;

namespace Daily
{
    public class DailyBonusWindow : MonoBehaviour
    {
        public AnimatedWindow AnimatedWindow;
        
        [SerializeField] private TextMeshProUGUI _prizeText;

        public void Initialize(int prize)
        {
            _prizeText.text = $"+{prize}";
        }
    }
}