using Assets.Scripts.Architecture.States;
using Assets.Scripts.Architecture.States.Interfaces;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuModel
    {
        public void MoveToGameScene(IStateMachine stateMachine) =>
            stateMachine.Enter<LoadGameState>();

        public void Exit() =>
            Application.Quit();

        public void UpdateTheHighestScore(TextMeshProUGUI text, int score) =>
            text.text = score.ToString();
    }
}