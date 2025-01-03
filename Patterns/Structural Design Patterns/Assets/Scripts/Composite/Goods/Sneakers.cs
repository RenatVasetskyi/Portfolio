namespace Composite.Goods
{
    public class Sneakers : IGoods
    {
        private readonly int _price;

        public Sneakers(int price)
        {
            _price = price;
        }
        
        public int Price()
        {
            return _price;
        }
    }
}