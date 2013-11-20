using System;

namespace Candy
{
    public struct RawCandy
    {
        public string Brand;
        public string Type;
    }

    public interface ICandyShop
    {
        RawCandy BuyMostPopularCandy();
    }

    public class CandyShop : ICandyShop
    {
        public RawCandy BuyMostPopularCandy()
        {
            throw new NotImplementedException("You should be using the Mock silly!");
        }
    }
}
