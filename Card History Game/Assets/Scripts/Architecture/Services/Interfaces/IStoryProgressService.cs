using Games.Stories.Data;
using Games.Stories.Enums;

namespace Architecture.Services.Interfaces
{
    public interface IStoryProgressService
    {
        StoryData SelectedStory { get; }
        void SelectStory(StoryType type);
        void SetNextStoryToPass();
        StoryData GetCurrentStoryToPass();
        void Load();
        void SelectNextStory();
        bool CanSelectNextStory();
        bool IsGamePassed();
    }
}