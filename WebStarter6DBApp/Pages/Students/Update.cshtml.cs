using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStarter6DBApp.DTO;
using WebStarter6DBApp.Models;
using WebStarter6DBApp.Services;

namespace WebStarter6DBApp.Pages.Students
{
    public class UpdateModel : PageModel
    {
        [BindProperty] // Two-way binding
        public StudentUpdateDTO StudentUpdateDTO { get; set; } = new();

        public List<Error> ErrorArray { get; set; } = [];

        private readonly IStudentService _studentService;

        // Used for injection
        public UpdateModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult OnGet(int id)
        {
            // Bring the form from the id given
            try
            {
                StudentReadOnlyDTO? studentReadOnlyDTO = _studentService.GetStudent(id);
                StudentUpdateDTO = new StudentUpdateDTO()
                {
                    Id = studentReadOnlyDTO.Id,
                    Firstname = studentReadOnlyDTO.Firstname,
                    Lastname = studentReadOnlyDTO.Lastname
                };
            }
            catch (Exception ex)
            {
                ErrorArray.Add(new Error("", ex.Message, ""));
            }
            return Page();
        }

        public void OnPost(int id)
        {
            if (!ModelState.IsValid)    // ModelState is a dictionary where validation errors are inserted (because of BindProperty)
            {
                return; // Returns the current page again but this time with errors. (asp-validation-for)
            }

            try
            {
                StudentUpdateDTO.Id = id;
                _studentService.UpdateStudent(StudentUpdateDTO);   // It has been automatically binded from the form (Names must be the same both in form and DTO)
                Response.Redirect("/Students/getall");  // HTTP 302 returns and sends a new request to /Students/getall
            }
            catch (Exception ex)
            {
                ErrorArray.Add(new Error("", ex.Message, ""));
                return;
            }
        }
    }
}
