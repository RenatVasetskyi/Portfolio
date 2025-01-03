using UnityEngine;
using UnityEngine.UI;

namespace FactoryMethod
{
    public class CreateControllerView : MonoBehaviour
    {
        [SerializeField] private Button _createPlayerControllerButton;
        [SerializeField] private Button _createEnemyControllerButton;

        private void OnEnable()
        {
            _createPlayerControllerButton.onClick.AddListener(CreatePlayerController);
            _createEnemyControllerButton.onClick.AddListener(CreateEnemyController);
        }

        private void OnDisable()
        {
            _createPlayerControllerButton.onClick.RemoveListener(CreatePlayerController);
            _createEnemyControllerButton.onClick.RemoveListener(CreateEnemyController);
        }

        private void CreatePlayerController()
        {
            ControllerFactory creator = new PlayerControllerCreator();
            Controller controller = creator.Create();
            controller.Control();
        }
        private void CreateEnemyController()
        {
            ControllerFactory creator = new EnemyControllerCreator();
            Controller controller = creator.Create();
            controller.Control();
        }
    }
}