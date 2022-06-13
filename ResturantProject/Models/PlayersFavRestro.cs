namespace ResturantProject.Models
{
    public class PlayersFavRestro

    {

       public PlayersFavRestro()
        {
            player = new dbPlayer();
            restaurent = new dbRestaurant();
            Fav = new Reslinkplayer();
        }
        public dbPlayer player { get; set; }
        public dbRestaurant restaurent { get; set; }

        public Reslinkplayer Fav  { get; set; }


    }
    public class PlayersFavRestroList

    {
        public List<dbPlayer> player { get; set; }
        public List<dbRestaurant> restaurent { get; set; }

    }
}
