using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intexii.Models.ViewModels
{
    public class SummaryPageModel
    {
        public long Id { get; set; }
        public DateTime? Dateofexcavation { get; set; }
        public string TextileDescription { get; set; }
        public string Squarenorthsouth { get; set; }
        public string Squareeastwest { get; set; }
        public string Sex { get; set; }
        public string Depth { get; set; }
        public string? Burialnumber { get; set; }
        public string? Estimatedperiod { get; set; }


        public string? Locale { get; set; }
        public string? Wrapping { get; set; }
        public string? Adultsubadult { get; set; }
        public string? Text { get; set; }
        public string? Haircolor { get; set; }
        public string AgeatDeath { get; set; }
        public string Color { get; set; }
        public string Structure { get; set; }
        public string Headdirection { get; set; }

    }
}