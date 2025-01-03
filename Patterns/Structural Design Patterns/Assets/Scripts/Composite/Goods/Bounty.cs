namespace Composite.Goods
{
    public class Bounty : IGoods
    {
        private readonly int _price;

        public Bounty(int price)
        {
            _price = price;
        }
        
        public int Price()
        {
            return _price;
        }
    }
}