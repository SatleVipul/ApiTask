using Microsoft.AspNetCore.Mvc;
using ResturantProject.Models;
using ResturantProject.Repository;


namespace ResturantProject.Controllers
{

    [ApiController]
    [Route("Api/[Controller]/[action]")]
    public class TaskController : Controller
    {
        private readonly ResRepository repo = null;
        public TaskController(ResRepository _repo)
        {
            repo = _repo;
        }
        public IActionResult IndexforRestro()
        {
            var a = repo.IndexforRestro();
            return Ok(a);
        }
        public IActionResult IndexforPlayer()
        {
            var a = repo.IndexforPlayer();
            return Ok(a);
        }
        [HttpPost]
        public IActionResult CreateforRestro(dbRestaurant emp)
        {
            var a = repo.CreateforRestro(emp);
            return Ok(a);
        }
        [HttpPost]
        public IActionResult CreateforPlayer(dbPlayer pl)
        {
            var a = repo.CreateforPlayer(pl);
            return Ok(a);
        }
        [HttpGet("{id}")]
        public IActionResult EditforRestro(int id)
        {
            return Ok(repo.EditforRestro(id));
        }
        [HttpGet("{id}")]
        public IActionResult EditforPlayer(int id)
        {
            return Ok(repo.EditforPlayer(id));
        }
        [HttpGet("{id}")]
        public IActionResult DeleteforRestro(int id)
        {
            return Ok(repo.DeleteforRestro(id));
        }
        [HttpGet("{id}")]
        public IActionResult DeleteforPlayer(int id)
        {
            return Ok(repo.DeleteforPlayer(id));
        }
        //public IActionResult RestaurantPlayerLink(List<PlayersFavRestro> obj)
        //{
        //    return Ok(repo.RestaurantPlayerLink(List<PlayersFavRestro> obj));
        //}

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(repo.Index() );
        }

        [HttpGet("{name}")]
        public List<dbRestaurant> ResturantByName(string name)
        {
            return (repo.ResturantByName(name));
        }

        [HttpGet("{name}")]
        public List<dbPlayer> PlayerByName(string name)
        {
            return (repo.PlayerByName(name));
        }
        [HttpGet("{name}")]
        public PlayersFavRestroList favplyRes(string name)
        {
            var statuss = true;
            return (repo.FvtplyRest(name, statuss));
        }

        //[HttpGet("{name}")]
        //public List<string> FvtplyResatuarnt(string name)
        //{
        //    return (repo.FvtplyRes(name));
        //}
        [HttpGet]
        public IActionResult GetbyAge(string Name,int Age)
        {
            var result = repo.GetbyAge(Name,Age);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult RestroPlayerMapping(Reslinkplayer mod)
        {
            var successStatus = repo.RestroPlayerMapping(mod);
            return Ok(successStatus);
        }
        [HttpGet("{Name}")]
        public List<PlayersFavRestro> FvrtRes(string name, bool status = true)
        {
          
            return (repo.FvrtRes(name));
        }

    }
}
