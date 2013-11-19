using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Candy;

namespace Candy
{
    public class Developer
    {
        public RawCandy BuyTastiestCandy(ICandyShop candyShop)
        {
            return candyShop.BuyMostPopularCandy();
        }
    }
}
