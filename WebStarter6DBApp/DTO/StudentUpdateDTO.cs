using System.ComponentModel.DataAnnotations;

namespace WebStarter6DBApp.DTO
{
    public class StudentUpdateDTO : BaseDTO
    {
        public StudentUpdateDTO()
        {
        }

        [Required(ErrorMessage = "First name is required.")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters long.")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long.")]
        public string? Lastname { get; set; }

        public StudentUpdateDTO(int id, string? firstname, string? lastname)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
        }
    }
}
