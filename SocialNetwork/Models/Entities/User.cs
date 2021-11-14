using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models.Entities
{
    public class User
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public string Residence { get; set; }

        public DateTime BirthDate { get; set; }

        public string Interests { get; set; }
        
        public byte[] Image { get; set; }

        public static implicit operator User(ApplicationUser u) 
        {
            return new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Residence = u.Residence,
                    BirthDate = u.BirthDate,
                    Interests = u.Interests,
                    Image = u.Image
                };
        }
    }
}
