using System.Collections;
using Assets.Scripts.Architecture.States;
using Assets.Scripts.Architecture.States.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _seconds;

        [SerializeField] private TextMeshProUGUI _timerText;

        private float _step = 1;

        private IStateMachine _stateMachine;

        [Inject]
        public void Construct(IStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        private void Start() =>
            StartCoroutine(TimerCoroutine());

        private IEnumerator TimerCoroutine()
        {
            _timerText.text = _seconds.ToString();

            while (_seconds > 0)
            {
                yield return new WaitForSeconds(_step);
                _seconds -= _step;
                _timerText.text = _seconds.ToString();
            }

            _stateMachine.Enter<GameOverState>();
        }
    }
}