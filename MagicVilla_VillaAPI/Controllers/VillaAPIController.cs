using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Data;
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
            return VillaStore.villaList;
        }

        [HttpGet("{id:int}")]
        public VillaDTO GetVilla(int id) //Creating a public enumerable method getVillas of type Villa 
        {
            return VillaStore.villaList.FirstOrDefault(x=>x.Id==id);
        }
    }
}
