using System.Linq;

namespace intexii.Models
{
    public class EFebdbContextRepository : IebdbContextRepository
    {
        private ebdbContext context {get; set;}

        public EFebdbContextRepository (ebdbContext temp)
        {
            context = temp;
        }

        public IQueryable<Burialmain> Burialmains => context.Burialmains;
        public IQueryable<BurialmainTextile> BurialmainTextiles => context.BurialmainTextiles;
        public IQueryable<Textile> Textiles => context.Textiles;


    }

}