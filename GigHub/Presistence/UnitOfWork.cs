﻿using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Presistence.Repositories;

namespace GigHub.Presistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepositroy Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowingRepository Follows { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IUserNotificationRepository UserNotifications { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepositroy(context);
            Attendances = new AttendanceRepository(context);
            Follows = new FollowingRepository(context);
            Genres = new GenreRepository(context);
            Notifications = new NotificationRepository(context);
            UserNotifications = new UserNotificationRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}