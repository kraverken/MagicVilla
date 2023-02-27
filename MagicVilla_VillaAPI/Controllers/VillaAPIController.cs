using MagicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{

    [ApiController]
    public class VillaAPIController :ControllerBase
    {
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
