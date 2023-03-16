using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

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
            //if (villastore.villalist.firstordefault(u => u.name.tolower() == villadto.name.tolower())
            //{
            //    modelstate.addmodelerror("customerror", "villa already exists!"); //first parameter is key second is error message 
            //    return badrequest(modelstate);
            //}
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

        [HttpDelete("{id:int}",Name ="DeleteVilla")] //name not compulsory
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult DeleteVilla(int id) //Iactionresult automatically decides the return type
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa= VillaStore.villaList.FirstOrDefault(u=>u.Id==id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}",Name ="UpdateVillaviaPUT")] //updates the full modal
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if(villaDTO==null || id!=villaDTO.Id)
            {
                return BadRequest();
            }
            var villa= VillaStore.villaList.FirstOrDefault(u=>u.Id==id);
            villa.Name=villaDTO.Name;
            villa.Occupancy=villaDTO.Occupancy;
            villa.Sqft=villaDTO.Sqft;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdateVillaviaPAtch")] //updates the patial modal
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO==null|| id == 0)
            {
                return BadRequest();
            }
            var villa= VillaStore.villaList.FirstOrDefault(u=>u.Id==id);
            if(villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villa,ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

            /*{ Required fields for patch
                "path": "/name",
                "op": "replace",
                "value": "new Villa 2"
            }*/
        }

    }
}
