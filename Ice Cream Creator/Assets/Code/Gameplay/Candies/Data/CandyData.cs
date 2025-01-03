using System;
using System.Collections.Generic;
using Code.Gameplay.Candies.Enums;
using UnityEngine;

namespace Code.Gameplay.Candies.Data
{
    [Serializable]
    public class CandyData
    {
        public FullCandyType FullCandyType;
        public Sprite FullCandySprite;
        public List<HalfCandyData> HalfCandyDatas;
    }
}