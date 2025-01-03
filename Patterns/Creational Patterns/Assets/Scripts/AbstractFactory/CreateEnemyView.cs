using AbstractFactory.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace AbstractFactory
{
    public class CreateEnemyView : MonoBehaviour
    {
        [SerializeField] private Toggle _createRedEnemyToggle;
        [SerializeField] private Toggle _createBlueEnemyToggle;

        [SerializeField] private Button _fireEnemyButton;
        [SerializeField] private Button _mageEnemyButton;
        [SerializeField] private Button _waterEnemyButton;
        
        private EnemyFactory _enemyFactory = new RedEnemyFactory();

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _createBlueEnemyToggle.onValueChanged.AddListener(SetBlueType);
            _createRedEnemyToggle.onValueChanged.AddListener(SetRedType);
            
            _fireEnemyButton.onClick.AddListener(CreateFireEnemy);
            _mageEnemyButton.onClick.AddListener(CreateMageEnemy);
            _waterEnemyButton.onClick.AddListener(CreateWaterEnemy);
        }

        private void Unsubscribe()
        {
            _createBlueEnemyToggle.onValueChanged.RemoveListener(SetBlueType);
            _createRedEnemyToggle.onValueChanged.RemoveListener(SetRedType);
            
            _fireEnemyButton.onClick.RemoveListener(CreateFireEnemy);
            _mageEnemyButton.onClick.RemoveListener(CreateMageEnemy);
            _waterEnemyButton.onClick.RemoveListener(CreateWaterEnemy);
        }

        private void SetRedType(bool enable)
        {
            if (enable)
                _enemyFactory = new RedEnemyFactory();
        }

        private void SetBlueType(bool enable)
        {
            if (enable)
                _enemyFactory = new BlueEnemyFactory();
        }

        private void CreateFireEnemy()
        {
            _enemyFactory.CreateFireEnemy();
        }

        private void CreateMageEnemy()
        {
            _enemyFactory.CreateMageEnemy();
        }
        
        private void CreateWaterEnemy()
        {
            _enemyFactory.CreateWaterEnemy();
        }
    }
}