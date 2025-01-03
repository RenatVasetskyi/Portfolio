using System;
using Code.Audio.Enums;
using UnityEngine;

namespace Code.Audio
{
    [Serializable]
    public class Sfx
    {
        public SfxTypeEnum Type;
        public AudioClip Clip;
    }
}