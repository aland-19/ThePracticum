using System.Collections.Generic;

namespace Application;

public class Menu
{
    public Dictionary<string, List<MenuItems>> Meals;
}

public class MenuItems
{
    public string DishName { get; set; }
    public bool AllowsMany { get; set; }
    public int ItemNumber { get; set; }
    public int Count { get; set; }

    public List<int> MealNumber;

}