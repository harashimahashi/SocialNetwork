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

        public static implicit operator Publication(ApplicationPublication pub)
        {
            return new() { 
                Heading = pub.Heading,
                Content = pub.Content,
                Image = pub.Image
            };
        }
    }
}
