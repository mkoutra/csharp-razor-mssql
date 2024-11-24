namespace WebStarter6DBApp.DTO
{
    public class StudentReadOnlyDTO : BaseDTO
    {
        public StudentReadOnlyDTO()
        {
        }

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public StudentReadOnlyDTO(int id, string? firstname, string? lastname)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
        }
    }
}
