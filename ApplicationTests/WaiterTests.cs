using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using Application;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Should;

namespace ApplicationTests;

[TestFixture]
public class WaiterTests
{
    private Waiter _cut = new Waiter();
    
    
    [Test]
    public void GivenAnInvalidOrder_WhenItIsProcessed_ThenReturnError()
    {
        //Arrange
        var order = CreateOrder();
        order.IsValid = false;
        
        //Act
        var orderResult = _cut.Process(order, (Menu)null);
        
        //Asset
        orderResult.IsProcessable.ShouldBeFalse();
        orderResult.InvalidReason.ShouldNotBeEmpty();
    }

    [Test]
    public void GivenAnInvalidMealType_WhenItIsProcessed_ThenReturnError()
    {
        //Arrange
        //Note to Alan
        // This was the original order "corvette brakes, brakes, steering wheel"
        
        var order = CreateOrder();
        order.IsValid = true;
        order.MealType = "hi"; // This does not need to match "morning" and "evening" below. If it doesn't the test will pass successfully.
        
        var todaysMenu = CreateMenu();
        todaysMenu.Meals.Add("Morning", new List<MenuItem>());
        todaysMenu.Meals.Add("Evening", new List<MenuItem>());
        
        
        //Act
        var orderResult = _cut.Process(order, todaysMenu);
        
        //Assert
        orderResult.IsProcessable.ShouldBeFalse();
        orderResult.InvalidReason.ShouldNotBeEmpty();
        orderResult.InvalidReason.ShouldEqual($"The meal type {order.MealType} you ordered is not on today's menu");
    }

    [Test]
    public void GivenAnInvalidMenuItem_WhenItIsProcessed_ThenReturnError()
    {
        var order = CreateOrder();
        order.IsValid = true;
        order.MealType = "x";
        order.Dishes = new List<string> { "coffee" };
        
        var todaysMenu = CreateMenu();
        todaysMenu.Meals.Add("x", new List<MenuItem>());
                
                // Meal type ^
        
        var orderResult = _cut.Process(order, todaysMenu);
        
        orderResult.IsProcessable.ShouldBeFalse();
        orderResult.InvalidReason.ShouldNotBeEmpty();
        orderResult.InvalidReason.ShouldEqual($"The dish item {order.Dishes.First()} is not on the menu.");
    }

    [Test]
    public void GivenAValidOrder_WhenItIsProcessed_ThenReturnNoError()
    
    {
        var order = CreateOrder();
        order.IsValid = true;
        order.MealType = "x";
        order.Dishes = new List<string> { "coffee" };

        var todaysMenu = CreateMenu();
        var todaysMenuItems = new List<MenuItem>
        {
            new MenuItem
            {
                DishName = "egg",
                AllowsMany = false,
            },
            new MenuItem
            {
                DishName = "toast",
                AllowsMany = false,
            },
            new MenuItem
            {
                DishName = "coffee",
                AllowsMany = true
            },
            new MenuItem
            {
                DishName = "steak",
                AllowsMany = false
            },
            new MenuItem
            {
                DishName = "potato",
                AllowsMany = true,
            },
            new MenuItem
            {
                DishName = "wine",
                AllowsMany = false
            },
            new MenuItem
            {
                DishName = "cake",
                AllowsMany = false
            }
        };
        
        
        todaysMenu.Meals.Add("x", todaysMenuItems);

        var orderResult = _cut.Process(order, todaysMenu);
        
        orderResult.IsProcessable.ShouldBeTrue();
        orderResult.MealType.ShouldEqual("x");
        orderResult.InvalidReason.ShouldBeNull();
        
        
        orderResult.DishCount.ShouldBeLessThanOrEqualTo(1);
    }

    
    public class MultipleOrdersTests
    {
        public MenuItemsThatDontTakeMultipleOrders _validator = new MenuItemsThatDontTakeMultipleOrders();

        [TestCase("morning", "1,1", false)]
        [TestCase("morning", "2,2", false)]
        [TestCase("morning", "3,3", true)]
        [TestCase("evening", "1,1", false)]
        [TestCase("evening", "2,2", true)]
        [TestCase("evening", "3,3", false)]
        [TestCase("evening", "4,4", false)]
        
        public void GivenAMenuItemThatDoesNotAllowMultipleOrders_WhenItIsProcessed_ThenReturnError(string mealTypesFromMenu, List<MenuItem> menuItems, bool isValid )
        {
          var anyMealType = mealTypesFromMenu.Split(",");
          _validator.IsValid(menuItems).ShouldEqual(isValid);

        }
        
    }
    
    /*
    public void GivenAMenuItemThatDoesNotAllowMultipleOrders_WhenItIsProcessed_ThenReturnError()
    {
        var order = CreateOrder();
        order.IsValid = true;
        order.MealType = "morning";
        order.MealType = "evening";
        order.Dishes = new List<string> { "egg", "toast", "steak", "wine" , "cake"};

        var todaysMenu = CreateMenu();
        var orderResult = _cut.Process(order, todaysMenu);

        foreach (var dishName in order.Dishes)
        {
            var menuItem = todaysMenu.Meals;
            Assert.IsTrue(menuItem.Count <= 1);
        }
        
        orderResult.IsProcessable.ShouldBeFalse();
        orderResult.DishCount.ShouldBeLessThanOrEqualTo(1);
        


    }
    
    */
    
    private Menu CreateMenu()
    {
        var menu = new Menu();
        menu.Meals = new Dictionary<string, List<MenuItem>>();

        return menu;
    }
    

    private OrderParserResult CreateOrder()
    {
        return new OrderParserResult();
    }
}