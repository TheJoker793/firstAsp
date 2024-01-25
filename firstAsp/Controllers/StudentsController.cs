using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace firstAsp.Controllers
{



    //https://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentsName = new string[] {"jean","jack","Mark","Emily","David" };
            return Ok(studentsName);
        }

    }
}
