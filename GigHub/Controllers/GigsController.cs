using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
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

        //  NOTE: The Authorize Attribute controls the login Authorization
        [Authorize]
        public ActionResult Create()
        {
            var dropdownListOfGenres = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(dropdownListOfGenres);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            //  NOTE: Be wery of Inline variables - replaced below - ArtistId...
            //var artistId = User.Identity.GetUserId(); --Inline varible

            //  NOTE: 2 additional trips to Db not neccessary... set up foriegn keys for ArtistId and GenreId navagation properties
            //var artist = _context.Users.Single(u => u.Id == artistId); -- trip 1
            //var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);  -- trip 2

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = DateTime.Parse(string.Format("{0} {1}", viewModel.Date, viewModel.Time)),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}