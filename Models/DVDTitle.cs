using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseCoursework.Models
{
    public class DVDTitle
    {
        [Key]
        public int DVDNumber { get; set; }

        public int CategoryNumber { get; set; }
        public int StudioNumber { get; set; }
        public int ProducerNumber { get; set; }  
        public string DVDTitles { get; set; }
        public DateTime DateReleased { get; set; }
        public string StandardCharge { get; set; }
        public string PenaltyCharge { get; set; }

    }
}
