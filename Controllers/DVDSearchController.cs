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

        [HttpPost]
        public IActionResult Filter(string lName)
        {
            if (lName == null || lName.Trim() == "")
            {
                return RedirectToAction("Index");
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
                     
                 }
                 ).Where(x => x.lName.ToLower() == lName.ToLower()).ToList();



            return View(objDvdList);
        }

        public IActionResult DVDWithAvailability()
        {
            return View();
        }

        public IActionResult FilterWithAvailability(string lName)
        {
            if (lName == null || lName.Trim() == "")
            {
                return RedirectToAction("DVDWithAvailability");
            }


            var test = (
                        from l in _db.Loans
                        join dvcpy in _db.DVDCopies
                        on l.CopyNumber equals dvcpy.CopyNumber
                        join dvtitle in _db.DVDTitles
                        on dvcpy.DVDNumber equals dvtitle.DVDNumber
                        join casmem in _db.CastMembers
                        on dvtitle.DVDNumber equals casmem.DVDNumber
                        join a in _db.Actors
                        on casmem.ActorNumber equals a.ActorNumber
                        group new {
                            Loan = l,
                            DVDCop = dvcpy,
                            DVDTit = dvtitle,
                            CASTMEMB = casmem,
                            ACT = a,

                        } by dvtitle.DVDTitles
                        //select new JoinHelper
                        //{
                        //    fName = a.ActorFirstname,
                        //    lName = a.ActorSurname,
                        //    dvdReturnedDate = l.DateReturned,
                        //    castMemberId = casmem.DVDNumber,
                        //    dvdtitle = dvtitle.DVDTitles,
                        //    copyId = dvcpy.CopyNumber


                        //}
                ).ToList();
                //.Where(x => x.lName == lName.ToLower())
                
      

            return Json(test);

        }


    }
}
