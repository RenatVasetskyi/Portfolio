using System;
using Games.Stories.Cards.Enums;
using UnityEngine;

namespace Games.Stories.Data
{
    [Serializable]
    public class StoryPieceData
    {
        public StoryPieceType Type;
        public string Text;
        public Sprite Sprite;
    }
}