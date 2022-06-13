using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Net;
using ResturantProject.Models;
using Microsoft.AspNetCore.Mvc;
namespace ResturantProject.Repository
{
    public interface IRestro
    {
       PlayersFavRestroList FvtplyRest(string name, bool status = true);
        List<dbRestaurant> ResturantByName(string name);
        List<dbPlayer> PlayerByName(string name);

        List<PlayersFavRestro> getall();
        List<dbRestaurant> IndexforRestro();

        List<dbPlayer> IndexforPlayer();

        bool CreateforRestro(dbRestaurant emp);

        bool CreateforPlayer(dbPlayer pl);

        dbRestaurant EditforRestro(int id);

        dbPlayer EditforPlayer(int id);

        bool DeleteforRestro(int id);

        bool DeleteforPlayer(int id);
        bool RestaurantPlayerLink(PlayersFavRestro Obj);
        //List<string> FvtplyRes(string name, bool status = true);

        //List<string> FvtplyResatuarnt(string name);



    }
    public abstract class RestroAbs : IRestro
    {
        public abstract List<dbRestaurant> ResturantByName(string name);

        public abstract List<dbPlayer> PlayerByName(string name);


        public abstract List<PlayersFavRestro> getall();
        public abstract List<dbRestaurant> IndexforRestro();

        public abstract List<dbPlayer> IndexforPlayer();

        public abstract bool CreateforRestro(dbRestaurant emp);

        public abstract bool CreateforPlayer(dbPlayer pl);

        //public abstract List<string> FvtplyRes(string name, bool status = true);

        public abstract dbRestaurant EditforRestro(int id);

        public abstract dbPlayer EditforPlayer(int id);

        public abstract bool DeleteforRestro(int id);

        public abstract bool DeleteforPlayer(int id);
        public abstract bool RestaurantPlayerLink(PlayersFavRestro Obj);

        public abstract PlayersFavRestroList FvtplyRest(string name, bool status = true);

        //public abstract List<string> FvtplyResatuarnt(string name);
    }

    public class ResRepository : RestroAbs
    {
        private readonly RpContext dbcontext = null;
        private readonly object item;

        public ResRepository(RpContext _dbContxet)
        {
            dbcontext = _dbContxet;
        }

        public override bool CreateforRestro(dbRestaurant emp)
        {
            if (emp == null)
            {
                return false;
            }
            else
            {
                if (emp.RestaurantId == 0)
                {
                    dbcontext.Add(emp);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    dbcontext.Entry(emp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbcontext.SaveChanges();
                    return true;
                }
            }
        }

        public override bool CreateforPlayer(dbPlayer pl)
        {
            if (pl == null)
            {
                return false;
            }
            else
            {
                if (pl.PlayerId == 0)
                {
                    dbcontext.Add(pl);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    dbcontext.Entry(pl).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbcontext.SaveChanges();
                    return true;
                }
            }
        }

        public override List<dbRestaurant> IndexforRestro()
        {
            return dbcontext.Restauranttbl.ToList();
        }

        public override List<dbPlayer> IndexforPlayer()
        {
            return dbcontext.Playertbl.ToList();
        }
        public override dbRestaurant EditforRestro(int id)
        {
            var a = dbcontext.Restauranttbl.Find(id);
            return a;
        }

        public override dbPlayer EditforPlayer(int id)
        {
            var a = dbcontext.Playertbl.Find(id);
            return a;
        }
        public override bool DeleteforRestro(int id)
        {
            var a = dbcontext.Restauranttbl.Find(id);
            if (a == null)
            {
                return false;
            }
            else
            {
                dbcontext.Remove(a);
                dbcontext.SaveChanges();
                return true;
            }
        }

        public override bool DeleteforPlayer(int id)
        {
            var a = dbcontext.Playertbl.Find(id);
            if (a == null)
            {
                return false;
            }
            else
            {
                dbcontext.Remove(a);
                dbcontext.SaveChanges();
                return true;
            }
        }

        public override bool RestaurantPlayerLink(PlayersFavRestro Obj)
        {

            return true;

        }

        public override List<PlayersFavRestro> getall()
        {
            List<PlayersFavRestro> allplayer = new List<PlayersFavRestro>();
            var res = (from player in dbcontext.Playertbl
                       from Fav in dbcontext.ReslinkPlayer
                       from restaurent in dbcontext.Restauranttbl
                       where player.PlayerId == Fav.PlayerId
                       && restaurent.RestaurantId == Fav.RestaurantId
                       where player.PlayerId > 0
                       select new
                       {
                           player = player,
                           restaurent = restaurent,
                           //Fav = Fav
                       }).ToList();

            foreach (var item in res)
            {
                PlayersFavRestro obj = new PlayersFavRestro();

                obj.player = item.player;
                obj.restaurent = item.restaurent;
                //obj.Fav = item.Fav;
                allplayer.Add(obj);
            }
            return allplayer;
        }


        public override List<dbRestaurant> ResturantByName(string name)
        {
            var obj = dbcontext.Restauranttbl.Where(Models => Models.Name == name).ToList();
            return obj;
        }

        public override List<dbPlayer> PlayerByName(string name)
        {
            var obj1 = dbcontext.Playertbl.Where(Models => Models.Name == name).ToList();
            return obj1;
        }

        public override PlayersFavRestroList FvtplyRest(string name, bool status = true)
        {
            PlayersFavRestroList lt = new PlayersFavRestroList();

            var playerDetail = dbcontext.Playertbl.Where(x => x.Name == name).ToList();
            if (playerDetail != null)
            {
                var playerID = playerDetail.FirstOrDefault().PlayerId;

                var resDetail = (from map in dbcontext.ReslinkPlayer
                                 join res in dbcontext.Restauranttbl
                                 on map.RestaurantId equals res.RestaurantId
                                 where map.PlayerId == playerID
                                 select new dbRestaurant
                                 {
                                     RestaurantId = map.RestaurantId,
                                     Name = res.Name
                                 }).ToList();

                lt.player = playerDetail;
                lt.restaurent = resDetail;
            }
            return lt;

        }

        //public override list<string> fvtplyresatuarnt(string name)
        //{
        //    var player = dbcontext.playertbl.where(x => x.name == name).firstordefault();
        //    var playerid = player.playerid;
        //    var res = (from a in dbcontext.reslinkplayer
        //               where a.playerid == playerid
        //               select new dbrestaurant
        //               {
        //                   restaurantid = a.restaurantid,
        //               }
        //               ).tolist();
        //    list<string> listrest = new list<string>();
        //    foreach (dbrestaurant item in res)
        //    {
        //        var adder = dbcontext.restauranttbl.where(x => x.restaurantid == item.restaurantid).firstordefault().name;
        //        listrest.add(adder);
        //    }
        //    return listrest;
        //}





    }
}
