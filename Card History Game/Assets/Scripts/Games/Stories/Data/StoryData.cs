using System;
using System.Collections.Generic;
using Games.Stories.Enums;
using Games.Stories.UI;
using UI.Base;
using UnityEngine;

namespace Games.Stories.Data
{
    [Serializable]
    public class StoryData
    {
        public StoryType Type;
        public List<StoryPieceData> StoryPieces;
        public string CongratulationsText;
        public Fade VictoryWindowPrefab;
    }
}