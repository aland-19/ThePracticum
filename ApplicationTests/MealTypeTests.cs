using Application;
using NUnit.Framework;
using Should;

namespace ApplicationTests;

public class MealTypeTests
{
    private MealTypeValidator _mealTypeValidator = new MealTypeValidator();

    [TestCase("morning,evening", "morning", true)]
    [TestCase("morning,evening", "Morning", true)]
    [TestCase("morning,evening", "MORNING", false)]
    public void GivenAValidMealTypeThatBeginsWithProperOrLowerCase_WhenItIsProcessed_ThenCorrectlyValidates(string mealTypesFromMenu, string userProvidedMealType, bool isValid)
    {
        var mealTypes = mealTypesFromMenu.Split(",");
        _mealTypeValidator.IsValid(mealTypes, userProvidedMealType).ShouldEqual(isValid);
    }
}