using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Candy;

namespace CandyStoreTestProject
{
    class Developer
    {
        public void BuyTastiestCandy(ICandyShop candyShop)
        {
            candyShop.BuyMostPopularCandy();
        }
    }
}
