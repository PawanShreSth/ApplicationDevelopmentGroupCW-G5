using groupCW.Data;
using groupCW.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace groupCW.Controllers
{
    public class NoCopyLoanedForLast31DaysController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NoCopyLoanedForLast31DaysController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<NoCopyLoanedForLast31DaysViewModel> dvdList = _db.Loans
                .Join(
                    _db.DVDCopies,
                    loans => loans.CopyNumber, dvdcopies => dvdcopies.CopyNumber,
                    (loans, dvdcopies) => new NoCopyLoanedForLast31DaysViewModel
                    {
                        dvdNumber = dvdcopies.DVDNumber,
                        dateOut = loans.DateOut
                    }
                )
                .Join(
                    _db.DVDTitles,
                    dvdcopies => dvdcopies.dvdNumber, dvdtitle => dvdtitle.DVDNumber,
                    (dvdcopies, dvdtitle) => new NoCopyLoanedForLast31DaysViewModel
                    {
                        dvdNumber = dvdcopies.dvdNumber,
                        dvdTitle = dvdtitle.DVDTitles,
                        dateOut = dvdcopies.dateOut
                    }
                )
                .Where(
                    x => (DateTime.Now.AddDays(-31) >= x.dateOut)
                )
                .ToList();

            return View(dvdList);
        }
    }
}
