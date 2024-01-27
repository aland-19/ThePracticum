using System.Linq;
using Application;
using NUnit.Framework;
using Should;

namespace ApplicationTests
{
    [TestFixture]
    public class OrderParserShould
    {
        private OrderParser _cut = new OrderParser();
        
        [Test]
        public void NotAllowEmptyString()
        {
            var result = _cut.Parse(string.Empty);
            result.IsValid.ShouldBeFalse();
        }
        
        [Test]
        public void NotAllowNull()
        {
            var result = _cut.Parse(null);
            result.IsValid.ShouldBeFalse();
        }
        
        [Test]
        public void NotAllowAnOrderWithJustAMealTypeAndNotDishes()
        {
            var result = _cut.Parse("abc");
            result.IsValid.ShouldBeFalse();
        }
        
        [TestCase("dinner 1", 1)]
        [TestCase("breakfast 1,2,2", 3)]
        [TestCase("breakfast 1,2,24  ,    ", 3)]
        public void AllowsOrderThatIsCorrectlyFormatted(string order, int dishCount)
        {
            var result = _cut.Parse(order);
            result.IsValid.ShouldBeTrue();
            result.Dishes.Count().ShouldEqual(dishCount);
        }
    }
}