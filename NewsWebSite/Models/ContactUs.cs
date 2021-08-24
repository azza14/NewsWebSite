using System.ComponentModel.DataAnnotations;

namespace NewsWebSite.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        [Display(Name = " Your Name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }


    }
}
