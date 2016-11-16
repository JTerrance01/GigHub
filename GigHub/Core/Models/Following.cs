namespace GigHub.Core.Models
{
    public class Following
    {
        //  NOTE: FollowerId and FolloweeId create a COMPOSITEID
        //  NOTE: Alternatively, this class could be called Relationship
        //  NOTE: A Follower can have many Artist and an Followee (Artist) can have many Followers

        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }

        //  NOTE: The ApplicationUser properties are Navigation properties

        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
    }
}