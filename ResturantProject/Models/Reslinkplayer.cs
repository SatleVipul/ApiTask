using System.ComponentModel.DataAnnotations;

namespace ResturantProject.Models
{
    public class Reslinkplayer
    {
        [Key]
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public int PlayerId { get; set; }

        public bool Fav { get; set; }

    }
}
