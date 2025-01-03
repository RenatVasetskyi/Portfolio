using Code.GameAudio;
using Code.Gaming;
using Code.MainUI.Store;
using Code.MainUI.Store.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.RootGameData
{
    [CreateAssetMenu(fileName = "Game Data Holder", menuName = "Spawn Game Data Holder")]
    public class GameDataWrapper : ScriptableObject
    {
        public AllSfxesHolder _allSfxesHolder;
        public AllMusicsHolder _allMusicsHolder;
        public StoreRopeLotData[] Ropes;
        public StoreHookLotData[] Hooks;
        public Fish[] Fishes;
    }
}