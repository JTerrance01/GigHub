using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Attendance
    {
        //  NOTE: Navagation properties
        public Gig Gig { get; set; }
        public ApplicationUser Attendee { get; set; }

        //  NOTE: Foreign key properties. AttendeeId reflects the ApplicationUserId
        //  NOTE: Data Annotations Key and Column are used to create a COMPOSITE KEY
        //  NOTE: Column - specifies the order of the key
        [Key]
        [Column(Order = 1)]
        public int GigId { get; set; }

        //  NOTE: The Id property in the ApplicationUser class is a string
        //  CONT. AttendeeId will be mapped to the ApplicationUserId property
        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }
    }
}