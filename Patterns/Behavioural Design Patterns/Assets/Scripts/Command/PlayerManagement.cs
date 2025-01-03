using System.Collections.Generic;
using System.Linq;
using Command.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Command
{
    public class PlayerManagement : MonoBehaviour
    {
        private readonly List<ICancellableCommand> _commands = new();
        
        [SerializeField] private Button _moveDownButton;
        [SerializeField] private Button _moveStraightButton;
        [SerializeField] private Button _undoButton;

        [SerializeField] private float _step;

        [SerializeField] private Transform _player;

        private void OnEnable()
        {
            _moveDownButton.onClick.AddListener(MoveDown);
            _moveStraightButton.onClick.AddListener(MoveStraight);
            _undoButton.onClick.AddListener(Undo);
        }

        private void OnDisable()
        {
            _moveDownButton.onClick.RemoveListener(MoveDown);
            _moveStraightButton.onClick.RemoveListener(MoveStraight);
            _undoButton.onClick.RemoveListener(Undo);
        }

        private void MoveDown()
        {
            ICancellableCommand command = new MoveDownCommand(_step, _player);
            _commands.Add(command);
            command.Execute();
        }

        private void MoveStraight()
        {
            ICancellableCommand command = new MoveStraightCommand(_step, _player);
            _commands.Add(command);
            command.Execute();
        }

        private void Undo()
        {
            if (_commands.Count > 0)
            {
                ICancellableCommand lastCommand = _commands.Last();
                lastCommand.Undo();
                _commands.Remove(lastCommand);
            }
        }
    }
}
