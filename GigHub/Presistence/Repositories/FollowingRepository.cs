using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core;

namespace GigHub.Presistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly IApplicationDbContext _context;

        public FollowingRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Following> GetFollowing(string userId, string artistId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId && f.FolloweeId == artistId);
        }
    }
}