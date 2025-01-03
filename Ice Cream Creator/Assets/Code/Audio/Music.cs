using System;
using Code.Audio.Enums;
using UnityEngine;

namespace Code.Audio
{
    [Serializable]
    public class Music
    {
        public MusicTypeEnum Type;
        public AudioClip Clip;
    }
}