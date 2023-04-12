using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intexii.Models.ViewModels
{
    public class SummaryPageModel
    {
        public long Id { get; set; }
        public DateTime? Dateofexcavation {get; set;}
        public string TextileDescription { get; set; }
        public string Squarenorthsouth { get; set; }
        public string Squareeastwest { get; set; }
        public string Sex { get; set; }
        public string Depth {get; set;}
        public string? Burialnumber { get; set; }
        public string? Estimatedperiod { get; set; }
    }
}