using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public enum PopType
    {
        farmer,
        woodcutter,
        crafter,
        trader
    }

    class Pop
    {
        public PopType type;
        public int count = 0;
        public int newborn = 0;
        public Storage demand = new Storage();
        public double coins = 0;
        public ResourceType producingResource;
        public double productionPower;
        public double productionAmount;

        public Pop(PopType _type, int _count)
        {
            type = _type;
            count = _count;
            InitPop();
        }

        private void InitPop()
        {
            switch (type)
            {
                case PopType.farmer:
                    producingResource = ResourceType.food;
                    productionPower = 5;
                    break;
                case PopType.woodcutter:
                    producingResource = ResourceType.wood;
                    productionPower = 10;
                    break;
                case PopType.crafter:
                    producingResource = ResourceType.coins;
                    productionPower = 1;
                    break;
                default:
                    producingResource = ResourceType.none;
                    break;
            }
        }
        public void CalculateDemand(double surplus)
        {
            demand.resources[(int)ResourceType.food] = count * 1;
            demand.resources[(int)ResourceType.wood] = count * 1 + newborn * 5;
            demand.resources[(int)ResourceType.coins] = surplus;
        }

    }
}
