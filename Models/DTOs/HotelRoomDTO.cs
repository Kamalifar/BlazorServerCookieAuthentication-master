using System.ComponentModel.DataAnnotations;

namespace BlazorServerTestDynamicAccess.Models.DTOs
{
    public class HotelRoomDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the room's name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the occupancy")]
        public int Occupancy { get; set; }

        [Range(1, 3000, ErrorMessage = "Regular rate must be between 1 and 3000")]
        public decimal RegularRate { get; set; }

        public string Details { get; set; }

        public string SqFt { get; set; }
    }
}
