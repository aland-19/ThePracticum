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

        //var CaseInsensitiveMealType = string.IndexOf("morning", StringComparsion.OrdinalIgnoreCase);

    }
    
    public class MealTypeCaseValidator
    {
        public List<string> mealTypes;
    
        public List<string> GetValidMealTypes()
        {
            List<string> validMealTypes = new List<string>();
            foreach (string mealType in mealTypes)
            { 
                if (IsValid(mealType))
                {
                    validMealTypes.Add(mealType);
                }
            
            }

            return validMealTypes;
        }
        
        public bool IsValid(string mealType)
        {
            // Lower case
            string lowerCaseMealType = mealType.ToLower();
            return lowerCaseMealType == "morning" || lowerCaseMealType == "evening";
            
            // Upper case
            string upperCaseMealType = mealType.ToUpper();
            return upperCaseMealType == "Morning" || upperCaseMealType == "Evening";
            
        }
    }
    
    public class Order
    {
        public bool IsProcessable { get; set; }
        public string InvalidReason { get; set; }
        public string MealType { get; set; }
        public List<MenuItem> OrderedItems { get; set; }
        public static bool MealTypeValidator(string userInput)
        {
            string[] validMealTypes = { "morning","Morning","evening","Evening"};
            return Array.Exists(validMealTypes, input => input.Equals(userInput, StringComparison.OrdinalIgnoreCase));
        }

    }

  /*  public class MealTypeCaseValidator
    {
        public bool CaseValidator(string userInput)
        {
            string[] validMealTypes = { "morning","Morning","evening","Evening"};
            return Array.Exists(validMealTypes, input => input.Equals(userInput, StringComparison.OrdinalIgnoreCase));
        }
    }
    */
}



/* public bool CaseInsensitive(string caseInsensitiveMealType)
{
    return caseInsensitiveMealType.IndexOf("morning", StringComparison.OrdinalIgnoreCase) >= 0;
    return caseInsensitiveMealType.IndexOf("evening", StringComparison.OrdinalIgnoreCase) >= 0;
}
*/