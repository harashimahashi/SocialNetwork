using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models.Entities
{
    public class Publication
    {
        public User Owner { get; set; }

        [Required]
        public string Heading { get; set; }

        [Required]
        public string Content { get; set; }

        public byte[] Image { get; set; }
    }
}
