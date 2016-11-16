using System;

namespace GigHub.Core.Models
{
    public class UserNotification
    {
        public string UserId { get; private set; }

        public int NotificationId { get; private set; }

        public bool IsRead { get; private set; }

        //  NOTE: Navagation properties
        //  NOTE: Must not be allowed to set the property after initialized set private
        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }

        //  NOTE: Set Default constructor
        //  NOTE: When you create customer constructors always create default constructor
        public UserNotification()
        {
        }

        //  NOTE: UserNotification cannot be null
        //  NOTE: The domain model must protect it's state
        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (notification == null)
                throw new ArgumentNullException("notification");

            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}