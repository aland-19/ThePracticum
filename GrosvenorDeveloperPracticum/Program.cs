using System;
using Application;

namespace GrosvenorInHousePracticum
{
    class Program
    {
        static void Main()
        {
            var server = new Server(new DishManager());
            while (true)
            {
                Console.WriteLine("Please provide your order:");
                var unparsedOrder = Console.ReadLine();
                var output = server.TakeOrder(unparsedOrder);
                Console.WriteLine(output);
            }
        }
    }
}
