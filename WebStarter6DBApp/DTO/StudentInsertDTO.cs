using System.ComponentModel.DataAnnotations;

namespace WebStarter6DBApp.DTO
{
    public class StudentInsertDTO
    {

        [Required(ErrorMessage ="First name is required.")]
        [MinLength(2, ErrorMessage ="First name must be at least 2 characters long.")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long.")]
        public string? Lastname { get; set; }
        
        public StudentInsertDTO()
        {
        }

        public StudentInsertDTO(string? firstname, string? lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
    }
}
