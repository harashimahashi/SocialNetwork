namespace SocialNetwork.Models.Entities
{
    public class Comment
    {
        public string Id { get; set; }

        public User Owner { get; set; }

        public string Content { get; set; }

        public static implicit operator Comment(ApplicationComment com)
        {
            return new()
            {
                Id = com.Id,
                Content = com.Content
            };
        }
    }
}
