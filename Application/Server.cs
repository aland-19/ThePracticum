using System;
using System.Collections.Generic;

namespace Application
{
    public class Server : IServer
    {

        private readonly IDishManager _dishManager;

        public Server(IDishManager dishManager)
        {
            _dishManager = dishManager;
        }
        
        public string TakeOrder(string unparsedOrder)
        {
            try
            {
                MenuItems order = ParseOrder(unparsedOrder);
                List<MenuItems> meals = _dishManager.GetMeals();
                string returnValue = FormatOutput(meals);
                return returnValue;
            }
            catch (ApplicationException)
            {
                return "error";
            }
        }

        public MenuItems ParseOrder(string unparsedOrder)
        {
            var returnValue = new MenuItems()
            {
                ItemNumber = new int()
            };

            var orderItems = unparsedOrder.Split(',');
            foreach (var orderItem in orderItems)
            {
                if (int.TryParse(orderItem, out int parsedOrder))
                {
                    
                }
                else
                {
                    throw new ApplicationException("Order needs to be comma separated list of numbers");
                }
            }

            var ItemNumber = unparsedOrder;
            foreach (var Number in ItemNumber)
            {
                if (int.TryParse(ItemNumber, out int OrderNumber))
                {
                    returnValue.ItemNumber.ToString();
                }
                else
                {
                    throw new ApplicationException("Meals need to be inputted by numbers.");
                }
            }
            
            return returnValue;
        }

        private string FormatOutput(List<MenuItems> dishes)
        {
            var returnValue = "";

            foreach (var dish in dishes)
            {
                returnValue = returnValue + string.Format(",{0}{1}", dish.DishName, GetMultiple(dish.Count));
            }

            if (returnValue.StartsWith(","))
            {
                returnValue = returnValue.TrimStart(',');
            }

            return returnValue;
        }

        private object GetMultiple(int count)
        {
            if (count > 1)
            {
                return string.Format("(x{0})", count);
            }
            return "";
        }
    }
}
