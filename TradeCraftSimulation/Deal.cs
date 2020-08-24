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
        public ResourceType sellResource;
        public ResourceType buyResource;

        public double ammount;
        public double result;
        public double profitPerUnit;

        public Deal()
        {

        }

        public Deal(City _cityFrom, City _cityTo, ResourceType _sellResource, ResourceType _buyResource)
        {
            cityFrom = _cityFrom;
            cityTo = _cityTo;
            sellResource = _sellResource;
            buyResource = _buyResource;
        }

        public void CalculateProfit()
        {
            //ammount = cityFrom.
            //
        }
    }
}
