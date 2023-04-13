using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intexii.Models.ViewModels
{
    public class BurialViewModel
    {
        public IQueryable<Burialmain> Burialmains { get; set; }
        public List<BurialPageModel> BurialViews { get; set; }

        public List<SummaryPageModel> identification { get; set; }



        public List<SummaryPageModel> SummaryViews { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
