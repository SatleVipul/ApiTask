using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantProject.Models
{
    public class Mapping
    {
        [Key]
        public int Id { get; set; }

        public virtual dbRestaurant RestaurantId { get; set; }

     
        public virtual dbPlayer PlayerId { get; set; }


        //[ForeignKey("UserDetails")]
        //public virtual int UserDetailsId
        //{
        //    get;
        //    set;
        //}
       

    }
}
