using GigHub.Core.Models;
using GigHub.Presistence;
using GigHub.Presistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace GigHub.Test.Presistance.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        private NotificationRepository _repository;
        private Mock<DbSet<Notification>> _mockNotifications;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockNotifications = new Mock<DbSet<Notification>>();
            var mockContext = new Mock<IApplicationDbContext>();

            mockContext.SetupGet(c => c.Notifications).Returns(_mockNotifications.Object);

            _repository = new NotificationRepository(mockContext.Object);
        }

        // Write unit test for NotificationRepository.GetNewNotificationsFor()
    }
}
