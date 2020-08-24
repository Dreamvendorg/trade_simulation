using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Xml.Serialization;

namespace Simulator
{
    public enum CityTypes
    {
        center,
        plain,
        forest,
        mountains
    }

    class City
    {
        public List<City> neighbourCities = new List<City>();
        public Pop[] population = new Pop[4];
        public String name;
        public Storage tradeStorage = new Storage();
        public Storage storage = new Storage();
        public Market market = new Market();
        public int tradePower;

        private CityTypes type;

        public City(CityTypes _type)
        {
            type = _type;
            CityInit();
        }

        private void CityInit()
        {
            switch(type)
            {
                case CityTypes.center:
                    name = "Capitalist";
                    PopInit(20, 20, 20, 20);
                    break;
                case CityTypes.plain:
                    name = "Farmland";
                    PopInit(15, 10, 10, 5);
                    break;
                case CityTypes.forest:
                    name = "Forresty";
                    PopInit(10, 15, 10, 5);
                    break;
                case CityTypes.mountains:
                    name = "Craftovo";
                    PopInit(10, 10, 15, 5);
                    break;
            }
        }

        private void PopInit(int farmers, int woodcutters, int crafters, int traders)
        {
            population[(int)PopType.farmer] = new Pop(PopType.farmer, farmers);
            population[(int)PopType.woodcutter] = new Pop(PopType.woodcutter, woodcutters);
            population[(int)PopType.crafter] = new Pop(PopType.crafter, crafters);
            population[(int)PopType.trader] = new Pop(PopType.trader, traders);
        }

        public void CalculateDemand()
        {
            for (int i = 0; i < market.demand.resources.Length; ++i)
            {
                market.demand.resources[i] = 0;
            }
            foreach (Pop pop in population)
            {
                pop.CalculateDemand(CalculateSurplus(pop.type));
                for (int i = 0; i < market.demand.resources.Length; ++i)
                {
                    market.demand.resources[i] += pop.demand.resources[i];
                }
            }
        }

        private double CalculateSurplus(PopType popType)
        {
            double surplus = 0;
            ResourceType res = population[(int)popType].producingResource;
            if (res != ResourceType.none)
            {
                surplus = population[(int)popType].productionAmount * market.PriceInCoins(res);
            }
            else
            {
                if (popType == PopType.trader)
                {
                    surplus = TradeStoreWorth();
                }
            }

            return surplus;
        }

        public double TradeStoreWorth()
        {
            double worth = 0;
            for (int i = 0; i < tradeStorage.resources.Length; ++i)
            {
                worth += tradeStorage.resources[i] * market.PriceInCoins((ResourceType)i);
            }
            return worth;
        }

        public void Produce()
        {
            for (int i = 0; i < population.Length; ++i)
            {
                if (population[i].producingResource != ResourceType.none)
                {
                    population[i].productionAmount += population[i].count * population[i].productionPower * CityMod((PopType)i);
                    storage.resources[(int)population[i].producingResource] = population[i].productionAmount;
                }
            }

            tradePower = population[(int)PopType.trader].count * 10;
        }

        public void CalculateOffer()
        {

            for (int i = 0; i < market.offer.resources.Length; ++i)
            {
                market.offer.resources[i] = storage.resources[i] + tradeStorage.resources[i];
            }
        }

        private double CityMod(PopType popType)
        {
            double mod = 1;
            if (type == CityTypes.forest && popType == PopType.woodcutter) mod *= 1.5;
            if (type == CityTypes.plain && popType == PopType.farmer) mod *= 1.5;
            if (type == CityTypes.mountains && popType == PopType.crafter) mod *= 1.5;
            return mod;
        }

        public void Trade()
        {
            UseTradeStorage();
        }

        public void UseTradeStorage()
        {
            for (int i = 0; i < tradeStorage.resources.Length; ++i)
            {
                if ((population[(int)PopType.trader].demand.resources[i] > 0) && (tradeStorage.resources[i] > 0))
                {
                    double diff = Math.Min(population[(int)PopType.trader].demand.resources[i], tradeStorage.resources[i]);
                    population[(int)PopType.trader].demand.resources[i] -= diff;
                    tradeStorage.resources[i] -= diff;
                }
            }
        }

        public Deal FindBestDeal(ResourceType type)
        {
            foreach (City city in neighbourCities)
            {
                Deal bestDeal = new Deal();
                foreach (Pop popSeller in population)
                {
                    foreach (Pop popBuyer in city.population)
                    {
                        Deal newDeal = new Deal(this, city, popSeller, popBuyer);
                    }
                }
            }

            return bestDeal;
        }

        

        public string PopsInfo()
        {
            string output = "";
            foreach (Pop pop in population)
            {
                output += "Type: " + Enum.GetName(typeof(PopType), pop.type) + "\n";
                output += "Count: " + pop.count + "\n";
                output += "Wealth: " + pop.coins + "\n";
                output += "----------\n";

            }
            return output;
        }

        public string ResourceInfo()
        {
            string output = "";
            for (int i = 0; i < market.price.resources.Length; ++i)
            {
                output += "Type: " + Enum.GetName(typeof(ResourceType), (ResourceType)i) + "\n";
                output += "Price: " + market.price.resources[i] + "\n";
                output += "----------\n";
            }
            return output;
        }
    }
}
