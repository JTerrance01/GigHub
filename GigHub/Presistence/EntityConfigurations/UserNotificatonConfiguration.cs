using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Presistence.EntityConfigurations
{
    public class UserNotificatonConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificatonConfiguration()
        {
            HasKey(u => new { u.UserId, u.NotificationId });

            HasRequired(u => u.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}