namespace Composite.Goods
{
    public class Confetti : IGoods
    {
        private readonly int _price;

        public Confetti(int price)
        {
            _price = price;
        }

        public int Price()
        {
            return _price;
        }
    }
}