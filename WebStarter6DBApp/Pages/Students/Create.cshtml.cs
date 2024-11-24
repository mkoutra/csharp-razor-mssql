using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStarter6DBApp.DTO;
using WebStarter6DBApp.Models;
using WebStarter6DBApp.Services;

namespace WebStarter6DBApp.Pages.Students
{
    public class CreateModel : PageModel
    {
        [BindProperty] // Two-way binding
        public StudentInsertDTO StudentInsertDTO { get; set; } = new();

        public List<Error> ErrorArray { get; set; } = [];

        private readonly IStudentService _studentService;

        // Used for injection
        public CreateModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public void OnGet()
        {
            //return Page();
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)    // ModelState is a dictionary where validation errors are inserted (because of BindProperty)
            {
                return; // Returns the current page again but this time with errors. (asp-validation-for)
            }

            try
            {
                StudentReadOnlyDTO? studentReadOnlyDTO = _studentService.InsertStudent(StudentInsertDTO);   // It has been automatically binded from the form (Names must be the same both in form and DTO)
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
