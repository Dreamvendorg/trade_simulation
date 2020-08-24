using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Simulator
{
    class Simulation
    {
        public City[] Cities = new City[4];
        public int turn = 0;

        public Simulation()
        {
            Cities[0] = new City(CityTypes.center);
            Cities[1] = new City(CityTypes.plain);
            Cities[2] = new City(CityTypes.forest);
            Cities[3] = new City(CityTypes.mountains);

            ConnectCities(Cities[0], Cities[1]);
            ConnectCities(Cities[0], Cities[2]);
            ConnectCities(Cities[0], Cities[3]);
        }

        private void ConnectCities(City city1, City city2)
        {
            city1.neighbourCities.Add(city2);
            city2.neighbourCities.Add(city1);
        }

        public void Turn()
        {
            ++turn;
            foreach (City city in Cities)
            {
                city.market.CalculatePrice();
                city.CalculateDemand();
                city.Produce();
            }

            foreach (City city in Cities)
            {
                city.Trade();
            }

            foreach (City city in Cities)
            {
                city.CalculateOffer();
            }
        }

        public string CityView()
        {
            string output = "=====================CITIES OVERVIEW=====================\n";
            foreach (City city in Cities)
            {
                output += city.name + "\n";
                output += city.PopsInfo() + "\n";
                //output += "\n-----------------\n";
                output += city.ResourceInfo() + "\n";
                output += "\n-----------------\n";
            }

            return output;
        }
    }
}
