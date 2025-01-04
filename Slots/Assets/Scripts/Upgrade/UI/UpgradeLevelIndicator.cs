using System.Collections.Generic;
using UnityEngine;

namespace Upgrade.UI
{
    public class UpgradeLevelIndicator : MonoBehaviour
    {
        [SerializeField] private List<LevelIndicationImage> _indicatorImages;   
        
        public void Indicate(int level)
        {
            for (int i = 0; i < level; i++)
                _indicatorImages[i].Enable();
            
            _indicatorImages[level].Enable();

            for (int i = level + 1; i < _indicatorImages.Count; i++)
                _indicatorImages[i].Disable();
        }
    }
}