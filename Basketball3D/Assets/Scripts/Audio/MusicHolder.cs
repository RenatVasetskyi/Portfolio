using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    [CreateAssetMenu(fileName = "MusicHolder", menuName = "Create Music Holder/Holder")]
    public class MusicHolder : ScriptableObject
    {
        public List<MusicData> Musics;
    }
}