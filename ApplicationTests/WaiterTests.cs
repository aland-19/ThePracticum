using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using Application;
using NUnit.Framework;
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
        order.MealType = "alan"; // This does not need to match "morning" and "evening" below. If it doesn't the test will pass successfully.
        
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
    
    [Ignore("Not yet fully implemented")]
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
                DishName = "coffee"
            }
        };

        todaysMenu.Meals.Add("x", todaysMenuItems);

        var orderResult = _cut.Process(order, todaysMenu);
        
        orderResult.IsProcessable.ShouldBeTrue();
        orderResult.MealType.ShouldEqual("x");
        orderResult.InvalidReason.ShouldBeNull();
        
        //Not yet implemented in the code
        orderResult.OrderedItems.Count.ShouldEqual(1);
        orderResult.OrderedItems.First().DishName.ShouldEqual("coffee");
    }

    [Test]

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
        orderResult.OrderedItems.ShouldBeLessThanOrEqualTo<>(1);
        
    }

    /* [Test]
    
    // This case tests which menu items can be ordered more than once

     public void GivenAMenuItemThatAllowsMultipleOrders_WhenItIsProcessed_ThenReturnNoError()
    {
        var order = CreateOrder();
        order.IsValid = true;

        var todaysMenu = CreateMenu();
        var coffeeCanBeOrderedMoreThanOnce = new List<MenuItem>
        {
            new MenuItem()
            {
                DishName = "coffee",
                AllowsMany = true
            }
        };

        var potatoesCanBeOrderedMoreThanOnce = new List<MenuItem>
        {
            new MenuItem()
            {
                DishName = "potato",
                AllowsMany = true
            }
        };
        
        var orderResult = _cut.Process(order, todaysMenu);
        
        orderResult.IsProcessable.ShouldBeTrue();
        orderResult.InvalidReason.ShouldBeNull();
        
        // orderResult.OrderedItems.Count.ShouldEqual<>(1..10); // Coffee or potatoes can be ordered up to 10 times. 
        // orderResult.OrderedItems.First().DishName.ShouldEqual($"coffee(x{orderResult})"); // <- not to sure about this one.
    }
    */

    [Test]

    public void GivenAMealTypeThatBeginsWithUpperOrLowerCase_WhenItIsProcessed_ThenReturnNoError()
    {

        var order = CreateOrder();
        order.IsValid = true;
        order.MealType = "MORNING"; 
        
        var todaysMenu = CreateMenu();
        todaysMenu.Meals.Add("morning", new List<MenuItem>());
        todaysMenu.Meals.Add("Morning", new List<MenuItem>());
        todaysMenu.Meals.Add("evening", new List<MenuItem>());
        todaysMenu.Meals.Add("Evening", new List<MenuItem>());


        var orderResult = _cut.Process(order, todaysMenu);
        
        Assert.IsTrue(Waiter.Order.MealTypeValidator("morning"));
        Assert.IsTrue(Waiter.Order.MealTypeValidator("Morning"));
        Assert.IsTrue(Waiter.Order.MealTypeValidator("evening"));
        Assert.IsTrue(Waiter.Order.MealTypeValidator("Evening"));

        orderResult.IsProcessable.ShouldBeTrue();
        orderResult.InvalidReason.ShouldBeNull();


    }
    
    [Test]

    public void GivenAMealTypeThatBeginsWithUpperOrLowerCase_WhenItIsProcessed_ThenReturnNoErro()
    {

        var order = CreateOrder();
        order.IsValid = true;
        order.MealType = "MORNING";
       
        var todaysMenu = CreateMenu();
        todaysMenu.Meals.Add("morning", new List<MenuItem>());
        todaysMenu.Meals.Add("Morning", new List<MenuItem>());
        todaysMenu.Meals.Add("evening", new List<MenuItem>());
        todaysMenu.Meals.Add("Evening", new List<MenuItem>());
        
        var orderResult = _cut.Process(order, todaysMenu);
        
        var validator = new Waiter.MealTypeCaseValidator();

        Assert.IsTrue(validator.CaseValidator("morning"));
        Assert.IsTrue(validator.CaseValidator("Morning"));
        Assert.IsTrue(validator.CaseValidator("evening"));
        Assert.IsTrue(validator.CaseValidator("Evening"));

        orderResult.IsProcessable.ShouldBeTrue();
        orderResult.InvalidReason.ShouldBeNull();

    }
    
    
    
    

   /* public void GivenAMealTypeThatIsCaseInsensitive_WhenItIsProcessed_ThenReturnNoError()
    {
        var order = CreateOrder();
        order.IsValid = true;
        order.MealType = "morning";
        order.MealType = "evening";

        var caseInsensitveMealTypes = CaseInsensitive();
        
        var orderResult = _cut.Process(order, caseInsensitveMealTypes);
        
        orderResult.IsProcessable.ShouldBeTrue();
        caseInsensitveMealTypes.CaseInsensitiveMealType.ShouldEqual<>("morning");
        caseInsensitveMealTypes.CaseInsensitiveMealType.ShouldEqual<>("evening");
    }
    */
   
   
   


    private Menu CreateMenu()
    {
        var menu = new Menu();
        menu.Meals = new Dictionary<string, List<MenuItem>>();

        return menu;
    }
    
    
    /*  private Waiter.CaseInsensitive CaseInsensitive()
    {
        var caseInsensitiveMealType = new Waiter.CaseInsensitive();
        caseInsensitiveMealType.CaseInsensitiveMealType = true;

        return caseInsensitiveMealType;
        
        */

    private OrderParserResult CreateOrder()
    {
        return new OrderParserResult();
    }
}