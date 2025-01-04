using Architecture.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Game
{
    public class CoefficientSlot : Slot
    {
        [SerializeField] private float _coefficient;
        [SerializeField] private Material _material;
        
        private IBaseFactory _baseFactory;

        [Inject]
        public void Construct(IBaseFactory baseFactory)
        {
            _baseFactory = baseFactory;
        }
         
         public override void WinAction()
         {
             _slotSystem.MultiplyWinCount(_coefficient);
             _baseFactory.CreateRandomEffect(_material, transform.root);
         }
    }
}