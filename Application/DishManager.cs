using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class DishManager : IDishManager
    {
        private IDishManager _dishManagerImplementation;

        /// <summary>
        /// Takes an Order object, sorts the orders and builds a list of dishes to be returned. 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<MenuItems> GetMeals(Order order)
        {
            var returnValue = new List<MenuItems>();
            order.Dishes.Sort();
            foreach (var dishType in order.Dishes)
            {
                AddOrderToList(dishType, returnValue);
            }

            return returnValue;
        }

        public List<MenuItems> GetMeals(MenuItems menuItems)
        {
            throw new NotImplementedException();
        }

        public List<MenuItems> GetMeals()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Takes an int, representing an order type, tries to find it in the list.
        /// If the dish type does not exist, add it and set count to 1
        /// If the type exists, check if multiples are allowed and increment that instances count by one
        /// else throw error
        /// </summary>
        /// <param name="order">int, represents a dishtype</param>
        /// <param name="returnValue">a list of dishes, - get appended to or changed </param>
        private void AddOrderToList(int order, List<MenuItems> returnValue)
        {
            string morningOrderName = GetMorningOrderName(order);
            string eveningOrderName = GetEveningOrderName(order);
            var existingOrder = returnValue.SingleOrDefault(x => x.DishName == morningOrderName);
            if (existingOrder == null)
            {
                returnValue.Add(new MenuItems()
                {
                    DishName = morningOrderName,
                    Count = 1
                });
            }
            else if (IsMultipleAllowed(order))
            {
                existingOrder.Count++;
            }
            else
            {
                throw new ApplicationException(string.Format("Multiple {0}(s) not allowed", morningOrderName));
            }
        }

        private string GetEveningOrderName(int order)
        {
            switch (order)
            {
                case 1:
                    return "steak";
                case 2:
                    return "potato";
                case 3:
                    return "wine";
                case 4:
                    return "cake";
                default:
                    throw new ApplicationException("Order does not exist");

            }
        }

        private string GetMorningOrderName(int order)
        {
            switch (order)
            {
                case 1:
                    return "egg";
                case 2:
                    return "coffee";
                case 3:
                    return "toast";
            }

            return null;
        }

    private bool IsMultipleAllowed(int order)
        {
            switch (order)
            {
                case 2:
                    return true;
                default:
                    return false;

            }
        }
    }
}