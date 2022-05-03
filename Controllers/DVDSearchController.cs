using DatabaseCoursework.Models;
using groupCW.Data;
using groupCW.Views.DVDSearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace groupCW.Controllers
{
    public class DVDSearchController : Controller
    {
        private readonly ApplicationDbContext _db;

      

        public DVDSearchController(ApplicationDbContext db)
        {
            _db = db;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Filter(string lName)
        {
            if (lName == null)
            {
                return NotFound();
            }

            IEnumerable<JoinHelper> objDvdList = _db.DVDTitles.Join(_db.CastMembers,
                 dvdtitles => dvdtitles.DVDNumber, castmem => castmem.DVDNumber,
                 (dvdtitles, castmem) => new
                 {
                     dTitle = dvdtitles.DVDTitles,
                     castmemberid = castmem.DVDNumber,
                     actoriden = castmem.ActorNumber,
                     releaseDate = dvdtitles.DateReleased
                 }
                 ).Join(_db.Actors, castmeme => castmeme.actoriden, act => act.ActorNumber,
                 (castmeme, act) => new JoinHelper
                 {
                     fName = act.ActorFirstname,
                     lName = act.ActorSurname,
                     castMemberId = castmeme.castmemberid,
                     dTitleName = castmeme.dTitle,
                     releaseDate2 = castmeme.releaseDate
                 }
                 ).Where(x => x.lName.ToLower() == lName.ToLower()).ToList();

            

            return View(objDvdList);
        }


    }
}
