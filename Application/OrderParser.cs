using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class OrderParser
    {
        public OrderParserResult Parse(string order)
        {
            if (string.IsNullOrEmpty(order))
            {
                return new OrderParserResult
                {
                    IsValid = false,
                    InvalidReason = "Order cannot be null"
                };
            }

            // Find the space between the meal type and the dishes
            
            var firstSpace = order.IndexOf(" ", StringComparison.InvariantCulture);

            if (firstSpace == -1)
            {
                return new OrderParserResult
                {
                    IsValid = false,
                    InvalidReason = "MealType and Dishes do not have a space"
                };    
            }
            

            var dishes = order.Substring(firstSpace);
            var dishesList = dishes.Split(",", StringSplitOptions.TrimEntries);
            
            return new OrderParserResult
            {
                IsValid = true,
                MealType = order.Substring(0, firstSpace),
                Dishes = dishesList.Where(x => x.Length > 0)
            };
        }
    }

    public class OrderParserResult
    {
        public List<MenuItem> OrderedItems { get; set; }
        public bool IsValid { get; set; }
        public string InvalidReason { get; set; }
        public string MealType { get; set; }
        public IEnumerable<string> Dishes { get; set; }
        
        public bool IsProcessable { get; set; }
    }
}