using System;
using Simulator;

namespace TradeCraftSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Simulation sim = new Simulation();

            Console.WriteLine("\n@@@@@@@@@@@@@@@@@@@@@@@@__TURN " + sim.turn + "__@@@@@@@@@@@@@@@@@@@@@@@\n");
            Console.WriteLine(sim.CityView());
            for (int i = 0; i < 3; ++i)
            {
                sim.Turn();
                Console.WriteLine("\n@@@@@@@@@@@@@@@@@@@@@@@@__TURN " + sim.turn + "__@@@@@@@@@@@@@@@@@@@@@@@\n");
                Console.WriteLine(sim.CityView());
            }
            Console.ReadKey();
        }
    }
}
