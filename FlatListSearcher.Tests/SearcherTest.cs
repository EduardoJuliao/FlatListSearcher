using System.Collections.Generic;
using System.Linq;
using FlatListSearcher;
using NUnit.Framework;

namespace Tests
{
    public class SearcherTest
    {
        private List<TestData> _data;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _data = new List<TestData>
            {
                new TestData {Id=1,Level=0},
                new TestData {Id=2,Level=1},
                new TestData {Id=3,Level=2},
                new TestData {Id=4,Level=3},
                new TestData {Id=5,Level=0},
                new TestData {Id=6,Level=1},
                new TestData {Id=7,Level=1},
                new TestData {Id=8,Level=2},
                new TestData {Id=9,Level=3},
                new TestData {Id=10,Level=3},
                new TestData {Id=11,Level=0},
                new TestData {Id=12,Level=1},
                new TestData {Id=13,Level=1},
                new TestData {Id=14,Level=2},
                new TestData {Id=15,Level=2},
            };
        }

        [TestCase(1, 3)]
        [TestCase(5, 5)]
        [TestCase(8, 2)]
        [TestCase(11, 4)]
        [TestCase(12, 0)]
        public void FindAllChildren(int id, int expectedChildrenCount)
        {
            // Arrange

            // Act
            var result = _data.FindChildren(x => x.Id == id);

            // Assert
            Assert.AreEqual(expectedChildrenCount, result.Count());
        }

        [TestCase(1, 1)]
        [TestCase(5, 2)]
        [TestCase(8, 2)]
        [TestCase(11, 2)]
        [TestCase(12, 0)]
        public void FindFirstLevelChildren(int id, int expectedChildrenCount)
        {
            // Arrange

            // Act
            var result = _data.FindFirstLevelChildren(x => x.Id == id);

            // Assert
            Assert.AreEqual(expectedChildrenCount, result.Count());
        }

        [TestCase(4, 1)]
        [TestCase(10, 5)]
        [TestCase(15, 11)]
        [TestCase(8, 5)]
        [TestCase(3, 1)]
        public void FindRoot(int id, int expectedId)
        {
            // Arrange

            // Act
            var result = _data.FindRoot(x => x.Id == id);

            // Assert
            Assert.AreEqual(expectedId, result.Id);
        }

        [TestCase(1, null)]
        [TestCase(5, null)]
        [TestCase(11, null)]
        [TestCase(8, 7)]
        [TestCase(3, 2)]
        [TestCase(15, 13)]
        public void FindParent(int id, int? expectedId)
        {
            // Arrange

            // Act
            var result = _data.FindParent(x => x.Id == id);

            // Assert
            if (expectedId.HasValue)
                Assert.AreEqual(expectedId.Value, result.Id);
            else
                Assert.IsNull(result);
        }
    }

    class TestData : ILevel
    {
        public int Level { get; set; }
        public int Id { get; set; }
    }
}