using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class MainSlotsData
    {
        public ScriptableObject WinCombinations;
        public List<SlotPack> SlotPacks;
    }
}
