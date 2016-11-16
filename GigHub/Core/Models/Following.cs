using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class Following
    {
        //  NOTE: FollowerId and FolloweeId create a COMPOSITEID
        //  NOTE: Alternatively, this class could be called Relationship
        //  NOTE: A Follower can have many Artist and an Followee (Artist) can have many Followers
        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string FolloweeId { get; set; }

        //  NOTE: The ApplicationUser properties are Navigation properties
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
    }
}