using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]//adding data annotations on modal class thru .net for field name 
        //These validations are provided by the [APIController] and if we comment it out then these validations will be of no use
        [MaxLength(30)]
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }

    }
}
