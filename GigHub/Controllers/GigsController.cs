using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            //  NOTE: Get list of User gigs
            var gigs = _context.Gigs
                .Where(g =>
                g.ArtistId == userId &&
                g.DateTime > DateTime.Now &&
                !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            //  NOTE: A list of the user's Gigs
            //  NOTE: Must EAGER load Artist and Genre
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            //  NOTE: attendances - used in HomeController to update Going button
            //  NOTE: Code duplication will have to be refactored, however need to initialize GigsViewModel
            var attendances = _context.Attendances
               .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
               .ToList()
               .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending.",
                Attendances = attendances
            };

            return View("Gigs", viewModel);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        //  NOTE: The Authorize Attribute controls the login Authorization
        [Authorize]
        public ActionResult Create()
        {
            var dropdownListOfGenres = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig",
            };

            return View("GigForm", dropdownListOfGenres);
        }

        [Authorize]
        public ActionResult Edit(int gigId)
        {
            var userId = User.Identity.GetUserId();

            //  Note: The ArtistId is the id of the currently logged in user
            var gig = _context.Gigs
                .Single(g => g.Id == gigId && g.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Id = gig.Id,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig"
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken] // NOTE: (CSRF) used to combat
        public ActionResult Create(GigFormViewModel viewModel)
        {
            //  NOTE: Be wery of Inline variables - replaced below - ArtistId...
            //  var artistId = User.Identity.GetUserId(); --Inline varible

            //  NOTE: 2 additional trips to Db not neccessary... set up foriegn keys for ArtistId and GenreId navagation properties
            //  var artist = _context.Users.Single(u => u.Id == artistId); -- trip 1
            //  var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);  -- trip 2


            //  NOTE: Server-side Validation - ModelState.IsValid return Create viewModel
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken] // NOTE: (CSRF) used to combat
        public ActionResult Update(GigFormViewModel viewModel)
        {


            //  NOTE: Server-side Validation - ModelState.IsValid return Create viewModel
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances
                    .Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

                viewModel.IsFollowing = _context.Followings
                    .Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == userId);
            }

            return View("Details", viewModel);
        }
    }
}