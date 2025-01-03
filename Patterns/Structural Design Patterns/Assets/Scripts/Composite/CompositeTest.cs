using System.Collections.Generic;
using Composite.Goods;
using UnityEngine;

namespace Composite
{
    public class CompositeTest : MonoBehaviour
    {
        private void Awake()
        {
            Box box1 = new Box(new List<IGoods>()
            {
                new MiniBox(10, new Sneakers(15), new Bounty(12)),
                new BirthdayBox(12, new Confetti(5), new Sneakers(15))
            });
            
            Box box2 = new Box(new List<IGoods>()
            {
                new Bounty(17),
                new MiniBox(4, new Sneakers(18), new Bounty(15)),
                new MiniBox(5, new Sneakers(16), new Bounty(13)),
                new BirthdayBox(15, new Confetti(6), new Sneakers(20))
            });
            
            Debug.Log($"Box1 price: {box1.Price()}");
            Debug.Log($"Box2 price: {box2.Price()}");
        }
    }
}