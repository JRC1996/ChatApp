using Azure;
using ChatApp.Models;
using ChatApp.Models.Viewmodels;
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

        public UsersController(ChatAppContext chatAppContext)
        {
                
            _chatAppContext = chatAppContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewmodel Uvm, CancellationToken cancellationToken)
        {

            var response = new Models.Response.Response();


            using (var transact = _chatAppContext.Database.BeginTransaction()) 
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
                        transact.Rollback();
                        response.Message = "Error in one or more fields";
                        return BadRequest(response);
                    
                    }

                    await _chatAppContext.Users.AddAsync(user, cancellationToken);
                    await _chatAppContext.SaveChangesAsync(cancellationToken);

                    transact.Commit();

                    response.Success = 1;
                    response.Message = "User added";


                }
                catch (Exception ex)
                {
                    transact.Rollback();
                    response.Message = ex.Message;
                    return BadRequest(response);

                }

                

            }

            return Ok(response);




        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewmodel lvm, CancellationToken cancellationToken) 
        {

            var user = await _chatAppContext.Users.FirstOrDefaultAsync(u => u.Email == lvm.Email
            && u.Password == lvm.Password, cancellationToken);

            try 
            { 
            
            
            } 
            catch (Exception ex) 
            {



            }


            return Ok();
        
        }
    }
}
