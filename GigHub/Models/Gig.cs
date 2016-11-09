﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        //  NOTE: Foreign key properties
        [Required]
        public byte GenreId { get; set; }

        [Required]
        public string ArtistId { get; set; }

        //  NOTE: Navigation property - be sure to keep it in a valid state private set
        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            //  Initialize navigation property in contstructor
            Attendances = new Collection<Attendance>();
        }

        //  NOTE:
        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.GigCanceled(this);

            //  NOTE: Iterate of Attendee not Attendances
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);

            }
        }

        internal void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}