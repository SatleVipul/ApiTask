using Microsoft.EntityFrameworkCore;

namespace ResturantProject.Models
{
    public class RpContext: DbContext
    {
        public RpContext(DbContextOptions<RpContext> options) : base(options)
        {

        }
        public DbSet<dbRestaurant> Restauranttbl { get; set; }

        public DbSet<dbPlayer> Playertbl { get; set; }

        public DbSet<Reslinkplayer> ReslinkPlayer { get; set; }

        //public DbSet<Mapping> tblMapping { get; set; }



    }
}
