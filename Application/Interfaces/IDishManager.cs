using System.Collections.Generic;

namespace Application
{

    public interface IDishManager
    {
        /// <summary>
        /// Constructs a list of dishes, each dish with a name and a count
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        List<MenuItem> GetMeals(MenuItem order);
        List<MenuItem> GetMeals();
    }
}