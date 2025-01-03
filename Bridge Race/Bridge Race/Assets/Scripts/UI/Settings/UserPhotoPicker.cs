using Architecture.Services.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Settings
{
    public class UserPhotoPicker : MonoBehaviour
    {
        [SerializeField] private Button _pickButton;
        
        private IUserDataStorage _userDataStorage;
        
        [Inject]
        public void Construct(IUserDataStorage userDataStorage)
        {
            _userDataStorage = userDataStorage;
        }

        private void OnEnable()
        {
            _pickButton.onClick.AddListener(_userDataStorage.PickPhotoFromNativeGallery);
        }

        private void OnDisable()
        {
            _pickButton.onClick.RemoveListener(_userDataStorage.PickPhotoFromNativeGallery);
        }
    }
}