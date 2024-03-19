using System.Collections.Generic;

namespace Application;

public class Menu
{
    public Dictionary<string, List<MenuItem>> Meals;
    
}

public class MenuItem
{
    public string DishName { get; set; }
    public bool AllowsMany { get; set; }
    public int ItemNumber { get; set; }
}