using System;
using Code.GameAudio.Enums;
using UnityEngine;

namespace Code.GameAudio
{
    [Serializable]
    public class SfxClipData
    {
        public Sfxes Type;
        public AudioClip Clip;
    }
}