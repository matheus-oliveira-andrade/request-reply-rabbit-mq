using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([Required] string document)
    {
        var customers = new List<dynamic>
        {
            new
            {
                Document = "01234567890",
                Name = "Chan Su",
                Email = "chan.su@gmail.com",
            },
        };

        return Ok(customers.Where(x => x.Document == document));
    }
}