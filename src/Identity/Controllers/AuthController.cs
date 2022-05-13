using Core;
using Core.Events;
using EasyNetQ;
using Identity.Domain;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    private readonly ILogger<AuthController> _logger;
    private IBus _bus;

    public AuthController(IAuthService auth, ILogger<AuthController> logger)
    {
        _auth = auth;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNewUserRequest request)
    {
        _logger.LogInformation("Received new request for create new user");
        
        var success = _auth.Create(new User(request.Name, request.Email, request.Document));
        if (success)
        {
            _logger.LogInformation("Created new user");
            
            var result = await CreateCustomer(request);

            if (!result.ValidationResult.IsValid)
            {
                _logger.LogInformation("Error creating new customer");
                
                return BadRequest(result.ValidationResult.Errors.SelectMany(x => x.ErrorMessage));
            }
            
            _logger.LogInformation("New customer created");

            return NoContent();
        }

        return BadRequest();
    }

    private async Task<ResponseMessage> CreateCustomer(CreateNewUserRequest request)
    {
        _logger.LogInformation("Sent request for create new customer");
        
        var userCreated = new UserCreatedEvent(request.Name, request.Email, request.Document);

        _bus = RabbitHutch.CreateBus("host=localhost:5672");

        var responseMessage = await _bus.Rpc.RequestAsync<UserCreatedEvent, ResponseMessage>(userCreated);

        return responseMessage;
    }
}