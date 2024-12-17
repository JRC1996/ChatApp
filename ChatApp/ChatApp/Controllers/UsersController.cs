using Azure;
using ChatApp.Models;
using ChatApp.Models.Services;
using ChatApp.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ChatApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

      private readonly ChatAppContext _chatAppContext;
      private IUserService _userService;

        public UsersController(ChatAppContext chatAppContext, IUserService userService)
        {
            _userService = userService;
            _chatAppContext = chatAppContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewmodel Uvm, CancellationToken cancellationToken)
        {
               

            var response = new Models.Response.Response();


            using (var transact = await _chatAppContext.Database.BeginTransactionAsync()) 
            {

                try
                {
                    var userEmail = await _chatAppContext.Users.AsNoTracking().AnyAsync(u => u.Email == Uvm.Email,
                        cancellationToken);


                    var user = new User();

                    user.Name = Uvm.Name;
                    user.Surname = Uvm.Surname;
                    user.Email = Uvm.Email;
                    user.Password = Encrypt.GetSHA256(Uvm.Password);


                    

                    if (Uvm.Password != Uvm.ComfirmPassword || !userEmail) 
                    {
                        await transact.RollbackAsync();
                        response.Message = "Error in one or more fields";
                        return BadRequest(response);
                    
                    }

                    await _chatAppContext.Users.AddAsync(user, cancellationToken);
                    await _chatAppContext.SaveChangesAsync(cancellationToken);

                    await transact.CommitAsync();

                    response.Success = 1;
                    response.Message = "User added";


                }
                catch (Exception ex)
                {
                    await transact.RollbackAsync();
                    response.Message = ex.Message;
                    return BadRequest(response);

                }

                

            }

            return Ok(response);




        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthViewmodel avm, CancellationToken cancellationToken) 
        {



            var response = new Models.Response.Response();
            var userResponse = await _userService.Auth(avm, cancellationToken);

           
            if (userResponse == null) 
            {
                response.Message = "Incorrect credentials";
                response.Success = 0;
                return BadRequest(response);                
            }

            response.Success = 1;
            response.Message = "All good";
            response.Data = userResponse;

            return Ok(response);
           
            
        }
    }
}