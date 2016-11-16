﻿using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Follows { get; }
        IGenreRepository Genres { get; }
        IGigRepositroy Gigs { get; }

        void Complete();
    }
}