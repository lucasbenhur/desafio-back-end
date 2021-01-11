using DesafioConexa.Api.Enums;
using DesafioConexa.Api.Interfaces;
using DesafioConexa.Api.Services;
using NUnit.Framework;

namespace DesafioConexa.Tests
{
    [TestFixture]
    class PlayListServiceTests
    {
        private IPlayListService _playListService;

        [SetUp]
        public void Setup()
        {
            _playListService = new PlayListService(null, null);
        }

        [TestCase(45f, Category.PARTY)]
        [TestCase(31f, Category.PARTY)]
        [TestCase(30.01f, Category.PARTY)]
        [TestCase(30f, Category.POP)]
        [TestCase(22f, Category.POP)]
        [TestCase(15f, Category.POP)]
        [TestCase(14f, Category.ROCK)]
        [TestCase(12f, Category.ROCK)]
        [TestCase(10f, Category.ROCK)]
        [TestCase(9.99f, Category.CLASSICAL)]
        [TestCase(0f, Category.CLASSICAL)]
        [TestCase(-10f, Category.CLASSICAL)]
        public void GetPlayListCategory(float temp, Category categoryExpected)
        {
            var category = _playListService.GetPlayListCategory(temp);

            Assert.AreEqual(categoryExpected, category);
        }
    }
}
