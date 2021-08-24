using NewsWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebSite.ViewModel
{
    public class NewsViewModel
    {
        public IList<New> ListNews { get; set; }
        public  New News { get; set; }

    }
}
