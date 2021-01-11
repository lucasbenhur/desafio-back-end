using System.Collections.Generic;
using DesafioConexa.Api.Controllers;
using DesafioConexa.Api.Interfaces;
using DesafioConexa.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DesafioConexa.Tests
{
    [TestFixture]
    public class PlayListControllerTests
    {
        private PlayListController _playListController { get; set; }
        private PlayList _playList;

        [SetUp]
        public void Setup()
        {
            _playList = new PlayList
            {
                TrackList = new List<Track>()
                {
                    new Track("Track 1"),
                    new Track("Track 2"),
                    new Track("Track 3"),
                    new Track("Track 4"),
                    new Track("Track 5")
                }
            };

            var mockPlayListService = new Mock<IPlayListService>();
            mockPlayListService.Setup(m => m.GetPlayList(It.IsAny<City>())).Returns(_playList);

            var mockLogger = new Mock<ILogger<PlayListController>>();

            _playListController = new PlayListController(mockLogger.Object, mockPlayListService.Object);
        }

        [Test]
        public void GetPlayList_CityNameNotNullOrEmpty_ReturnsPlayList()
        {
            var result = _playListController.Get("teste", null, null);

            Assert.AreEqual(result.Value, _playList);
        }

        [Test]
        public void GetPlayList_LatitudeAndLongitudeHasValue_ReturnsPlayList()
        {
            var result = _playListController.Get(string.Empty, 1f, 1f);

            Assert.AreEqual(result.Value, _playList);
        }

        [Test]
        public void GetPlayList_AllParametersNull_ReturnsBadRequest()
        {
            var result = _playListController.Get(string.Empty, null, null);

            Assert.That(result.Result, Is.InstanceOf<BadRequestResult>());
        }
    }
}