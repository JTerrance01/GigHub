﻿using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Presistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                           .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                           .ToList();
        }
        public IEnumerable<Attendance> GetAttendance(int gigId, string userId)
        {
            return _context.Attendances
                .Where(a => a.GigId == gigId && a.AttendeeId == userId);
        }
    }
}