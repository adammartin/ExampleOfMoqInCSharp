using FluentAssertions;
using Moq;
using FakeItEasy;
using Xunit;
using Candy;

namespace CandyStoreTestProject
{
    public class DeveloperTest
    {
        public DeveloperTest()
        {
            _expectedCandy.Type = "bar";
            _expectedCandy.Brand = "foo";
            _mockCandyShop = new Mock<ICandyShop>();
            _aDeveloper = new Developer();
            _fakeCandyShop = A.Fake<ICandyShop>();
        }

        [Fact]
        public void DeveloperWillBuyCandyFromTheCandyStore()
        {
            _mockCandyShop.Setup(aCandyShop => aCandyShop.BuyMostPopularCandy()).Returns(_expectedCandy);
            var theCandy = _aDeveloper.BuyTastiestCandy(_mockCandyShop.Object);
            theCandy.Should().Be(_expectedCandy);
        }

        [Fact]
        public void DeveloperWillBuyCandyFromTheCandyStoreWithFake()
        {
            A.CallTo(() => _fakeCandyShop.BuyMostPopularCandy()).Returns(_expectedCandy);
            var theCandy = _aDeveloper.BuyTastiestCandy(_fakeCandyShop);
            theCandy.Should().Be(_expectedCandy);
        }

        private readonly Developer _aDeveloper;
        private readonly RawCandy _expectedCandy;
        private readonly ICandyShop _fakeCandyShop; 
        private readonly Mock<ICandyShop> _mockCandyShop;
    }
}
