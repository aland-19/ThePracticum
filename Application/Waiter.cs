using System;
using System.Collections.Generic;
using System.Linq;

namespace Application;

public class Waiter
{
    public Order Process(OrderParserResult input, Menu todaysMenu)
    {
        if (input.IsValid == false)
        {
            return new Order
            {
                IsProcessable = false,
                InvalidReason = "This order is not processable."
            };
        }



        //Validate that the provided mealType is on today's menu
        if (!todaysMenu.Meals.ContainsKey(input.MealType))
        {
            return new Order
            {
                IsProcessable = false,
                InvalidReason = $"The meal type {input.MealType} you ordered is not on today's menu"
            };
        }

        //What do we know?
        // That the meal type the user is ordering is in the menu!
        // What can we do?  We can get the menu items for the meal type!
        var menuItemsThatAreValidForCurrentForMealType = todaysMenu.Meals[input.MealType];

        //Iterate through each ordered item
        foreach (var orderedDish in input.Dishes)
        {
            if (!menuItemsThatAreValidForCurrentForMealType.Any(x => x.DishName == orderedDish))
            {
                return new Order
                {
                    IsProcessable = false,
                    InvalidReason = $"The dish item {orderedDish} is not on the menu."
                };
            }
        }

        return new Order
        {
            IsProcessable = true,
            MealType = input.MealType
        };

    }
}

public class MealTypeValidator
{
    public bool IsValid(IEnumerable<string> allMealTypes, string providedMealType)
    {
        foreach (var mealType in allMealTypes)
        {
            if (string.Equals(mealType, providedMealType, StringComparison.OrdinalIgnoreCase))
            {
                if (IsLowerCase(providedMealType))
                {
                    return true;
                }

                if (IsProperCase(providedMealType))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsLowerCase(string userInput)
    {
        return userInput == userInput.ToLower();
    }

    private bool IsProperCase(string userInput)
    {
        return char.IsUpper(userInput[0]) && userInput.Substring(1).ToLower() == userInput.Substring(1);
    }
}

public class MenuItemsThatDontTakeMultipleOrders
{
    public OrderParserResult Process(Order order, List<MenuItem> menu)
    {
        var orderResult = new OrderParserResult();
        orderResult.OrderedItems = new List<MenuItem>();

        foreach (var dishName in order.Dishes)
        {
            var menuItem = menu.FirstOrDefault(menuItem => menuItem.AllowsMany);
            if (menuItem.AllowsMany == false && order.Dishes.Count(dish => dish == dishName) > 1) 
            {
                orderResult.IsValid = false;
                orderResult.InvalidReason = "This dish cannot be ordered more than once:" + dishName;
                return orderResult;
            }
            orderResult.OrderedItems.Add(menuItem);
        }
        
        orderResult.IsValid = true;
        orderResult.IsProcessable = orderResult.OrderedItems.Count <= 1;
        return orderResult;
    }

}

public class OrderIsValidOrNot
{
    public OrderParserResult Process (Order order, Menu todaysMenu)
    {
        if (!order.IsValid)
        {
            return new OrderParserResult
            {
                IsProcessable = false,
                IsValid = false,
                InvalidReason = "Order is not valid"

            };
        }

        var validOrder = new OrderParserResult()
        {
            IsProcessable = true,
            IsValid = true,
            InvalidReason = null

        };
        
        if (todaysMenu.Meals.ContainsKey(order.MealType))
        {
            foreach (var dish in order.Dishes)
            {
                var menuItem = todaysMenu.Meals[order.MealType].FirstOrDefault(newMenuItem => newMenuItem.DishName == dish);
                if (menuItem == null)
                {
                    validOrder.OrderedItems.Add(menuItem);
                    
                }
            }
        }

        return validOrder;
    }

}

public class Order
    {
        public bool IsValid { get; set; }
        public bool IsProcessable { get; set; }
        public string InvalidReason { get; set; }
        public string MealType { get; set; }
        public List<MenuItem> OrderedItems { get; set; }
        public static bool MealTypeValidator(string userInput)
        {
            string[] validMealTypes = { "morning","Morning","evening","Evening"};
            return Array.Exists(validMealTypes, input => input.Equals(userInput, StringComparison.OrdinalIgnoreCase));
        }
        
        public List<string> Dishes { get; set; }
    }
    