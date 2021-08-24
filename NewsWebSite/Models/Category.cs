using System.Collections.Generic;

namespace NewsWebSite.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<New> News { get; set; }

    }
}
