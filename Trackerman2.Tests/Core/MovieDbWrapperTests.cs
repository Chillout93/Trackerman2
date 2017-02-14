using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using Trackerman2.Core;
using Trackerman2.DomainModels;

namespace Trackerman2.Tests.Core
{
    [TestFixture]
    public class MovieDbWrapperTests
    {
        private MovieDbWrapper movieDbWrapper;
        private Mock<ApplicationDbContext> mockContext;
        private Mock<IRestClient> mockRestClient;
        private Show show;

        [SetUp]
        public void SetUp()
        {
            show = new Show() { ID = 1, BackdropPath = "aaa", FirstAirDate = DateTime.Now.ToString(), Name = "test", Overview = "test", Popularity = 8.0, PosterPath = "/test", VoteAverage = 8.0, VoteCount = 100 };
            mockRestClient = new Mock<IRestClient>();
            mockContext = new Mock<ApplicationDbContext>();
            var restResponse = new RestResponse() { Content = JsonConvert.SerializeObject(show), StatusCode = System.Net.HttpStatusCode.OK };
            mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(restResponse);

            movieDbWrapper = new MovieDbWrapper(mockContext.Object, mockRestClient.Object);
        }

        [Test]
        public void FindTvShow_WithCorrectID_ReturnsPopulatedShowObject()
        {
            var jsonShow = movieDbWrapper.FindShow(1);
            jsonShow.ShouldBeEquivalentTo(show);
        }

        [Test]
        public void FindTvShow_WithIncorrectID_ReturnsNull()
        {
            var jsonShow = movieDbWrapper.FindShow(2);
            jsonShow.Should().BeNull();
        }

    }
}
