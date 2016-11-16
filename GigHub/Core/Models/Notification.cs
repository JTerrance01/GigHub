using System;

namespace GigHub.Core.Models
{
    public class Notification
    {
        //  NOTE:   Id, DateTime, NotificationType and Gig set to private to protect 
        //          the state of Notification
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public string OriginalVenue { get; private set; }
        public DateTime OriginalDateTime { get; private set; }

        //  NOTE: The property Gig must be a non-nullalbe type - reference Fluent API Configuration
        public Gig Gig { get; private set; }


        public Notification()
        {
        }

        //  NOTE: Make sure this class protects it's state
        //  NOTE: Implement Factory methods - defer instantiation to subclasses
        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }

        //  NOTE: The FACTORY METHOD will make sure the NOTIFICATION is always in a valid state

        //  NOTE: Implementing Factory method - defer instantiation to subclasses
        public static Notification GigCreated(Gig gig)
        {
            //  NOTE: return constructor
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig newgig, DateTime originalDateTime, string originalVenue)
        {
            //  NOTE: return constructor
            var notification = new Notification(NotificationType.GigUpdated, newgig);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}