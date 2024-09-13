using System.Collections.Generic;
using InstagramClone.ViewModels;


namespace InstagramClone.Models
{
	public class UserProfileViewModel
	{
		public string Username { get; set; }
		public string ProfilePictureUrl { get; set; }
		public string ProfileHeading { get; set; }
		public string ProfileBio { get; set; }
		public List<PostViewModel> Posts { get; set; }
		public int FollowersCount { get; set; }
		public int FollowingCount { get; set; }

		public byte[] ProfilePicture { get; set; }
	}
}
