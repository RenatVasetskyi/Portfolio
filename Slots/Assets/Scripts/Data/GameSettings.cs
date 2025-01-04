using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Game.Effects;
using UnityEngine;
using Upgrade;
using Upgrade.Enums;

namespace Data
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Create Settings Holder/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public Effect[] Effects;
        public LeanTweenType MoveSlotEase;
        public Vector3 SlotsScale;
        public SlotWinCombinations SlotWinCombinations;
        public SerializedDictionary<UpgradeableType, List<UpgradeableStaticData>> UpgradeableStaticData;
        public int AddXpForWinCombination;
    }
}