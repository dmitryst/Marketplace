using Marketplace.Domain;
using Xunit;

namespace Marketplace.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void Money_objects_with_the_same_amount_should_be_equal()
        {
            var money1 = new Money(5);
            var money2 = new Money(5);

            Assert.Equal(money1, money2);
        }
    }
}
