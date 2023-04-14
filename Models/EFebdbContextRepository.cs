using System.Linq;

namespace intexii.Models
{
    public class EFebdbContextRepository : IebdbContextRepository
    {
        private ebdbContext context { get; set; }

        public EFebdbContextRepository(ebdbContext temp)
        {
            context = temp;
        }

        public IQueryable<Burialmain> Burialmains => context.Burialmains;
        public IQueryable<BurialmainTextile> BurialmainTextiles => context.BurialmainTextiles;
        public IQueryable<Textile> Textiles => context.Textiles;
        public IQueryable<ColorTextile> ColorTextiles => context.ColorTextiles;
        public IQueryable<Color> Colors => context.Colors;
        public IQueryable<StructureTextile> StructureTextiles => context.StructureTextiles;
        public IQueryable<Structure> Structures => context.Structures;



    }

}