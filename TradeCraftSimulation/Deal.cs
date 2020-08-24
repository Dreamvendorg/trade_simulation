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

        public double ammount;
        public double result;
        public double profitPerUnit;

        public Deal()
        {

        }

        public Deal(City _cityFrom, City _cityTo, Pop _seller, Pop _buyer)
        {
            cityFrom = _cityFrom;
            cityTo = _cityTo;
            seller = _seller;
            buyer = _buyer;
        }

        public void CalculateProfit()
        {
            //ammount = cityFrom.
            //
        }
    }
}
