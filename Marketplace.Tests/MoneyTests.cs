using Marketplace.Domain;
using Xunit;

namespace Marketplace.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void Money_objects_with_the_same_amount_should_be_equal()
        {
            var money1 = Money.FromDecimal(5, "Usd");
            var money2 = Money.FromDecimal(5, "USD");

            Assert.Equal(money1, money2);
        }

        [Fact]
        public void Sum_of_money_gives_full_amount()
        {
            var coin1 = Money.FromDecimal(1);
            var coin2 = Money.FromDecimal(2);
            var coin3 = Money.FromDecimal(2);

            var banknote = Money.FromDecimal(5);

            Assert.Equal(banknote, coin1 + coin2 + coin3);
        }
    }
}
