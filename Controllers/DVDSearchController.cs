using DatabaseCoursework.Models;
using groupCW.Data;
using groupCW.ViewModel;
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

        [HttpPost]
        public IActionResult FilterWithAvailability(string lName)
        {
            if (lName == null || lName.Trim() == "")
            {
                return RedirectToAction("DVDWithAvailability");
            }

            IEnumerable<JoinHelper> objDvdList = (IEnumerable<JoinHelper>)_db.Loans.Join(_db.DVDCopies,
                loan => loan.CopyNumber, dvdcopy => dvdcopy.CopyNumber,
                (loan, dvdcopy) => new
                {
                    dvdIdFromCopies = dvdcopy.DVDNumber,
                    dvdReturnedDate = loan.DateReturned,
                    copyId = dvdcopy.CopyNumber,
                    dvdNumberId = dvdcopy.DVDNumber,
                    dateOut = loan.DateOut,
                }
            ).Join(_db.DVDTitles,
                dvdcopies => dvdcopies.dvdNumberId, dvdtitles => dvdtitles.DVDNumber,
                (dvdcopies, dvdtitles) => new
                {
                    dvdIdFromCopies = dvdcopies.dvdIdFromCopies,
                    copyId = dvdcopies.copyId,
                    dvdReturnedDate = dvdcopies.dvdReturnedDate,
                    dvdNumberId = dvdtitles.DVDNumber,
                    releaseDate2 = dvdtitles.DateReleased,
                    dvdtitle = dvdtitles.DVDTitles,
                    dateOut = dvdcopies.dateOut,
                }
            ).Join(_db.CastMembers,
                dvdtitnumber => dvdtitnumber.dvdNumberId, castmem => castmem.DVDNumber,
                (dvdtitnumber, castmem) => new
                {
                    dvdIdFromCopies = dvdtitnumber.dvdIdFromCopies,
                    dvdtitle = dvdtitnumber.dvdtitle,
                    copyId = dvdtitnumber.copyId,
                    dvdReturnedDate = dvdtitnumber.dvdReturnedDate,
                    releaseDate2 = dvdtitnumber.releaseDate2 == null ? "" : dvdtitnumber.releaseDate2.ToString(),
                    dvdNumberId = dvdtitnumber.dvdNumberId,
                    castmemberid = castmem.DVDNumber,
                    actoriden = castmem.ActorNumber,
                    dateOut = dvdtitnumber.dateOut,
                }
            ).Join(_db.Actors,
                castmeme => castmeme.actoriden, act => act.ActorNumber,
                (castmeme, act) => new JoinHelper
                {
                    dvdNumberId = castmeme.dvdIdFromCopies,
                    fName = act.ActorFirstname,
                    lName = act.ActorSurname,
                    dvdReturnedDate = castmeme.dvdReturnedDate,
                    castMemberId = castmeme.castmemberid,
                    releaseDate2 = castmeme.releaseDate2,
                    dvdtitle = castmeme.dvdtitle,
                    copyId = castmeme.copyId,
                    dateOut = castmeme.dateOut
                }
            ).Where(x => x.lName.ToLower() == lName.ToLower())
            .ToList();

            return View(objDvdList);


            //return Json(objDvdList);
        }


    }
}
