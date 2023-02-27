using MagicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController :ControllerBase
    {
        [HttpGet]
        public IEnumerable<Villa> GetVillas() //Creating a public enumerable method getVillas of type Villa 
        {
            return new List<Villa> //Creating a list of type Villa
            {
                new Villa {Id=1,Name="Pool View"},
                new Villa {Id=2,Name="Beach View"}
            };
        }
    }
}
