using System.Collections.Generic;
using UnityEngine;

namespace Code.Game.PlayerLogic
{
    public class PlayerHpView : MonoBehaviour
    {
        private const int MinHp = 0;
        
        [SerializeField] private List<Heart> _hearts;

        public void UpdateHearts(int hp)
        {
            if (hp <= MinHp)
            {
                foreach (Heart heart in _hearts)
                    heart.Off();

                return;
            }
            
            for (int i = 0; i < hp; i++)
                _hearts[i].On();                

            for (int i = hp; i < _hearts.Count; i++)
                _hearts[i].Off();
        }
    }
}