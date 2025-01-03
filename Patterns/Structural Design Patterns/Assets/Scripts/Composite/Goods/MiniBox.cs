namespace Composite.Goods
{
    public class MiniBox : IGoods
    {
        private readonly int _price;
        
        private readonly Sneakers _sneakers;
        private readonly Bounty _bounty;

        public MiniBox( int price, Sneakers sneakers, Bounty bounty)
        {
            _sneakers = sneakers;
            _bounty = bounty;
            _price = price;
        }
        
        public int Price()
        {
            return _price + _sneakers.Price() + _bounty.Price();
        }
    }
}