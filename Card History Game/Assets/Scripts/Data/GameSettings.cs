using System.Collections.Generic;
using Audio;
using Games.ComposeTheSubject.Data;
using Games.Stories.Data;
using UI.Background;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Create Settings Holder/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Shop")]
        public MusicHolder MusicHolder;
        public BackgroundSkin[] BackgroundSkins;

        [Header("Stories")]
        public List<StoryData> Stories;

        [Header("Compose The Subject")]
        public List<ComposeTheSubjectQuestion> ComposeTheSubjectQuestions;
        public int CheckTriesCount;
        public int HintsCount;
        public Sprite[] RightTexts;
    }
}