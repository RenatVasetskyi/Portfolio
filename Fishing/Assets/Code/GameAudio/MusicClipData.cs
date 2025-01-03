using System;
using Code.GameAudio.Enums;
using UnityEngine;

namespace Code.GameAudio
{
    [Serializable]
    public class MusicClipData
    {
        public Musics Type;
        public AudioClip Clip;
    }
}