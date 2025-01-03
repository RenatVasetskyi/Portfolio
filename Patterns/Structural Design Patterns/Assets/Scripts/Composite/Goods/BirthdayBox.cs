namespace Composite.Goods
{
    public class BirthdayBox : IGoods
    {
        private readonly int _price;
        private readonly Confetti _confetti;
        private readonly Sneakers _sneakers;

        public BirthdayBox(int price, Confetti confetti, Sneakers sneakers)
        {
            _price = price;
            _confetti = confetti;
            _sneakers = sneakers;
        }
        
        public int Price()
        {
            return _price + _confetti.Price() + _sneakers.Price();
        }
    }
}