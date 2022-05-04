namespace groupCW.Views.DVDSearch
{
    public class JoinHelper
    {
        // This class was created to supply multiple models in a view
        public string fName { get; set; }
        public string lName { get; set; }
        public int castMemberId { get; set; }
        public int castmemberid { get; set; }
        public int actoriden { get; set; }
        public int dvdNumberId { get; set; }
        public string dTitleName { get; set; }
        public string dvdtitle { get; set; }
        public int dNumber { get; set; }
        public string releaseDate2 { get; set; }

        public int dvdCopyNumber { get; set; }

        public int copyId { get; set; }

        public int copyNumber { get; set; }

        public DateTime? dateOut { get; set; }

        public int loan { get; set; }

        public DateTime? dvdCopyDatePurchased { get; set; }

        public DateTime? dvdReturnedDate { get; set; }


        // Used for number 4
        public int dvdId { get; set; }
        public string dvdTitle { get; set; }
        public string? producerName { get; set; }
        public string studioName { get; set; }

        public string actorFirstName { get; set; }
        public string actorLastName { get; set; }

    }
}
