using TMPro;
using UnityEngine;

namespace Daily
{
    public class DailyBonusWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _prizeText;
        
        public void Initialize(int prize)
        {
            _prizeText.text = "+" + prize.ToString("#,##0");
        }
    }
}