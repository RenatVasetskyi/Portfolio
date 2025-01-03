using UnityEngine;

namespace AbstractFactory.Enemies.Red
{
    public class RedMageEnemy : MageEnemy
    {
        [SerializeField] private float _sphereSize;

        public void Initialize(float sphereSize)
        {
            _sphereSize = sphereSize;
            base.Initialize(_mana);
        }
        
        public override void CastSphere()
        {
            Debug.Log("Red mage enemy cast sphere");
        }
    }
}