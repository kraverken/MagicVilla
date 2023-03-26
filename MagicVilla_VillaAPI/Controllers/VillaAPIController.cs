using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Logging;
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
        //private readonly ILogger<VillaAPIController> _logger;

        //public VillaAPIController(ILogger<VillaAPIController> logger)
        //{
        //    _logger = logger;
        //} In order to implement our own logger thru Dependency Injection we comment this 

        private readonly ILogging _logger;
        private readonly ApplicationDBContext _db;
        public VillaAPIController(ILogging logging, ApplicationDBContext db)
        {
            _logger = logging;
            _db = db;
        }

        [HttpGet]
        public ActionResult< IEnumerable<VillaDTO>>GetVillas() //Creating a public enumerable method getVillas of type Villa 
        {
            _logger.Log("Getting All Villas","");
            //_logger.LogInformation("Getting All Villas");

            //return Ok(VillaStore.villaList);
            return Ok(_db.Villas);
        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        public ActionResult<VillaDTO> GetVilla(int id) //Creating a public enumerable method getVillas of type Villa 
        {
            //var villa=VillaStore.villaList.FirstOrDefault(x=>x.Id==id);
            var villa=_db.Villas.FirstOrDefault(x=>x.Id==id);

            if (id == 0)
            {
                _logger.Log("Get Villa Error with " + id,"error");
                //_logger.LogInformation("Get Villa Error with " + id);
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
            //if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower())
            //{
            //    ModelState.AddModelError("customerror", "villa already exists!"); //first parameter is key second is error message 
            //    return BadRequest(ModelState);
            //}

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if(villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //villaDTO.Id=VillaStore.villaList.OrderByDescending(u=>u.Id).FirstOrDefault().Id+1; not needed as now Id is a key 

            Villa model= new()
                {
                    Amenity=villaDTO.Amenity,
                    Details=villaDTO.Details,
                    Id=villaDTO.Id,
                    ImageUrl=villaDTO.ImageUrl,
                    Name=villaDTO.Name,
                    Occupancy=villaDTO.Occupancy,
                    Rate=villaDTO.Rate,
                    Sqft=villaDTO.Sqft,
                }; //use this to remove the villa is not null error
            _db.Villas.Add(model);
            _db.SaveChanges();

            //VillaStore.villaList.Add(villaDTO);

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
            var villa= _db.Villas.FirstOrDefault(u=>u.Id==id);
            //var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();

            //VillaStore.villaList.Remove(villa);

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
            //var villa= VillaStore.villaList.FirstOrDefault(u=>u.Id==id);
            //villa.Name=villaDTO.Name;
            //villa.Occupancy=villaDTO.Occupancy;
            //villa.Sqft=villaDTO.Sqft;

            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Update(model);
            _db.SaveChanges();

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

            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            //var villa = VillaStore.villaList.FirstOrDefault(u=>u.Id==id);

            VillaDTO villaDTO = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
            };
            if (villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villaDTO,ModelState);
            //patchDTO.ApplyTo(villa,ModelState);

            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

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
