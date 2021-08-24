using NewsWebSite.Models;
using System.Collections.Generic;

namespace NewsWebSite.ViewModel
{
    public class ListViewModel
    {
        public IEnumerable<TemMember> TemMembers { get; set; }
        public IEnumerable<New> News { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
