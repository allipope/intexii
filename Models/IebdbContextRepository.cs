using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intexii.Models
{
    public interface IebdbContextRepository
    {
        IQueryable<Burialmain> Burialmains {get;}
        IQueryable<BurialmainTextile> BurialmainTextiles { get; }
        IQueryable<Textile> Textiles { get; }
    }
}