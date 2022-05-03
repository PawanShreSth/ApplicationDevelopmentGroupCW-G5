using System.ComponentModel.DataAnnotations;

namespace DatabaseCoursework.Models
{
    public class MembershipCategory
    {
        [Key]
        public int MembershipCatagoryNumber { get; set; }
        public string MembershipCategoryDescription { get; set; }
        public string MembershipCategoryTotalLoans { get; set; }
    }
}
