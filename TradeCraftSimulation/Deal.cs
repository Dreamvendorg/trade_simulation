using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;

namespace Simulator
{
    class Deal
    {
        public bool isPossibly;
        public City cityFrom;
        public City cityTo;
        public Pop seller;
        public Pop buyer;

        public double amount;
        public double result;
        public double expectedAmount;
        public double profitPerUnit;

        public Deal()
        {
            isPossibly = false;
        }

        public Deal(City _cityFrom, City _cityTo, Pop _seller, Pop _buyer)
        {
            cityFrom = _cityFrom;
            cityTo = _cityTo;
            seller = _seller;
            buyer = _buyer;
            CalculateProfit();
        }

        public void CalculateProfit()
        {
            //ammount = seller.productionAmount;
            amount = Math.Min(seller.productionAmount, buyer.productionAmount * cityTo.market.PriceInAnotherResource(seller.producingResource, buyer.producingResource));
            result = amount * cityTo.market.PriceInAnotherResource(buyer.producingResource, seller.producingResource);
            expectedAmount = result * cityFrom.market.PriceInAnotherResource(seller.producingResource, buyer.producingResource);
            profitPerUnit = (expectedAmount - amount) / amount;
            isPossibly = (amount > 0) ? true : false;
        }
    }
}
