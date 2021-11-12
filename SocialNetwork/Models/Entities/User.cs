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
    }
}
