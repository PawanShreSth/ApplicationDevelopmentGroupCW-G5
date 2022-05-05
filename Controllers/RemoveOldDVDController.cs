using groupCW.Data;
using groupCW.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace groupCW.Controllers
{
    public class RemoveOldDVDController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RemoveOldDVDController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<RemoveOldDVDViewModel> objDvdList = _db.DVDCopies.Join(_db.DVDTitles,
                    copies => copies.DVDNumber, dvd => dvd.DVDNumber,
                    (copies, dvd) => new
                    {
                        dTitle = dvd.DVDTitles,
                        dvdid = dvd.DVDNumber,
                        copyNumber = copies.CopyNumber,
                        dvdReleasedDate = dvd.DateReleased,
                        dvdDatePurchased = copies.DatePurchased,

                    }
                ).Join(_db.Loans,
                    copies => copies.copyNumber, loan => loan.CopyNumber,
                    (copies, loan) => new RemoveOldDVDViewModel()
                    {
                        dvdTitle = copies.dTitle,
                        dvdReleaseDate = copies.dvdReleasedDate,
                        copyNumber = copies.copyNumber,
                        dvdDatePurchased = copies.dvdDatePurchased,
                        dvdDateReturned = loan.DateReturned,
                        dvdDateOut = loan.DateOut,

                    }).Where(x => x.dvdDatePurchased <= DateTime.Now.AddDays(-365) && x.dvdDateReturned != null).ToList();

            return View(objDvdList);

        }
    }
}
