using System;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using Application;
using NUnit.Framework;

namespace ApplicationTests;

public class WaiterTests
{
    private Server _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new Server(new DishManager());
    }

    // Switch between morning meals and evening meals

    [Test]

    public void ServingMorningMeals()
    {
        var order = "Morning";
        var caseInsensitiveOrder = "morning";
        string expected = "Morning";
        string caseInsentitiveExpected = "morning";
        var actual = _sut.TakeOrder(order);
        var alsoActual = _sut.TakeOrder(caseInsensitiveOrder);
        Assert.AreEqual(expected, actual);
        Assert.AreEqual(caseInsentitiveExpected, alsoActual);
    }
    
    [Test]
    public void ServeEveningMeals()
    {
        var order = "Evening";
        var caseInsensitiveOrder = "evening";
        string expected = "Evening";
        string caseInsensitiveExpected = "evening";
        var actual = _sut.TakeOrder(order);
        var alsoActual = _sut.TakeOrder(caseInsensitiveOrder);
        Assert.AreEqual(expected, actual);
        Assert.AreEqual(caseInsensitiveExpected, alsoActual);
    }


    // Morning meals

    [Test]
    
    public void ServeEgg()
    {
        var order = "1";
        string expected = "Egg";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    
    public void ServeToast()
    {
        var order = "2";
        string expected = "Toast";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    
    public void ServeCoffee()
    {
        var order = "3";
        string expected = "Coffee";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Dinner meals
    
    [Test]

    public void ServeSteak()
    {
        var order = "1";
        string expected = "Steak";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]

    public void ServePotatoes()
    {
        var order = "2";
        string expected = "Potato";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    
    public void ServeWine()
    {
        var order = "3";
        string expected = "Wine";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    
    public void ServeCake()
    {
        var order = "4";
        string expected = "Cake";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Incorrect output
    
    
    [Test]

    public void IncorrectOutputOne()
    {
        var order = "One";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    
    public void IncorrectOutputTwo()
    {
        var order = "Two";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    
    public void IncorrectOutputThree()
    {
        var order = "Three";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    
    public void IncorrectOutputFour()
    {
        var order = "Four";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }
    
    
    // Multiple Servings
    
    [Test]

    public void CanServeMultiplePotatoes()
    {
        var order = "2,2";
        string expected = "Potato(x2)";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);

    }
    
    [Test]

    public void CanServeMultipleCoffees()
    {
        var order = "3,3";
        string expected = "Coffee(x2)";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Incorrect multiple servings in the morning

    
    [Test]
    
    public void CantServeMultipleEggs()
    {
        var order = "1,1";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    
    public void CantServeMultipleToasts()
    {
        var order = "2,2";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Incorrect multiple servings in the evening

    [Test]
    
    public void CantServeMultipleSteaks()
    {
        var order = "1,1";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    
    public void CantServeMultipleWines()
    {
        var order = "3,3";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }
    
    
    [Test]

    public void CantServeMultipleCakes()
    {
        var order = "4,4";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Random meal combinations
    
    [Test]

    public void CanServeEggToastCoffee()
    {
        var order = "1,2,3";
        string expected = "Egg,toast,coffee";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]

    public void CanServeSteakPotatoWineCake()
    {
        var order = "1,2,3,4";
        string expected = "Steak,potato,wine,cake";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    [Test]

    public void AllowedMeals()

    {
        MealValidator validator = new MealValidator();
        
        // Morning Meals

        bool dish1 = validator.IsFoodAllowed(1); // Egg
        bool dish2 = validator.IsFoodAllowed(2); // Toast
        bool dish3 = validator.IsFoodAllowed(3); // Coffee
        
        // Evening Meals

        bool dish4 = validator.IsFoodAllowed(1); // Steak
        bool dish5 = validator.IsFoodAllowed(2); // Potato
        bool dish6 = validator.IsFoodAllowed(3); // Wine
        bool dish7 = validator.IsFoodAllowed(4); // Cake

        Assert.IsTrue(dish1);
        Assert.IsTrue(dish2);
        Assert.IsTrue(dish3);
        Assert.IsTrue(dish4);
        Assert.IsTrue(dish5);
        Assert.IsTrue(dish6);
        Assert.IsTrue(dish7);
    }

}

public class MealValidator
{
    public bool IsFoodAllowed(int dish)
    {
        throw new NotImplementedException();
    }
}
