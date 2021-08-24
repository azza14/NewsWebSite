using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebSite.Models
{
    public class New
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Topic { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
