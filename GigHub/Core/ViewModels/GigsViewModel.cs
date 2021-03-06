﻿using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }

        public bool ShowActions { get; set; }

        public string Heading { get; set; }

        public string SearchTerm { get; set; }

        //  NOTE: Functions similar to a Dictionary property Tkey, Telement
        //  CONT: Used to track attendances and update the going button
        public ILookup<int, Attendance> Attendances { get; set; }
    }
}