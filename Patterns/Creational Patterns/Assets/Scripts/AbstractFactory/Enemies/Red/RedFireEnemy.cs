using UnityEngine;

namespace AbstractFactory.Enemies.Red
{
    public class RedFireEnemy : FireEnemy
    {
        [SerializeField] private int _fireCount;
        [SerializeField] private int _castFrequency;

        public void Initialize(int fireCount, int castFrequency)
        {
            _fireCount = fireCount;
            _castFrequency = castFrequency;
            base.Initialize(_beamLenght);
        }
        
        public override void CastFireBeam()
        {
            Debug.Log("Red fire enemy cast fire beam");
        }
    }
}