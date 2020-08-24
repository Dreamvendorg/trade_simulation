using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;

namespace Simulator
{
    public enum ResourceType
    {
        none = -1,
        food,
        wood,
        coins
    }
    class Storage
    {
        public double[] resources = new double[3];
    }
}
