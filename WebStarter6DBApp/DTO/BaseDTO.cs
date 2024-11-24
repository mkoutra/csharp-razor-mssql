using System.ComponentModel.DataAnnotations;

namespace WebStarter6DBApp.DTO
{
    public abstract class BaseDTO
    {
        [Required(ErrorMessage = "The {0} is required.")]   // Required is an attribute used for Bean validation. Similar to annotations in Java.
        public int Id { get; set; }
        public BaseDTO()
        {
        }

        protected BaseDTO(int id)
        {
            Id = id;
        }
    }
}
