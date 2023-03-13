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

        [HttpGet("{id:int}", Name ="GetVilla")]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO) //property to be bounded using request body
        {
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if(villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.Id=VillaStore.villaList.OrderByDescending(u=>u.Id).FirstOrDefault().Id+1;
            VillaStore.villaList.Add(villaDTO);
            //return Ok(villaDTO);
            return CreatedAtRoute("GetVilla",new { id = villaDTO.Id }, villaDTO);
        }
    }
}
