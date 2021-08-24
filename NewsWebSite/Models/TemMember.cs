using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebSite.Models
{
    public class TemMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

    }
}
