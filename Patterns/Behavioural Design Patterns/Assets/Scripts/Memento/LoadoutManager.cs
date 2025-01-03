using UnityEngine;
using UnityEngine.UI;

namespace Memento
{
    public class LoadoutManager : MonoBehaviour
    {
        private readonly HotBar.Memento[] _loadoutMementos = new HotBar.Memento[3];
        
        [SerializeField] private HotBar _hotBar;
        [SerializeField] private Button[] _loadoutButtons;
        [SerializeField] private Button _saveButton;

        private int _selectedLoadout;

        private void Awake()
        {
            CreateStartMementosAndSubscribeButtons();
        }

        private void CreateStartMementosAndSubscribeButtons()
        {
            for (int i = 0; i < _loadoutMementos.Length; i++)
            {
                _loadoutMementos[i] = _hotBar.CreateMemento();

                int index = i;
                
                _loadoutButtons[i].onClick.AddListener(() => SelectLoadout(index));
            }

            _saveButton.onClick.AddListener(SaveLoadout);
        }

        private void SelectLoadout(int index)
        {
            SaveLoadout();
            
            _selectedLoadout = index;
            _hotBar.SetMemento(_loadoutMementos[index]);
        }

        private void SaveLoadout()
        {
            _loadoutMementos[_selectedLoadout] = _hotBar.CreateMemento();
        }
    }
}