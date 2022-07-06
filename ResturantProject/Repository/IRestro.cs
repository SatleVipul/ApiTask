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

        List<PlayersFavRestro> Index();
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
        PlayersFavRestroList GetbyAge(string Name, int age);

        List<string> fvtplyresatuarnt(string name);


    }
    public abstract class RestroAbs : IRestro
    {
        public abstract List<dbRestaurant> ResturantByName(string name);

        public abstract List<dbPlayer> PlayerByName(string name);


        public abstract List<PlayersFavRestro> Index();
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

        public abstract PlayersFavRestroList GetbyAge(string Name, int age);
        public abstract List<string> fvtplyresatuarnt(string name);
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

        public override List<PlayersFavRestro> Index()
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
        //public override List<FavRestroPlayer> FvrtRes(string name, bool status = true)
        //{
        //    List<FavRestroPlayer> ListRest = new List<FavRestroPlayer>();

        //    if (name != null && name != "")
        //    {
        //        var res = (from pl in dbcontext.Playertbl
        //                   from fv in dbcontext.ReslinkPlayer
        //                   from rt in dbcontext.Restrotab
        //                   where pl.PlayersId == fv.PlayersId
        //                   && rt.RestroId == fv.RestroId
        //                   && name.Equals(pl.PlayerName)
        //                   && fv.Fav == status
        //                   select new
        //                   {
        //                       pl = pl,
        //                       rt = rt,
        //                       fv = fv
        //                   }).ToList();
        //        //select new
        //        //{
        //        //    PlayersId = pt.PlayersId,
        //        //    PlayerName = pt.PlayerName,
        //        //    PlayerDOB = pt.PlayerDOB.ToString(),
        //        //    PlayerPrimaryAdd = pt.PlayerPrimaryAdd,
        //        //    PlayerAlternateAdd = pt.PlayerAlternateAdd,
        //        //    PlayerOfficeAdd = pt.PlayerOfficeAdd,
        //        //    PlayerMobNo = pt.PlayerMobNo,
        //        //    PlayerEmail = pt.PlayerEmail,
        //        //    PlayerDL = pt.PlayerDL,
        //        //    PlayerPassport = pt.PlayerPassport,
        //        //    PlayerCountry = pt.PlayerCountry,
        //        //    PlayerState = pt.PlayerState,
        //        //    PlayerCity = pt.PlayerCity,
        //        //    AddressLine1 = pt.AddressLine1,
        //        //    AddressLine2 = pt.AddressLine2,
        //        //    PlayerPostal = pt.PlayerPostal,
        //        //    RestroId = rt.RestroId,
        //        //    RestroName = rt.RestroName,
        //        //    RestroAddress = rt.RestroAddress,
        //        //    RestroContact = rt.RestroContact
        //        //}
        //        //).ToList();

        //        foreach (var item in res)
        //        {
        //            FavRestroPlayer obj = new FavRestroPlayer();

        //            obj.pl = item.pl;
        //            obj.rt = item.rt;
        //            obj.fv = item.fv;
        //            ListRest.Add(obj);
        //        }
        //    }
        //    return ListRest;
        //}

        public override PlayersFavRestroList GetbyAge(string Name, int age)
        {
            PlayersFavRestroList lst = new PlayersFavRestroList();
            var RestaurantCollection = dbcontext.Restauranttbl.FirstOrDefault(x => x.Name == Name);
            List<dbPlayer> pares = new List<dbPlayer>();
            List<dbPlayer> set = new List<dbPlayer>();

            if (RestaurantCollection != null)
            {
                var restaurentId = RestaurantCollection.RestaurantId;

                var LinkDetail = dbcontext.ReslinkPlayer.Where(x => x.RestaurantId == restaurentId).ToList();
                foreach (var item in LinkDetail)
                {

                    var abc = dbcontext.Playertbl.Where(x => x.PlayerId == item.PlayerId).FirstOrDefault();
                    set.Add(abc);

                }
                if (set.Count > 0)
                {
                    foreach (var item in set)
                    {

                        var years = DateTime.Now.Year - Convert.ToDateTime(item.dob).Year;

                        if (years >= age)
                        {
                            pares.Add(item);
                        }

                        years = 0;
                    }
                }
                List<PlayersFavRestro> res = new List<PlayersFavRestro>();
                var areas = (from player in dbcontext.Playertbl
                             from restaurent in dbcontext.Restauranttbl
                             from Fav in dbcontext.ReslinkPlayer
                             where player.PlayerId == Fav.PlayerId
                             && restaurent.RestaurantId == Fav.RestaurantId
                             select new
                             {
                                 player = player,
                                 restaurent = restaurent,
                                 Fav = Fav
                             }).ToList();
                foreach (var item in areas)
                {
                    PlayersFavRestro obj1 = new PlayersFavRestro();

                    obj1.player = item.player;
                    obj1.restaurent = item.restaurent;
                    obj1.Fav = item.Fav;
                    res.Add(obj1);
                }


                lst.rest = RestaurantCollection;
                lst.player = pares;




            }

            return lst;
        }

        public override List<string> fvtplyresatuarnt(string name)
        {
            var player = dbcontext.Playertbl.Where(x => x.Name == name).FirstOrDefault();
            var playerid = player.PlayerId;
            var res = (from a in dbcontext.ReslinkPlayer
                       where a.PlayerId == playerid
                       select new dbRestaurant
                       {
                           RestaurantId = a.RestaurantId,
                       }
                       ).ToList();
            List<string> Listrest = new List<string>();
            foreach (dbRestaurant item in res)
            {
                var adder = dbcontext.Restauranttbl.Where(x => x.RestaurantId == item.RestaurantId).FirstOrDefault().Name;
                Listrest.Add(adder);
            }
            return Listrest;
        }
        








    }
}

















