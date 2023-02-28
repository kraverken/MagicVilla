using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController :ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas() //Creating a public enumerable method getVillas of type Villa 
        {
            return new List<VillaDTO> //Creating a list of type Villa
            {
                new VillaDTO {Id=1,Name="Pool View"},
                new VillaDTO {Id=2,Name="Beach View"}
            };
        }
    }
}
