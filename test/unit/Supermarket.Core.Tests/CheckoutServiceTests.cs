using System;
using System.Threading.Tasks;
using Moq;
using Supermarket.Core.Contracts;
using Supermarket.Core.Entities;
using Supermarket.Core.Services;
using Xunit;

namespace Supermarket.Core.Tests
{
    public class CheckoutServiceTests
    {
        private readonly CheckoutService _sut;
        private readonly Mock<ICart> _mockCart;
        private readonly Mock<ITaxCalculator> _mockTaxCalculator;
        private readonly double _taxAmount = 1.15;
        private readonly string _mockPostalCode = "H1Z 2B4";

        public CheckoutServiceTests()
        {
            _mockCart = new Mock<ICart>();
            _mockTaxCalculator = new Mock<ITaxCalculator>();
            _mockTaxCalculator
                .Setup(tc => tc.Apply(It.IsAny<double>(), It.IsAny<string>()))
                .Returns((double x, string y) => Task.FromResult(x * _taxAmount));
            _sut = new CheckoutService(_mockCart.Object, _mockTaxCalculator.Object);
        }

        public class CheckOut : CheckoutServiceTests
        {
            [Fact]
            public void GivenCart_WithInvalidPostalCode_ThrowsException()
            {
                Assert.ThrowsAsync<ArgumentException>(() => _sut.Checkout(null));
            }

            [Fact]
            public async Task GivenNoItemsInCart_ThenCartCostIsZero()
            {
                _mockCart.Setup(c => c.Get()).Returns(new CartItem[0]);
                _mockTaxCalculator
                    .Setup(tc => tc.Apply(It.IsAny<double>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(0d));

                var actual = await _sut.Checkout(_mockPostalCode);

                Assert.Equal(0, actual.PreTax);
                Assert.Equal(0, actual.PostTax);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(50.55)]
            public async Task GivenSingleItemInTheCart_WhenCheckingOut_ThenPreTaxCostIsSingleItem(double itemCost)
            {
                var cartItem = MockCartItem(itemCost).Object;
                _mockCart.Setup(c => c.Get()).Returns(new CartItem[] { cartItem });

                var actual = await _sut.Checkout(_mockPostalCode);

                Assert.Equal(cartItem.Cost(), actual.PreTax);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(50.55)]
            public async Task GivenMultipleItemsInTheCart_ThenPreTaxCostIsTotalOfAllItems(double itemCost)
            {
                var cartItem1 = MockCartItem(itemCost).Object;
                var cartItem2 = MockCartItem(itemCost).Object;
                _mockCart.Setup(c => c.Get()).Returns(new CartItem[] { cartItem1, cartItem2 });

                var actual = await _sut.Checkout(_mockPostalCode);

                Assert.Equal(cartItem1.Cost() + cartItem2.Cost(), actual.PreTax);
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(50, 0)]
            [InlineData(50, 25)]
            public async Task GivenItemsInCart_WhenCalculatingTax_ThenPostTaxAmountIsGreaterOrEqualThanPreTaxAmount(double cost1, double cost2)
            {
                var cartItem1 = MockCartItem(cost1).Object;
                var cartItem2 = MockCartItem(cost2).Object;
                _mockCart.Setup(c => c.Get()).Returns(new CartItem[] { cartItem1, cartItem2 });

                var actual = await _sut.Checkout(_mockPostalCode);

                Assert.True(actual.PostTax >= actual.PreTax);
            }
        }

        public class Receipt : CheckoutServiceTests
        {
            [Fact]
            public void GivenEmptyCart_ThenReceiptIsEmpty()
            {
                _mockCart.Setup(c => c.Get()).Returns(new CartItem[0]);

                var actual = _sut.Receipt();

                Assert.Empty(actual);
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(50, 0)]
            [InlineData(50, 25)]
            public void GivenItemsInCart_ThenReturnsReceiptItems(double cost1, double cost2)
            {
                var cartItem1 = MockCartItem(cost1).Object;
                var cartItem2 = MockCartItem(cost2).Object;
                _mockCart.Setup(c => c.Get()).Returns(new CartItem[] { cartItem1, cartItem2 });

                var actual = _sut.Receipt();

                Assert.Equal(2, actual.Length);
            }
        }

        private Mock<CartItem> MockCartItem(double itemCost)
        {
            var mock = new Mock<CartItem>();
            mock.Setup(item => item.Cost()).Returns(50.55);
            return mock;
        }

    }
}
