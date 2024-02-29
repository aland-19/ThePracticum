using System.Collections.Generic;
using System.Linq;
using Application;
using NUnit.Framework;


namespace ApplicationTests
{
    [TestFixture]
    public class DishManagerTests
    {
        private DishManager _sut;

        [SetUp]
        public void DishManagerSetUp()
        {
            _sut = new DishManager();
        }

        [Test]
        public void EmptyListReturnsNull()
        {
            var order = new Order();
            var actual = _sut.GetMeals(order);
            Assert.AreEqual(0, actual.Count);
        }

        // Morning meals

        [Test]

        public void ListWith1ReturnsOneEgg()
        {
            var order = new MenuItems()
            {
                MealNumber = new List<int>()
                {
                    1
                }
            };

            var actual = _sut.GetMeals(order);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("egg" , actual.First().DishName);
            Assert.AreEqual("Egg" , actual.First().DishName);

        }
        
        [Test]
        
        public void ListWith2ReturnsOneToast()
        {
            var order = new Order
            {
                Dishes = new List<int>()
                {
                    2
                }
            };

            var actual = _sut.GetMeals(order);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("toast" , actual.First().DishName);
            Assert.AreEqual("Toast" , actual.First().DishName);
        }
        
        [Test]
        
        public void ListWith3ReturnsOneCoffee()
        {
            var order = new Order
            {
                Dishes = new List<int>()
                {
                    3
                }
            };

            var actual = _sut.GetMeals(order);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("coffee" , actual.First().DishName);
            Assert.AreEqual("Coffee" , actual.First().DishName);
        }
        
        // Evening meals
        
        [Test]
        public void ListWith1ReturnsOneSteak()
        {
            var order = new Order
            {
                Dishes = new List<int>
                {
                    1
                }
            };

            var actual = _sut.GetMeals(order);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("steak", actual.First().DishName);
            Assert.AreEqual("Steak", actual.First().DishName);
        }

        [Test]

        public void ListWith2ReturnsOnePotato()
        {
            var order = new Order
            {
                Dishes = new List<int>
                {
                    2
                }
            };

            var actual = _sut.GetMeals(order);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("potato", actual.First().DishName);
            Assert.AreEqual("Potato", actual.First().DishName);
        }

        [Test]

        public void ListWith3ReturnsOneWine()
        {
            var order = new Order
            {
                Dishes = new List<int>
                {
                    3
                }
            };

            var actual = _sut.GetMeals(order);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("wine", actual.First().DishName);
            Assert.AreEqual("Wine", actual.First().DishName);
        }

        [Test]

        public void ListWith4ReturnsOneCake()
        {
            var order = new Order
            {
                Dishes = new List<int>
                {
                    4
                }
            };

            var actual = _sut.GetMeals(order);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("cake", actual.First().DishName);
            Assert.AreEqual("Cake", actual.First().DishName);
        }
    }
}
