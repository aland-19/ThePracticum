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

    [TearDown]

    public void TearDown()
    {

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

    public void ServeEgg()
    {
        var order = "1";
        string expected = "Egg";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    public void ServeToast()
    {
        var order = "2";
        string expected = "Toast";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    public void ServeCoffee()
    {
        var order = "3";
        string expected = "Coffee";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Dinner meals

    public void ServeSteak()
    {
        var order = "1";
        string expected = "Steak";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    public void ServePotatoes()
    {
        var order = "2";
        string expected = "Potato";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    public void ServeWine()
    {
        var order = "3";
        string expected = "Wine";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    public void ServeCake()
    {
        var order = "4";
        string expected = "Cake";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Incorrect output

    public void IncorrectOutput()
    {
        var order = "One";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Multiple Servings

    public void CanServeMultiplePotatoes()
    {
        var order = "2,2";
        string expected = "Potato(x2)";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);

    }

    public void CanServeMultipleCoffees()
    {
        var order = "3,3";
        string expected = "Coffee(x2)";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Incorrect multiple servings in the morning

    public void CantServeMultipleEggs()
    {
        var order = "1,1";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    public void CantServeMultipleToasts()
    {
        var order = "2,2";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Incorrect multiple servings in the evening

    public void CantServeMultipleSteaks()
    {
        var order = "1,1";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    public void CantServeMultipleWines()
    {
        var order = "3,3";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    public void CantServeMultipleCakes()
    {
        var order = "4,4";
        string expected = "Error";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Random meal combinations

    public void CanServeEggToastCoffee()
    {
        var order = "1,2,3";
        string expected = "Egg,toast,coffee";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    public void CanServeSteakPotatoWineCake()
    {
        var order = "1,2,3,4";
        string expected = "Steak,potato,wine,cake";
        var actual = _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    // Not on the menu

    [Test]
    
    public void ExistentMenuItems()
    {
        var order = new Order();

        ServeEgg();
        ServeToast();
        ServeCoffee();
        ServeSteak();
        ServePotatoes();
        ServeWine();
        ServeCake();
    }

    public void NonExistentMenuItems()
    {
        
    }
    
    
}
