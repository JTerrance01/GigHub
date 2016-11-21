using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Test.Contollers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto { GigId = 2 });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void Attend_AttendanceForUserAlreadyExists_ShouldReturnBadRequest()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendance(2, _userId)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto { GigId = 2 });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void DeleteAttenance_ValidRequest_ShouldReturnOkResult()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendance(2, _userId)).Returns(attendance);

            var result = _controller.DeleteAttendance(2);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }

        [TestMethod]
        public void DeleteAttendance_VaildRequest_ShouldReturnOfDeleteAttendance()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendance(2, _userId)).Returns(attendance);

            var result = (OkNegotiatedContentResult<int>)_controller.DeleteAttendance(2);

            result.Content.Should().Be(2);
        }

        [TestMethod]
        public void DeleteAttendance_AttendanceDoesNotExist_ShouldReturnNotFoundResult()
        {
            var result = _controller.DeleteAttendance(2);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
