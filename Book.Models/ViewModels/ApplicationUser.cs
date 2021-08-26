using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models.ViewModels
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
