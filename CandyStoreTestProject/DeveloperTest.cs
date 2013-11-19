using System;
using Moq;
using Xunit;
using Candy;

namespace CandyStoreTestProject
{
    public class DeveloperTest
    {
        [Fact]
        public void DeveloperWillBuyCandyFromTheCandyStore()
        {
            var mockCandyShop = new Mock<ICandyShop>();
            RawCandy expectedCandy;
            expectedCandy.Type = "bar";
            expectedCandy.Brand = "foo";
            mockCandyShop.Setup(aCandyShop => aCandyShop.BuyMostPopularCandy()).Returns(expectedCandy).Verifiable();

            var aDeveloper = new Developer();
            aDeveloper.BuyTastiestCandy(mockCandyShop.Object);
            mockCandyShop.Verify();
        }
    }
}
