using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Presistence.EntityConfigurations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(f => new { f.FollowerId, f.FolloweeId });
        }
    }
}