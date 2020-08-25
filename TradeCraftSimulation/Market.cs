using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulator
{
    class Market
    {
        public Storage demand = new Storage();
        public Storage offer = new Storage();
        public Storage price = new Storage();

        public double BASIC_FOOD_PRICE = 2;
        public double BASIC_WOOD_PRICE = 1;
        public double BASIC_COINS_PRICE = 10;

        //public double NO_PRODUCTION_MODE = 3;

        public Market()
        {
            price.resources[(int)ResourceType.food] = BASIC_FOOD_PRICE;
            price.resources[(int)ResourceType.wood] = BASIC_WOOD_PRICE;
            price.resources[(int)ResourceType.coins] = BASIC_COINS_PRICE;
        }

        public void CalculatePrice()
        {
            for (int i = 0; i < price.resources.Length; ++i)
            {
                price.resources[i] = Math.Max(demand.resources[i], 1) / Math.Max(offer.resources[i], 1);
            }
        }

        public double PriceInCoins(ResourceType type)
        {
            return PriceInAnotherResource(type, ResourceType.coins);
        }

        public double PriceInAnotherResource(ResourceType sellType, ResourceType buyType)
        {
            if (sellType == ResourceType.none || buyType == ResourceType.none)
            {
                return 0;
            }
            else
            {
                return price.resources[(int)sellType] / price.resources[(int)buyType];
            }
        }
    }
}
