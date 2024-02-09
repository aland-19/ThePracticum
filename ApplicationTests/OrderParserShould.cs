using System.Linq;
using Application;
using NUnit.Framework;
using Should;

namespace ApplicationTests
{
    [TestFixture]
    public class OrderParserShould
    {
        private OrderParser orderParser = new OrderParser();
        
        [Test]
        public void NotAllowEmptyString()
        {
            var result = orderParser.Parse(string.Empty);
            result.IsValid.ShouldBeFalse();
        }
        
        [Test]
        public void NotAllowNull()
        {
            var result = orderParser.Parse(null);
            result.IsValid.ShouldBeFalse();
        }
        
        [Test]
        public void NotAllowAnOrderWithJustAMealTypeAndNotDishes()
        {
            var result = orderParser.Parse("abc");
            result.IsValid.ShouldBeFalse();
        }
        
        [TestCase("dinner 1", 1)]
        [TestCase("breakfast 1,2,2", 3)]
        [TestCase("breakfast 1,2,24  ,    ", 3)]
        public void AllowsOrderThatIsCorrectlyFormatted(string order, int itemsOrdered)
        {
            var result = orderParser.Parse(order);
            result.IsValid.ShouldBeTrue();
            result.Dishes.Count().ShouldEqual(itemsOrdered);
        }
    }
}