using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Game
{
    [RequireComponent(typeof(Image))]
    public class RandomBackground : MonoBehaviour
    {
        private Data _data;
        private Image _image;

        [Inject]
        public void Initialize(Data data)
        {
            _data = data;
        }

        private void Awake()
        {
            GetComponent<Image>().sprite = _data.Backgrounds[Random.Range(0, _data.Backgrounds.Count)];
        }
    }
}