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
        public ActionResult< IEnumerable<VillaDTO>>GetVillas() //Creating a public enumerable method getVillas of type Villa 
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<VillaDTO> GetVilla(int id) //Creating a public enumerable method getVillas of type Villa 
        {
            var villa=VillaStore.villaList.FirstOrDefault(x=>x.Id==id);
            if (id == 0)
            {
                return BadRequest();
            }
            else if (villa == null)
            {
                return NotFound();
            }
            else
            {
            return Ok(villa);
            }
        }
    }
}
