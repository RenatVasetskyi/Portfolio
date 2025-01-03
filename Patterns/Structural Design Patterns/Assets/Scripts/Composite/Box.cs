using System;
using System.Collections.Generic;

namespace Composite
{
    public class Box : IGoods
    {
        private readonly List<IGoods> _goods;

        public Box(List<IGoods> goods)
        {
            _goods = goods;
        }

        public int Price()
        {
            int price = 0;
            
            foreach (IGoods goods in _goods)
            {
                price += goods.Price();
            }

            return price;
        }
    }
}
