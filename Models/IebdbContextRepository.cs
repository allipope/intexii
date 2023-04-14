using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intexii.Models
{
    public interface IebdbContextRepository
    {
        IQueryable<Burialmain> Burialmains { get; }
        IQueryable<BurialmainTextile> BurialmainTextiles { get; }
        IQueryable<Textile> Textiles { get; }
        IQueryable<ColorTextile> ColorTextiles { get; }
        IQueryable<Color> Colors { get; }
        IQueryable<StructureTextile> StructureTextiles { get; }
        IQueryable<Structure> Structures { get; }
    }
}